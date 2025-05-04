using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopHomeUI : TopBaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(TopUIManager uiManager)
    {
        base.Init(uiManager); // BaseUI���� TopUIManager ����
        // ��ư Ŭ�� �̺�Ʈ ����
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        TopGameManager.instance.StartGame(); // TopGameManager ���� ���� ����
    }

    public void OnClickExitButton()
    {
        Application.Quit(); // ����� ���ø����̼� ���� (�����Ϳ����� �۵����� ����)
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}