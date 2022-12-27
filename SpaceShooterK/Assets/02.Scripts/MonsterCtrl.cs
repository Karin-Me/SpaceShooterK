using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//6-1 내비게이션 기능을 사용하기 위해 추가해야 하는 네임스페이스
using UnityEngine.AI;


public class MonsterCtrl : MonoBehaviour
{
    //6-2 몬스터의 상태 정보 --
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    //6-2 몬스터의 현재 상태 --
    public State state = State.IDLE;
    //6-2 추적 사정거리 --
    public float traceDist = 10.0f;
    //6-2 공격 사정거리 --
    public float attackDist = 2.0f;
    //6-2 몬스터의 사망 여부 --
    public bool isDie = false;


    //6-1 컴포넌트의 캐시를 처리할 변수
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    //6-4 Animator 변수 +=--
    private Animator anim;

    //6-5 Animator 파라미터의 해시값 추출 +==--
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    // 6-6 몬스터 피격 해시테이블 !==--
    private readonly int hashHit = Animator.StringToHash("Hit");    // !==--
    //6-7 혈흔 효과 프리팹 @==--
    private GameObject bloodEffect;

    void Start()
    {
        //6-1 몬스터의 Transform 할당
        monsterTr = GetComponent<Transform>();

        //6-1 추적 대상인 Player의 Transform 할당
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //6-1 NavMeshAgent 컴포넌트 할당
        agent = GetComponent<NavMeshAgent>();

        //6-4 Animator 컴포넌트 할당 +=--
        anim = GetComponent<Animator>();

        //6-7 BloodSprayEffect 프리팹 로드 @==--
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

            ////6-1 추적 대상의 위치를 설정하면 바로 추적 시작
            //agent.destination = playerTr.position;

        //6-2 몬스터의 상태를 체크하는 코루틴 함수 호출 --
        StartCoroutine(CheckMonsterState());
        //6-3 상태에 따라 몬스터의 행동을 수행하는 코루틴 함수 호출 =--
        StartCoroutine(MonsterAction());
    }


    //6-2 일정한 간격으로 몬스터의 행동 상태를 체크 --
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //6-2_ 0.3초 동안 중지(대기)하는 동안 제어권을 메시지 루프에 양보 --
            yield return new WaitForSeconds(0.3f);

            //6-2 몬스터와 주인공 캐릭터 사이의 거리 측정 --
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //6-2 공격 사정거리 범위로 들어왔는지 확인 --
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }

            //6-2 추적 사정거리 범위로 들어왔는지 확인 --
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }


    //6-3 몬스터의 상태에 따라 몬스터의 동작을 수행 =--
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                //6-3 IDLE 상태 =--
                case State.IDLE:
                    //6-3 추적 중지 =--
                    agent.isStopped = true;

                    // 6-5 Animator의 IsTrace 변수를 해시테이블로 변경 +==--
                    anim.SetBool(hashTrace, false);
                    break;

                //// 6-4 Animator의 IsTrace 변수를 false로 설정 +=--
                //anim.SetBool("IsTrace", false);
                //break;

                //6-3 추적 상태 =--
                case State.TRACE:
                    //6-3 추적 대상의 좌표로 이동 시작 =--
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    // 6-5 Animator의 IsTrace 변수를 해시테이블 true로 설정 +==--
                    anim.SetBool(hashTrace, true);


                    // 6-5 Animator의 IsAttack 변수를 해시테이블 false로 설정 +==--
                    anim.SetBool(hashAttack, false);
                    break;

                //// 6-4 Animator의 IsTrace 변수를 true로 설정 +=--
                //anim.SetBool("IsTrace", true);
                //break;

                //6-3 공격 상태 =--
                case State.ATTACK:
                    //6-5 Animator의 IsAttack 변수를 true로 설정 +==--
                    anim.SetBool(hashAttack, true);
                    break;

                //6-3 사망 =--
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    //6-6 몬스터의 Bullet 충돌 액션 !==--
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //6-6 충돌한 총알을 삭제 !==--
            Destroy(coll.gameObject);
            //6-6 피격 리액션 애니메이션 실행 !==--
            anim.SetTrigger(hashHit);

            //6-7 총알의 충돌 지점 @==--
            Vector3 pos = coll.GetContact(0).point;
            //6-7 총알의 충돌 지점의 법선 벡터 @==--
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            //6-7 혈흔 효과를 생성하는 함수 호출 @==--
            ShowBloodEffect(pos, rot);
        }
    }

    //6-7 혈흔 효과를 생성하는 함수 @==--
    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        //6-7 혈흔 효과 생성 @==--
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTr);
        Destroy(blood, 1.0f);
    }
    // 몬스터의 콜라이더 함수 충돌 테스트
    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }


    void OnDrawGizmos()
    {
        //6-2 추적 사정거리 표시 --
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        //6-2 공격 사정거리 표시 --
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}
