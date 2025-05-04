using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ UI ���¸� �����ϴ� ������
public enum UIState
{
    Home,
    Game,
    GameOver,
}

public class TopUIManager : MonoBehaviour
{
    TopHomeUI homeUI;
    TopGameUI gameUI;
    TopGameOverUI gameOverUI;
    private UIState currentState; // ���� UI ����

    private void Awake()
    {
        // �ڽ� ������Ʈ���� ������ UI�� ã�� �ʱ�ȭ
        homeUI = GetComponentInChildren<TopHomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<TopGameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<TopGameOverUI>(true);
        gameOverUI.Init(this);

        // �ʱ� ���¸� Ȩ ȭ������ ����
        ChangeState(UIState.Home);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangeWave(int waveIndex)
    {
        gameUI.UpdateWaveText(waveIndex);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP / maxHP);
    }

    // ���� UI ���¸� �����ϰ�, �� UI ������Ʈ�� ���¸� ����
    public void ChangeState(UIState state)
    {
        currentState = state;


        // �� UI�� �ڽ��� �������� �� �������� �Ǵ��ϰ� ǥ�� ���� ����
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
}