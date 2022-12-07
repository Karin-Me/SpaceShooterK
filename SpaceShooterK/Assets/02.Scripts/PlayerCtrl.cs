using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 캐시 처리할 변수
    private float horizontal;   // 평행 이동 변수
    private float vertical;     // 수직 이동 변수
    private float rotate;       // 회전 속도 변수
    private Transform tr;       // 컴포넌트를 캐시 처리할 변수. Transform 은 클래스이다.
    private Vector3 moveDir;    // Vector3 를 moveDir 변수에 할당.

    // 이동속도, 회전속도 변수 (public 으로 선언되어 Inspector View 에 노출됨.)
    public float moveSpeed = 10.0f, turnSpeed = 80.0f;


    void Start()
    {
        // Transform 컴포넌트를 추출해 변수에 대입(this.gameObject 는 생략가능하다.)
        tr = this.GetComponent<Transform>();
    }


    void Update()
    {
        // float horizontal = 과정을 위에 변수명으로 할당하여 그 과정을 간략화.
        // float vertical = 과정을 위에 변수명으로 할당하여 그 과정을 간략화.
        // float rotate = 과정을 위에 
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f
        rotate = Input.GetAxis("Mouse X");          // 마우스의 이동값을 받아와 구현한다.

        //Debug.Log("호리존탈=" + horizontal);
        //Debug.Log("버티칼=" + vertical);

        // 01.주석 시작


        // 전후좌우 이동 방향 벡터 계산
        moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);

        // Translate(이동 방향 * 속력 * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up 축을 기준으로 turnSpeed 만큼의 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rotate, Space.Self);
    }
}




////    01. Transform 컴포넌트의 위치를 변경
//// [컴포넌트.속성(프로퍼티)]/ [+= 누적 대입 연산자] / [저장할 값]
//transform.position += new Vector3(0, 0, 1);

//// 정규화 벡터를 사용한 코드
//transform.position += Vector3.forward * 1;

//// Transform 컴포넌트를 추출해 변수에 대입한 변수 tr 를 transform 컴포넌트를 대신하여 대입한다.
//tr.position += Vector3.forward * 1;

//// Translate 함수를 사용한 이동 로직
////  Space.Self 가 붙지 않으면 기본적으로 로컬좌표계이다.// Space.Self 는 생략가능하다.
//// tr.Translate(Vector3.forward * 1); >> 로컬좌표계로 이동하는 로직
//// tr.Translate(Vector3.forward * 1, Space.World); >> 월드좌표계로 이동하는 로직
//tr.Translate(Vector3.forward * 1, Space.Self);

//// translate 함수를 사용한 이동 로직에 time.deltatime 를 곱한 로직
//// vertical 는 키보드 입력값인 w 와 s 그리고 방향키 up 과 down 이다.
//tr.Translate(Vector3.forward * Time.deltaTime * vertical * moveSpeed, Space.Self);
//// Translate 함수를 사용한 이동 로직에 Time.deltaTime 를 곱한 로직
//// horizontal 는 키보드 입력값인 a 와 d 그리고 방향키 left 와 right 이다.
//tr.Translate(Vector3.right * Time.deltaTime * horizontal * moveSpeed, Space.Self);