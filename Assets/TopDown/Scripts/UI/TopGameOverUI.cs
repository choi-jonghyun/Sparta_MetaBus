using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopGameOverUI : TopBaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    public override void Init(TopUIManager uiManager)
    {
        base.Init(uiManager);
        // ��ư Ŭ�� �̺�Ʈ ����
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // �ٽ� ���� ��ư Ŭ�� �� ���� ���� �ٽ� �ε� (���� �����)
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ���� ��ư Ŭ�� �� ���ø����̼� ����
    public void OnClickExitButton()
    {
        Application.Quit();
       
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}