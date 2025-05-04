using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ü�� �����ϴ� ���� �Ŵ��� Ŭ����
public class TopGameManager : MonoBehaviour
{
    public static TopGameManager instance;

    public TopPlayerController player { get; private set; } // �÷��̾� ��Ʈ�ѷ� (�б� ���� ������Ƽ)
    private TopResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0; // ���� ���̺� ��ȣ

    private TopEnemyManager enemyManager; // �� ���� �� �����ϴ� �Ŵ���

    private TopUIManager uiManager;
    public static bool isFirstLoading = true;

    private void Awake()
    {
        // �̱��� �Ҵ�
        instance = this;

        // �÷��̾� ã�� �ʱ�ȭ
        player = FindObjectOfType<TopPlayerController>();
        player.Init(this);

        // UI �Ŵ��� ���� ȹ��
        uiManager = FindObjectOfType<TopUIManager>();

        // �÷��̾��� ü�� ���ҽ� ��Ʈ�ѷ� ����
        _playerResourceController = player.GetComponent<TopResourceController>();

        // ü�� ���� �̺�Ʈ�� UI�� ����
        // �ߺ� ��� ������ ���� ���� ������ �� �ٽ� ���
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);


        // �� �Ŵ��� �ʱ�ȭ
        enemyManager = GetComponentInChildren<TopEnemyManager>();
        enemyManager.Init(this);
    }

    private void Start()
    {
        // ù �ε��̸� ��� ���·� ���� (Ÿ��Ʋ ȭ�鿡�� ��ư���� �����ϵ���)
        if (!isFirstLoading)
        {
            StartGame(); // �� ��° ���� �� �ε� �� �ڵ� ����
        }
        
    }

    public void StartGame()
    {
        uiManager.SetPlayGame(); // UI ���¸� ���� ���·� ��ȯ
        StartNextWave(); // ù ���̺� ����
    }

    void StartNextWave()
    {
        currentWaveIndex += 1; // ���̺� �ε��� ����
        uiManager.ChangeWave(currentWaveIndex); // UI�� ���� ���̺� ǥ��
        // 5���̺긶�� ���̵� ���� (��: 1~4 �� ���� 1, 5~9 �� ���� 2 ...)
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // ���̺� ���� �� ���� ���̺� ����
    public void EndOfWave()
    {
        StartNextWave();
    }

    // �÷��̾ �׾��� �� ���� ���� ó��
    public void GameOver()
    {
       
        enemyManager.StopWave(); // �� ���� ����
        uiManager.SetGameOver(); //���ӿ��� UI ����

        
    }

    
  
}