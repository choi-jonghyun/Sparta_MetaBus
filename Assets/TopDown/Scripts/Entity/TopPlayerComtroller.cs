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
        this.gameManager = gameManager; // ���� �Ŵ��� ����
        camera = Camera.main; // ���� ī�޶� ���� ȹ��
    }

    protected override void HandleAction()
    {
        

        

        
    }
    public override void Death()
    {
        base.Death();
        gameManager.GameOver(); // ���� ���� ó��
    }

    void OnMove(InputValue inputValue)
    {
        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        //float horizontal = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ��/��
        //float vertical = Input.GetAxisRaw("Vertical"); // W/S �Ǵ� ��/��
        movementDirection = inputValue.Get<Vector2>();

        // ���� ���� ����ȭ (�밢���� �� �ӵ� ����)
        movementDirection = /*new Vector2(horizontal, vertical)*/movementDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {


        // ���콺 ��ġ�� ȭ�� ��ǥ �� ���� ��ǥ�� ��ȯ
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // ���� ��ġ�κ��� ���콺 ��ġ������ ���� ���
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
            return; //UI�� ���콺�� �ö������� �Ƚ�
        isAttacking = Input.GetMouseButton(0);
    }
}