using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopInteractionTrigger : MonoBehaviour
{

    public GameObject interactionPopup; // 상호작용 안내 UI (에디터에서 연결)
    public bool isPlayerInRange = false;    // 플레이어가 범위 안에 있는지
    public string miniGameSceneName = "TopDownScene"; //미니게임 이름
    

    private void Start()
    {
        isPlayerInRange = false;
        interactionPopup.SetActive(false);
    }
    void Update()
    {

        // 범위 안에 있고 F키를 누르면 상호작용 실행
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            SceneManager.LoadScene(miniGameSceneName);
          
        }
    }
    // 플레이어가 트리거에 들어왔을 때 실행되는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (other.CompareTag("Player"))
        {

            isPlayerInRange = true;
            interactionPopup.SetActive(true);   // 플레이어가 접근하면 팝업 표시

            // 원하는 이벤트 실행 (예: 미니게임 시작, 대화 창 띄우기 등)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionPopup.SetActive(false);  // 플레이어가 벗어나면 팝업 숨김
        }
    }
}
