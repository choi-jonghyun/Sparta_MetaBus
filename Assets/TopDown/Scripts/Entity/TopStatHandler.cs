using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// ĳ������ �⺻ �ɷ�ġ�� �����ϰ� �����ϴ� Ŭ����
public class TopStatHandler : MonoBehaviour
{
    // ü�� (1 ~ 100 ���� ���� ���)
    [Range(1, 100)][SerializeField] private int health = 10;
    // �ܺο��� ���� ������ ������Ƽ (�� ���� �� �ڵ����� 0~100 ���̷� ����)
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    // �̵� �ӵ� (1f ~ 20f ���� ���� ���)
    [Range(1f, 20f)][SerializeField] private float speed = 3;
    // �ܺο��� ���� ������ ������Ƽ (�� ���� �� 0~20f�� ����)
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
