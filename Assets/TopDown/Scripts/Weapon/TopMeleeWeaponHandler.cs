using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMeleeWeaponHandler : TopWeaponHandler
{
    // 근접 무기 전용 핸들러 (TopWeaponHandler 상속)
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;  // 공격 범위 (충돌 박스 크기)

    // 무기 크기에 따라 충돌 범위를 확장
    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        // BoxCast로 근접 공격 판정 (LookDirection 방향으로 충돌 검사)
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, //박스크기
            collideBoxSize, 0, //회전 없음
            Vector2.zero, //이동 거리 없음(고정)
            0,              //거리 0 
            target);        // 공격 가능한 대상

        if (hit.collider != null)
        {
            TopResourceController resourceController = hit.collider.GetComponent<TopResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                if (IsOnKnockback)
                {
                    TopBaseController controller = hit.collider.GetComponent<TopBaseController>();
                }
                if (Controller != null)
                {
                    Controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                }
            }
        }
    }
    public override void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 100, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
        
    }
}
