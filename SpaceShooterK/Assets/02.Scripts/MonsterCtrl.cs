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

    //6-5 Animator �Ķ������ �ؽð� ���� +==--
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    // 6-6 ���� �ǰ� �ؽ����̺� !==--
    private readonly int hashHit = Animator.StringToHash("Hit");    // !==--
    //6-10 PlayerDie �ִϸ����� �ؽð� �߰� %==--
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");

    //6-7 ���� ȿ�� ������ @==--
    private GameObject bloodEffect;

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

        //6-7 BloodSprayEffect ������ �ε� @==--
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

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

                    // 6-5 Animator�� IsTrace ������ �ؽ����̺�� ���� +==--
                    anim.SetBool(hashTrace, false);
                    break;

                //// 6-4 Animator�� IsTrace ������ false�� ���� +=--
                //anim.SetBool("IsTrace", false);
                //break;

                //6-3 ���� ���� =--
                case State.TRACE:
                    //6-3 ���� ����� ��ǥ�� �̵� ���� =--
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    // 6-5 Animator�� IsTrace ������ �ؽ����̺� true�� ���� +==--
                    anim.SetBool(hashTrace, true);


                    // 6-5 Animator�� IsAttack ������ �ؽ����̺� false�� ���� +==--
                    anim.SetBool(hashAttack, false);
                    break;

                //// 6-4 Animator�� IsTrace ������ true�� ���� +=--
                //anim.SetBool("IsTrace", true);
                //break;

                //6-3 ���� ���� =--
                case State.ATTACK:
                    //6-5 Animator�� IsAttack ������ true�� ���� +==--
                    anim.SetBool(hashAttack, true);
                    break;

                //6-3 ��� =--
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    //6-6 ������ Bullet �浹 �׼� !==--
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //6-6 �浹�� �Ѿ��� ���� !==--
            Destroy(coll.gameObject);
            //6-6 �ǰ� ���׼� �ִϸ��̼� ���� !==--
            anim.SetTrigger(hashHit);

            //6-7 �Ѿ��� �浹 ���� @==--
            Vector3 pos = coll.GetContact(0).point;
            //6-7 �Ѿ��� �浹 ������ ���� ���� @==--
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            //6-7 ���� ȿ���� �����ϴ� �Լ� ȣ�� @==--
            ShowBloodEffect(pos, rot);
        }
    }

    //6-7 ���� ȿ���� �����ϴ� �Լ� @==--
    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        //6-7 ���� ȿ�� ���� @==--
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTr);
        Destroy(blood, 1.0f);
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

    //6-8 ������ �ݶ��̴� �Լ� �浹 �׽�Ʈ
    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

    void OnPlayerDie()
    {
        //6-10 ������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ��� ��� ������Ŵ %==--
        StopAllCoroutines();

        //6-10 ������ �����ϰ� �ִϸ��̼��� ���� %==--
        agent.isStopped = true;
        anim.SetTrigger(hashPlayerDie);
    }
}
