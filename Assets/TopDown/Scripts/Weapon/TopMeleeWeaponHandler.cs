using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMeleeWeaponHandler : TopWeaponHandler
{
    // ���� ���� ���� �ڵ鷯 (TopWeaponHandler ���)
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;  // ���� ���� (�浹 �ڽ� ũ��)

    // ���� ũ�⿡ ���� �浹 ������ Ȯ��
    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        // BoxCast�� ���� ���� ���� (LookDirection �������� �浹 �˻�)
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, //�ڽ�ũ��
            collideBoxSize, 0, //ȸ�� ����
            Vector2.zero, //�̵� �Ÿ� ����(����)
            0,              //�Ÿ� 0 
            target);        // ���� ������ ���

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
