using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 전체를 관리하는 메인 매니저 클래스
public class TopGameManager : MonoBehaviour
{
    public static TopGameManager instance;

    public TopPlayerController player { get; private set; } // 플레이어 컨트롤러 (읽기 전용 프로퍼티)
    private TopResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0; // 현재 웨이브 번호

    private TopEnemyManager enemyManager; // 적 생성 및 관리하는 매니저

    private TopUIManager uiManager;
    public static bool isFirstLoading = true;

    private void Awake()
    {
        // 싱글톤 할당
        instance = this;

        // 플레이어 찾고 초기화
        player = FindObjectOfType<TopPlayerController>();
        player.Init(this);

        // UI 매니저 참조 획득
        uiManager = FindObjectOfType<TopUIManager>();

        // 플레이어의 체력 리소스 컨트롤러 설정
        _playerResourceController = player.GetComponent<TopResourceController>();

        // 체력 변경 이벤트를 UI에 연결
        // 중복 등록 방지를 위해 먼저 제거한 뒤 다시 등록
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);


        // 적 매니저 초기화
        enemyManager = GetComponentInChildren<TopEnemyManager>();
        enemyManager.Init(this);
    }

    private void Start()
    {
        // 첫 로딩이면 대기 상태로 유지 (타이틀 화면에서 버튼으로 시작하도록)
        if (!isFirstLoading)
        {
            StartGame(); // 두 번째 이후 씬 로딩 시 자동 시작
        }
        
    }

    public void StartGame()
    {
        uiManager.SetPlayGame(); // UI 상태를 게임 상태로 전환
        StartNextWave(); // 첫 웨이브 시작
    }

    void StartNextWave()
    {
        currentWaveIndex += 1; // 웨이브 인덱스 증가
        uiManager.ChangeWave(currentWaveIndex); // UI에 현재 웨이브 표시
        // 5웨이브마다 난이도 증가 (예: 1~4 → 레벨 1, 5~9 → 레벨 2 ...)
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // 웨이브 종료 후 다음 웨이브 시작
    public void EndOfWave()
    {
        StartNextWave();
    }

    // 플레이어가 죽었을 때 게임 오버 처리
    public void GameOver()
    {
       
        enemyManager.StopWave(); // 적 스폰 중지
        uiManager.SetGameOver(); //게임오버 UI 띄우기

        
    }

    
  
}