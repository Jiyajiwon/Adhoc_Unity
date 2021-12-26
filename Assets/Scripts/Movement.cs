using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Physical Component Values
    Rigidbody rigidbody;
    Vector3 movement;

    // Movement Values
    float horizon; // �¿�
    float vertical; // ���Ʒ�

    float speed = 7f; // �ȱ� �ӵ�
    float runningSpeed = 11f; // �޸��� �ӵ�
    float jumpPower = 6f; // �����Ŀ�
    float Stamina = 100f; // ���¹̳�

    // Bool Statement
    bool isJumping = false; // ���� 
    bool jumpState = false; // ���� ���� 

    void Awake() // ������
    {
        rigidbody = GetComponent<Rigidbody>(); // ��ü Physics �̽�
    }
   
    void Update()
    {
        horizon = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)) speed = runningSpeed;
        else speed = 7f;

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