using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopProjectileManager : MonoBehaviour
{
    private static TopProjectileManager instance;
    public static TopProjectileManager Instance { get { return instance; } }

    // �߻��� ����ü ������ �迭 (�Ѿ� ������)
    [SerializeField] private GameObject[] projectilePrefabs;

    // �Ѿ��� �¾��� �� ����� ��ƼŬ �ý���
    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(TopRangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        // �ش� ���⿡�� ����� ����ü ������ ��������
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];

        // ������ ��ġ�� ����ü ����
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        // ����ü�� �ʱ� ���� ���� (����, ���� ������)
        TopProjectileController projectileController = obj.GetComponent<TopProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this);
    }

    // �Ѿ��� �浹���� ��, �ش� ��ġ�� ����Ʈ ��ƼŬ�� �����ϴ� �Լ�
    public void CreateImpactParticlesAtPostion(Vector3 position, TopRangeWeaponHandler weaponHandler)
    {
        // ��ƼŬ ��ġ�� �浹 �������� �̵�
        impactParticleSystem.transform.position = position;
        // Burst ����: ���� ���� �Ѿ� ũ�⿡ ���� ����
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));
        // �Ѿ� ũ�⿡ ���� ���� �ӵ��� ������Ŵ
        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
        // ��ƼŬ ���
        impactParticleSystem.Play();
    }

}
