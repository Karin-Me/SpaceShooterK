using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라가야 할 대상을 연결할 변수
    public Transform targetTr;
    // Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;

    // 따라갈 대상으로부터 떨어질 거리
    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    // Y 축으로 이동할 높이
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // 반응 속도
    public float damping = 10.0f;

    // 카메라 LookAt 의 Offset 값
    public float targetOffset = 2.0f;   //Target Offset 적용

    // SmoothDamp 에서 사용할 변수
    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        // Main Camera 자신의 Transform 컴포넌트를 추출
        camTr = GetComponent<Transform>();
    }


    void LateUpdate()
    {
        // ====01_로직부터.==== //     



        // 03_로직_SmoothDamp 로직.        

        // 추적해야 할 대상의 뒤쪽으로 distance 만큼 이동
        // 높이를 height 만큼 이동
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // SmoothDamp 을(를) 이용한 위치 보간.

        // camTr.position 은 시작 위치
        // pos 은 목표 위치
        // ref velocity(current velocity) 은 현재 속도
        // damping 은 목표 위치까지 도달할 시간
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);

        // Camera 를 피벗 좌표를 향해 회전 (Target Offset 적용)
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}


//// 01_로직_초반 로직.         

//// 추적해야 할 대상의 뒤쪽으로 distance 만큼 이동
//// 높이를 height 만큼 이동
//camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);
//// 좌표계를 통한 카메라 팔로우 
////camTr.position = targetTr.position + (-(targetTr.position + new Vector3(0, 0, 1)) * distance) +
////(new Vector3(0,1,0) * height);

//// Camera 를 피벗 좌표를 향해 회전
//camTr.LookAt(targetTr.position);



//// 02_로직_Slerp 로직.

//// 추적해야 할 대상의 뒤쪽으로 distance 만큼 이동
//// 높이를 height 만큼 이동
//Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

//// 구면 선형 보간 함수를 사용해 부드럽게 위치를 변경
//camTr.position = Vector3.Slerp(camTr.position,              // 시작위치
//                               pos,                         // 목표 위치
//                               Time.deltaTime * damping);   // 시간 t

//// Camera 를 피벗 좌표를 향해 회전
//camTr.LookAt(targetTr.position);
