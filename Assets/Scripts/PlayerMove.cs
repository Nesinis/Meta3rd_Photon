﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    // 캐릭터 컨트롤러
    CharacterController cc;

    // 이동 속력
    public float moveSpeed = 5;

    // 중력
    float gravity = -9.81f;
    // y 속력
    float yVelocity;
    // 점프 초기 속력
    public float jumpPower = 3;

       
    void Start()
    {
        // 캐릭터 컨트롤러 가져오자.
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1 .키보드 WASD 키 입력을 받자
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2. 방향을 정하자.
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;

        dir.Normalize();

        // 만약에 땅에 있으면 yVelocity 를 0 으로 초기화
        if(cc.isGrounded)
        {
            yVelocity = 0;
        }

        // 만약에 Space 바를 누르면
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // yVelocity 를 jumpPower 로 설정
            yVelocity = jumpPower;
        }

        // yVelocity 값을 중력에 의해서 변경시키자.
        yVelocity += gravity * Time.deltaTime;

        #region 물리적인 점프 아닌것
        // dir.y 에 yVelocity 값을 셋팅
        dir.y = yVelocity;

        // 3. 그 방향으로 움직이자.
        cc.Move(dir * moveSpeed * Time.deltaTime);
        #endregion

        #region 물리적인 점프
        //dir = dir * moveSpeed;
        //dir.y = yVelocity;
        //cc.Move(dir * Time.deltaTime);
        #endregion
    }
}
