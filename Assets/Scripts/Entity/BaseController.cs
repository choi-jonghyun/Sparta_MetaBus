using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

// ��� ĳ������ �⺻ ������, ȸ��, �˹� ó���� ����ϴ� ��� Ŭ����
public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ

    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ ���� ������
    

    protected Vector2 movementDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // ���� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }

    private bool isLookingLeft = false;

    

    


    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(movementDirection);
        
       

    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);

       
      
    }

   

    

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * 10; // �̵� �ӵ�

      

        // ���� ���� �̵�
        _rigidbody.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        if (direction.x > 0)
        {
            isLookingLeft = false;
            characterRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            isLookingLeft = true;
            characterRenderer.flipX = true;
        }
        else
        {   // ���� ���� ����
            characterRenderer.flipX = isLookingLeft;
        }

       
    }

  
}