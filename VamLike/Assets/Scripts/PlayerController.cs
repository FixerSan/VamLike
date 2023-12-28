using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;         //이동속도
    public float rotationSpeed;      //회전속도

    public Animator anim;         //애니메이터
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // moveSpeed = PlayerStatController.instance.moveSpeed[0].value;        //TODO : 나중에 코딩
    }

    private void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        //이동 바향 벡터를 기반으로 회전 각도를 계산
        if(moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);

            //회전을 부드럽게 적용하기 위한 Slerp사용
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        //애니메이션 적용
        if (moveInput != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }

        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
