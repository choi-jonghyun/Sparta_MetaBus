using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRangeWeaponHandler : TopWeaponHandler
{

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition; // �Ѿ��� �߻�Ǵ� ��ġ

    [SerializeField] private int bulletIndex; // ����� �Ѿ� ������ �ε���
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1; // �Ѿ� ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration; // �Ѿ��� ����ִ� �ð�
    public float Duration { get { return duration; } }

    [SerializeField] private float spread; // �Ѿ� ���� ���� ����
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot; // �� ���� �߻��� �Ѿ� ��
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngel; // �Ѿ˵� ���� ���� ���� ����
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }

    [SerializeField] private Color projectileColor; // �Ѿ� ���� (�ð��� ȿ��)
    public Color ProjectileColor { get { return projectileColor; } }

    private TopProjectileManager projectileManager; // ����ü�� �߻��ϴ� �Ŵ��� ����

    protected override void Start()
    {
        base.Start();
        projectileManager = TopProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngel; // �Ѿ� �� ���� ����
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; // �߻��� �Ѿ� ��

        // �Ѿ˵��� �¿� ��Ī���� ������ �ϱ� ���� ���� ���� ���
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;


        // �� �Ѿ˸��� ȸ�� ���� ��� �� �߻�
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i; // �⺻ ����
            float randomSpread = Random.Range(-spread, spread); // ���� ���� ����
            angle += randomSpread;

            // ���� ����ü ���� (Controller.LookDirection = ĳ���Ͱ� �ٶ󺸴� ����)
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        // ȸ���� ���� ���� ��� �� ����ü �߻� ��û
        projectileManager.ShootBullet(
            this,                                 // � ���⿡�� �߻��ߴ��� (TopRangeWeaponHandler)
            projectileSpawnPosition.position,     // �Ѿ��� ���� ��ġ
            RotateVector2(_lookDirection, angle)  // ���� ���� ȸ�� (ź ����, Ȯ�� �� ������ ���)
        );
    }

    // ���� ���͸� �־��� ������ŭ ȸ��
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}