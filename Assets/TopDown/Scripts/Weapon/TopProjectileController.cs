using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TopProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; // ����(�� ��) �浹 ������ ���̾�

    private TopRangeWeaponHandler rangeWeaponHandler; // �߻翡 ���� ���� ���� ����

    private float currentDuration; // ������� ����ִ� �ð�
    private Vector2 direction; // �߻� ����
    private bool isReady; // �߻� �غ� �Ϸ� ����
    private Transform pivot; // �Ѿ��� �ð� ȸ���� ���� �ǹ�

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true; // �浹 �� ����Ʈ�� �������� ����

    private TopProjectileManager projectileManager;

    // �ʱ� ������Ʈ ���� ����
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0); // �ǹ� ������Ʈ (��������Ʈ ȸ����)
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        // ���� �ð� ����
        currentDuration += Time.deltaTime;

        // ������ ���� �ð� �ʰ� �� �ڵ� �ı�
        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // ���� �̵� ó�� (���� * �ӵ�)
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹 �� �� �ణ �� ��ġ�� ����Ʈ �����ϰ� �ı�
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        // ���� ��� ���̾ �浹���� ���
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            // ������ ������ ���� ü�� �ý����� �ִ��� Ȯ��
            TopResourceController resourceController = collision.GetComponent<TopResourceController>();

            if (resourceController != null)
            {
                // ������ ����
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);

                // �˹� ������ �Ǿ� �ִٸ� �˹鵵 ����
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    TopBaseController controller = collision.GetComponent<TopBaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }

    }


    public void Init(Vector2 direction, TopRangeWeaponHandler weaponHandler, TopProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;

        // ũ�� �� ���� ����
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        // ȸ�� ���� ���� (���� ���� ����)
        transform.right = this.direction;

        // X ���⿡ ���� ��������Ʈ ���Ʒ� ���� (�¿� �߻� ��)
        if (this.direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    // ����ü �ı� �Լ�
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // �Ѿ��� �浹���� ��, �ش� ��ġ�� ����Ʈ ��ƼŬ�� �����ϴ� �Լ�
            projectileManager.CreateImpactParticlesAtPostion(position, rangeWeaponHandler);
        }
        Destroy(this.gameObject);
    }
}

