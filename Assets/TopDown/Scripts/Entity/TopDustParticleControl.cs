using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true; // 걷기 중 파티클 생성 여부
    [SerializeField] private ParticleSystem dustParticleSystem; // 사용할 파티클 시스템

    public void CreateDustParicles()
    {
        // 조건이 true일 때만 실행
        if (createDustOnWalk)
        {
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}
