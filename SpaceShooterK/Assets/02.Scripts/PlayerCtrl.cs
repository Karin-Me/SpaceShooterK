using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float horizontal, vertical;

    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;

    void Start()
    {
        // Transform 컴포넌트를 추출해 변수에 대입(this.gameObject 는 생략가능하다.)
        tr = this.GetComponent<Transform>();
    }


    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical   = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log("horizontal=" + horizontal);
        Debug.Log("vertical=" + vertical);


        //// Transform 컴포넌트의 위치를 변경
        //// [컴포넌트.속성(프로퍼티)]/ [+= 누적 대입 연산자] / [저장할 값]
        //transform.position += new Vector3(0, 0, 1);

        //// Transform 컴포넌트를 추출해 변수에 대입한 변수 tr 를 transform 대신하여 대입한다.
        //tr.position += Vector3.forward * 1;

        // Translate 함수를 사용한 이동 로직
        // forward * 1 뒤에 인자가 붙지 않으면 기본적으로 로컬좌표계이다.
        // tr.Translate(Vector3.forward * 1); >> 로컬좌표계로 이동하는 로직
        // tr.Translate(Vector3.forward * 1, Space.World); >> 월드좌표계로 이동하는 로직
        tr.Translate(Vector3.forward * 1, Space.Self);  
    }
}
