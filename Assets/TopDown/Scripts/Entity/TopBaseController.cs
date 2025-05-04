using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

// 모든 캐릭터의 기본 움직임, 회전, 넉백 처리를 담당하는 기반 클래스
public class TopBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트

    [SerializeField] private SpriteRenderer characterRenderer; // 좌우 반전을 위한 렌더러
    [SerializeField] private Transform weaponPivot; // 무기를 회전시킬 기준 위치

    protected Vector2 movementDirection = Vector2.zero; // 현재 이동 방향
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero; // 넉백 방향
    private float knockbackDuration = 0.0f; // 넉백 지속 시간

    protected TopAnimationHandler animationHandler;
    protected TopStatHandler statHandler; // 캐릭터의 능력치(속도, 체력 등)

    [SerializeField] public TopWeaponHandler weaponPrefab;  // 장착할 무기 프리팹 (없으면 자식에서 찾아 사용)
    protected TopWeaponHandler weaponHandler;  // 장착된 무기

    protected bool isAttacking;  // 공격 중 여부
    protected float timeSinceLatAttack = float.MaxValue; // 마지막 공격 이후 경과 시간
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<TopAnimationHandler>();
        statHandler = GetComponent<TopStatHandler>();

        // 프리팹이 지정되어 있다면 생성해서 장착 위치에 부착
        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<TopWeaponHandler>(); // 이미 붙어 있는 무기 사용
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay(); // 공격 입력 및 쿨타임 관리
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // 넉백 시간 감소
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed; // 이동 속도

        // 넉백 중이면 이동 속도 감소 + 넉백 방향 적용
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // 이동 속도 감소
            direction += knockback; // 넉백 방향 추가
        }

        // 실제 물리 이동
        _rigidbody.velocity = direction;
        // 이동 애니메이션 처리
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // 스프라이트 좌우 반전
        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            // 무기 회전 처리
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        weaponHandler?.Rotate(isLeft);  // 무기도 함께 좌우 반전 처리
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // 상대 방향을 반대로 밀어냄
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        if(weaponHandler == null)
            return;

        // 공격 쿨다운 중이면 시간 누적
        if (timeSinceLatAttack <= weaponHandler.Delay)
        {
            timeSinceLatAttack += Time.deltaTime;
        }

        // 공격 입력 중이고 쿨타임이 끝났으면 공격 실행
        if (isAttacking && timeSinceLatAttack > weaponHandler.Delay)
        {
            timeSinceLatAttack = 0;
            Attack(); //실제 공격 실행
        }
    }

    protected virtual void Attack()
    {
        // 바라보는 방향이 있을 때만 공격
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }
    public virtual void Death()
    {
        // 움직임 정지
        _rigidbody.velocity = Vector3.zero;

        // 모든 SpriteRenderer의 투명도 낮춰서 죽은 효과 연출
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // 모든 컴포넌트(스크립트 포함) 비활성화
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // 2초 후 오브젝트 파괴
        Destroy(gameObject, 2f);
    }
}
