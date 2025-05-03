using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f; //점프하는 힘
    public float forwardSpeed = 3f; //정면으로 이동하는 힘
    public bool isDead = false; // 생사여부
    float deathCooldown = 0f; //바로죽는아닌 일정시간 지난 후 죽음

    bool isFlap=false; // 점프 유무 확인

    public bool godMode=false; //테스트용
    private Rigidbody2D rb;
    

    

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager= GameManager.Instance;

        animator = GetComponentInChildren<Animator>(); //GetComponent는 게임 오브젝트에 추가된 컴포넌트를 가져오는 메서드
        _rigidbody =GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("Not Founded Animator");

        if (_rigidbody == null)
            Debug.LogError("Not Founded Rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                //게임 재시작
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown-= Time.deltaTime;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity; // velocity 가속도
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap=false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp( (_rigidbody.velocity.y*10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (godMode) return; 

        if (isDead) return;
        

        isDead= true;
        deathCooldown = 1f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 20f;

       

        animator.SetInteger("IsDie", 1);
        gameManager.GameOver();
        // 점수 넘기고 씬 이동
        
        
    }
  
}
