using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TopPlayerController : TopBaseController
{
    private TopGameManager gameManager;
    private Camera camera;

    public void Init(TopGameManager gameManager)
    {
        this.gameManager = gameManager; // 게임 매니저 연결
        camera = Camera.main; // 메인 카메라 참조 획득
    }

    protected override void HandleAction()
    {
        

        

        
    }
    public override void Death()
    {
        base.Death();
        gameManager.GameOver(); // 게임 오버 처리
    }

    void OnMove(InputValue inputValue)
    {
        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        //float horizontal = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        //float vertical = Input.GetAxisRaw("Vertical"); // W/S 또는 ↑/↓
        movementDirection = inputValue.Get<Vector2>();

        // 방향 벡터 정규화 (대각선일 때 속도 보정)
        movementDirection = /*new Vector2(horizontal, vertical)*/movementDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {


        // 마우스 위치를 화면 좌표 → 월드 좌표로 변환
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // 현재 위치로부터 마우스 위치까지의 방향 계산
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    void OnFire(InputValue inputValue)
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return; //UI에 마우스가 올라가있으면 안쏨
        isAttacking = Input.GetMouseButton(0);
    }
}