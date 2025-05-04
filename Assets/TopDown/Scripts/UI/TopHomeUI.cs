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
        base.Init(uiManager); // BaseUI에서 TopUIManager 저장
        // 버튼 클릭 이벤트 연결
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        TopGameManager.instance.StartGame(); // TopGameManager 통해 게임 시작
    }

    public void OnClickExitButton()
    {
        Application.Quit(); // 빌드된 애플리케이션 종료 (에디터에서는 작동하지 않음)
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}