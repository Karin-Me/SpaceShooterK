using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//6-1 ������̼� ����� ����ϱ� ���� �߰��ؾ� �ϴ� ���ӽ����̽�
using UnityEngine.AI;


public class MonsterCtrl : MonoBehaviour
{
    //6-2 ������ ���� ���� --
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    //6-2 ������ ���� ���� --
    public State state = State.IDLE;
    //6-2 ���� �����Ÿ� --
    public float traceDist = 10.0f;
    //6-2 ���� �����Ÿ� --
    public float attackDist = 2.0f;
    //6-2 ������ ��� ���� --
    public bool isDie = false;


    //6-1 ������Ʈ�� ĳ�ø� ó���� ����
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    //6-4 Animator ���� +=--
    private Animator anim;

    void Start()
    {
        //6-1 ������ Transform �Ҵ�
        monsterTr = GetComponent<Transform>();

        //6-1 ���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //6-1 NavMeshAgent ������Ʈ �Ҵ�
        agent = GetComponent<NavMeshAgent>();

        //6-4 Animator ������Ʈ �Ҵ� +=--
        anim = GetComponent<Animator>();

            ////6-1 ���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����
            //agent.destination = playerTr.position;

        //6-2 ������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ȣ�� --
        StartCoroutine(CheckMonsterState());
        //6-3 ���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ�� =--
        StartCoroutine(MonsterAction());
    }


    //6-2 ������ �������� ������ �ൿ ���¸� üũ --
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //6-2_ 0.3�� ���� ����(���)�ϴ� ���� ������� �޽��� ������ �纸 --
            yield return new WaitForSeconds(0.3f);

            //6-2 ���Ϳ� ���ΰ� ĳ���� ������ �Ÿ� ���� --
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //6-2 ���� �����Ÿ� ������ ���Դ��� Ȯ�� --
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }

            //6-2 ���� �����Ÿ� ������ ���Դ��� Ȯ�� --
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


    //6-3 ������ ���¿� ���� ������ ������ ���� =--
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                //6-3 IDLE ���� =--
                case State.IDLE:
                    //6-3 ���� ���� =--
                    agent.isStopped = true;
                    // 6-4 Animator�� IsTrace ������ false�� ���� +=--
                    anim.SetBool("IsTrace", false);
                    break;

                //6-3 ���� ���� =--
                case State.TRACE:
                    //6-3 ���� ����� ��ǥ�� �̵� ���� =--
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    // 6-4 Animator�� IsTrace ������ true�� ���� +=--
                    anim.SetBool("IsTrace", true);
                    break;

                //6-3 ���� ���� =--
                case State.ATTACK:
                    break;

                //6-3 ��� =--
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


    void OnDrawGizmos()
    {
        //6-2 ���� �����Ÿ� ǥ�� --
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        //6-2 ���� �����Ÿ� ǥ�� --
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}


