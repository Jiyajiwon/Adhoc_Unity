using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Physical Component Values
    Rigidbody rigidbody;
    Vector3 movement;

    // Movement Values
    float horizon; // 좌우
    float vertical; // 위아래

    float speed = 7f; // 걷기 속도
    float runningSpeed = 11f; // 달리기 속도
    float jumpPower = 6f; // 점프파워
    float stamina = 5f; // 스태미나

    // Bool Statement
    bool isJumping = false; // 점프 
    bool jumpState = false; // 점프 가능 

    void Awake() // 시작점
    {
        rigidbody = GetComponent<Rigidbody>(); // 물체 Physics 이식
    }
   
    void Update()
    {
        horizon = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                speed = runningSpeed;
                stamina -= 1 * Time.deltaTime;
                Debug.Log(stamina);
            }
            else {
                speed = 7f;
            }
        }
        else
        {
            speed = 7f;
            if (stamina < 5) stamina += 1 * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")) {
            if(jumpState == true)isJumping = true;
        }
    }

    void FixedUpdate(){

        Run();
        Jump();

    }

    void Run(){

        movement.Set(horizon, 0, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);

    }

    void Jump(){
      if (!isJumping) return;
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isJumping = false;
        jumpState = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")jumpState = true; 
    }
}
