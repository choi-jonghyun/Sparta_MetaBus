using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TopResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; // ���� �� ���� ���� �ð�

    private TopBaseController baseController;
    private TopStatHandler statHandler;
    private TopAnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue; // ������ ü�� ���� ���� ��� �ð�

    public float CurrentHealth { get; private set; } // ���� ü�� (�ܺ� ���ٸ� ���)
    public float MaxHealth => statHandler.Health; // �ִ� ü���� StatHandler�κ��� ������

    public AudioClip damageClip;

    // ü���� �ٲ� �� ȣ��Ǵ� �̺�Ʈ (���� ü��, �ִ� ü�� ����)
    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        statHandler = GetComponent<TopStatHandler>();
        animationHandler = GetComponent<TopAnimationHandler>();
        baseController = GetComponent<TopBaseController>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        // ���� ���� ���¶�� �ð� ����
        if (timeSinceLastChange < healthChangeDelay)
        {
            // ���� �ð� ���� �� �ִϸ��̼ǿ��� �˸�
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    // ü�� ���� �Լ� (���� or ȸ��)
    public bool ChangeHealth(float change)
    {
        // ��ȭ ���ų� ���� ���¸� ����
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f; // �ٽ� ���� ����

        // ü�� ����
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        // ü�� ���� �̺�Ʈ ȣ�� (UI ��� �� ���� ������ ó����)
        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);


        // �������� ��� (����)
        if (change < 0)
        {
            animationHandler.Damage(); // �´� �ִϸ��̼� ����
            if (damageClip != null)
                TopSoundManager.PlayClip(damageClip); // ���尡 �����Ǿ� ���� ��� ���
                                       

        }

        // ü���� 0 ���ϰ� �Ǹ� ��� ó��
        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    // �ܺο��� ü�� ���� �̺�Ʈ�� ����ϴ� �Լ�
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    // �ܺο��� ü�� ���� �̺�Ʈ�� �����ϴ� �Լ�
    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }

    private void Death()
    {
        baseController.Death();
    }

}
