using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 캐시 처리할 변수
    private float horizontal;   // 평행 이동 변수
    private float vertical;     // 수직 이동 변수
    private float rotate;       // 회전 속도 변수

    // public 으로 선언되어 Inspector View 에 노출됨.
    public float moveSpeed = 10.0f;     // 이동 속력 변수 (public으로 선언돼 인스펙터 뷰에 노출됨)
    public float turnSpeed = 80.0f;     // 회전 속도 변수 

    private Vector3 moveDir;    // Vector3 를 moveDir 변수에 할당.
    private Transform tr;       // 컴포넌트를 캐시 처리할 변수. Transform 은 클래스이다.
    private Animation anim;     // Animation 컴포넌트를 저장할 변수

    //6-8 초기 생명 값 #==--
    private readonly float initHp = 100.0f;
    //6-8 현재 생명 값 #==--
    public float currHp;


    //void Start()
    //{
    //    // Transform 컴포넌트를 추출해 변수에 대입(this.gameObject 는 생략가능하다.)
    //    tr = this.GetComponent<Transform>();
    //    anim = this.GetComponent<Animation>();

    //    // 애니메이션 실행
    //    anim.Play("Idle");
    //}

    // =============================================== //

    IEnumerator Start()
    {
        //6-8 HP 초기화 #==--
        currHp = initHp;

        //5-16 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        //5-16 애니메이션 실행
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 1000.0f;
    }

    // ============================================== //


    void Update()
    {
        // float horizontal = 과정을 위에 변수명으로 할당하여 그 과정을 간략화.
        // float vertical = 과정을 위에 변수명으로 할당하여 그 과정을 간략화.
        // float rotate = 과정을 위에 변수명으로 할당하여 그 과정을 간략화.
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f
        rotate = Input.GetAxis("Mouse X");          // 마우스의 이동값을 받아와 구현한다.

        //Debug.Log("호리존탈=" + horizontal);
        //Debug.Log("버티칼=" + vertical);

        // ====01. 로직부터.==== //



        // 전후좌우 이동 방향 벡터 계산
        moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);

        // Translate(이동 방향 * 속도 * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up 축을 기준으로 turnSpeed 만큼의 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rotate, Space.Self);

        // 주인공 캐릭터의 애니메이션 설정

        // 키보드 입력값을 기준으로 동작할 애니메이션 수행        
        PlayerAnim(horizontal, vertical);
    }


    private void PlayerAnim(float horizontal, float vertical)
    {
        // >= 왼쪽 vertical 가(이) 오른쪽 피연산자보다 크거나 같으면
        if (vertical >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);  // 전진 애니메이션 실행

        }   // <= 왼쪽 vertical 가(이) 오른쪽 피연산자보다 작거나 같으면
        else if (vertical <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);  // 후진 애니메이션 실행

        }   // >= 왼쪽 horizontal 가(이) 오른쪽 피연산자보다 크거나 같으면
        else if (horizontal >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);  // 오른쪽 이동 애니메이션 실행

        }   // <= 왼쪽 horizontal 가(이) 오른쪽 피연산자보다 작거나 같으면
        else if (horizontal <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);  // 왼쪽 이동 애니메이션 실행
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);  // 정지 시 Idle 애니메이션 실행
        }
    }

    //6-8 충돌한 Collider의 IsTrigger 옵션이 체크됐을 때 발생 #==--
    private void OnTriggerEnter(Collider coll)
    {
        //6-8 충돌한 Collider가 몬스터의 PUNCH이면 Player의 HP 차감 #==--
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 20.0f;
            Debug.Log($"Player hp = {currHp / initHp}");

            //6-8 Player의 생명이 0 이하이면 사망 처리 #==--
            if (currHp <= 0.0f)
            {               
                PlayerDie();
            }
        }
    }

    //6-8 Player의 사망 처리 #==--
    void PlayerDie()
    {        
        Debug.Log("Player Die !");
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