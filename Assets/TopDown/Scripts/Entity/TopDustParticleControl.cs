using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true; // �ȱ� �� ��ƼŬ ���� ����
    [SerializeField] private ParticleSystem dustParticleSystem; // ����� ��ƼŬ �ý���

    public void CreateDustParicles()
    {
        // ������ true�� ���� ����
        if (createDustOnWalk)
        {
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}
