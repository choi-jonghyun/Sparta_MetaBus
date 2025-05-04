using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

// ��� ĳ������ �⺻ ������, ȸ��, �˹� ó���� ����ϴ� ��� Ŭ����
public class TopBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ

    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ ���� ������
    [SerializeField] private Transform weaponPivot; // ���⸦ ȸ����ų ���� ��ġ

    protected Vector2 movementDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // ���� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero; // �˹� ����
    private float knockbackDuration = 0.0f; // �˹� ���� �ð�

    protected TopAnimationHandler animationHandler;
    protected TopStatHandler statHandler; // ĳ������ �ɷ�ġ(�ӵ�, ü�� ��)

    [SerializeField] public TopWeaponHandler weaponPrefab;  // ������ ���� ������ (������ �ڽĿ��� ã�� ���)
    protected TopWeaponHandler weaponHandler;  // ������ ����

    protected bool isAttacking;  // ���� �� ����
    protected float timeSinceLatAttack = float.MaxValue; // ������ ���� ���� ��� �ð�
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<TopAnimationHandler>();
        statHandler = GetComponent<TopStatHandler>();

        // �������� �����Ǿ� �ִٸ� �����ؼ� ���� ��ġ�� ����
        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<TopWeaponHandler>(); // �̹� �پ� �ִ� ���� ���
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay(); // ���� �Է� �� ��Ÿ�� ����
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // �˹� �ð� ����
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed; // �̵� �ӵ�

        // �˹� ���̸� �̵� �ӵ� ���� + �˹� ���� ����
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // �̵� �ӵ� ����
            direction += knockback; // �˹� ���� �߰�
        }

        // ���� ���� �̵�
        _rigidbody.velocity = direction;
        // �̵� �ִϸ��̼� ó��
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // ��������Ʈ �¿� ����
        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            // ���� ȸ�� ó��
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        weaponHandler?.Rotate(isLeft);  // ���⵵ �Բ� �¿� ���� ó��
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // ��� ������ �ݴ�� �о
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        if(weaponHandler == null)
            return;

        // ���� ��ٿ� ���̸� �ð� ����
        if (timeSinceLatAttack <= weaponHandler.Delay)
        {
            timeSinceLatAttack += Time.deltaTime;
        }

        // ���� �Է� ���̰� ��Ÿ���� �������� ���� ����
        if (isAttacking && timeSinceLatAttack > weaponHandler.Delay)
        {
            timeSinceLatAttack = 0;
            Attack(); //���� ���� ����
        }
    }

    protected virtual void Attack()
    {
        // �ٶ󺸴� ������ ���� ���� ����
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }
    public virtual void Death()
    {
        // ������ ����
        _rigidbody.velocity = Vector3.zero;

        // ��� SpriteRenderer�� ���� ���缭 ���� ȿ�� ����
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // ��� ������Ʈ(��ũ��Ʈ ����) ��Ȱ��ȭ
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // 2�� �� ������Ʈ �ı�
        Destroy(gameObject, 2f);
    }
}
