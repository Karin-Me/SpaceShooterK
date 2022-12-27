using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // ĳ�� ó���� ����
    private float horizontal;   // ���� �̵� ����
    private float vertical;     // ���� �̵� ����
    private float rotate;       // ȸ�� �ӵ� ����

    // public ���� ����Ǿ� Inspector View �� �����.
    public float moveSpeed = 10.0f;     // �̵� �ӷ� ���� (public���� ����� �ν����� �信 �����)
    public float turnSpeed = 80.0f;     // ȸ�� �ӵ� ���� 

    private Vector3 moveDir;    // Vector3 �� moveDir ������ �Ҵ�.
    private Transform tr;       // ������Ʈ�� ĳ�� ó���� ����. Transform �� Ŭ�����̴�.
    private Animation anim;     // Animation ������Ʈ�� ������ ����

    //6-8 �ʱ� ���� �� #==--
    private readonly float initHp = 100.0f;
    //6-8 ���� ���� �� #==--
    public float currHp;


    //void Start()
    //{
    //    // Transform ������Ʈ�� ������ ������ ����(this.gameObject �� ���������ϴ�.)
    //    tr = this.GetComponent<Transform>();
    //    anim = this.GetComponent<Animation>();

    //    // �ִϸ��̼� ����
    //    anim.Play("Idle");
    //}

    // =============================================== //

    IEnumerator Start()
    {
        //6-8 HP �ʱ�ȭ #==--
        currHp = initHp;

        //5-16 ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        //5-16 �ִϸ��̼� ����
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 1000.0f;
    }

    // ============================================== //


    void Update()
    {
        // float horizontal = ������ ���� ���������� �Ҵ��Ͽ� �� ������ ����ȭ.
        // float vertical = ������ ���� ���������� �Ҵ��Ͽ� �� ������ ����ȭ.
        // float rotate = ������ ���� ���������� �Ҵ��Ͽ� �� ������ ����ȭ.
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f
        rotate = Input.GetAxis("Mouse X");          // ���콺�� �̵����� �޾ƿ� �����Ѵ�.

        //Debug.Log("ȣ����Ż=" + horizontal);
        //Debug.Log("��ƼĮ=" + vertical);

        // ====01. ��������.==== //



        // �����¿� �̵� ���� ���� ���
        moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);

        // Translate(�̵� ���� * �ӵ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up ���� �������� turnSpeed ��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rotate, Space.Self);

        // ���ΰ� ĳ������ �ִϸ��̼� ����

        // Ű���� �Է°��� �������� ������ �ִϸ��̼� ����        
        PlayerAnim(horizontal, vertical);
    }


    private void PlayerAnim(float horizontal, float vertical)
    {
        // >= ���� vertical ��(��) ������ �ǿ����ں��� ũ�ų� ������
        if (vertical >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);  // ���� �ִϸ��̼� ����

        }   // <= ���� vertical ��(��) ������ �ǿ����ں��� �۰ų� ������
        else if (vertical <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);  // ���� �ִϸ��̼� ����

        }   // >= ���� horizontal ��(��) ������ �ǿ����ں��� ũ�ų� ������
        else if (horizontal >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);  // ������ �̵� �ִϸ��̼� ����

        }   // <= ���� horizontal ��(��) ������ �ǿ����ں��� �۰ų� ������
        else if (horizontal <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);  // ���� �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);  // ���� �� Idle �ִϸ��̼� ����
        }
    }

    //6-8 �浹�� Collider�� IsTrigger �ɼ��� üũ���� �� �߻� #==--
    private void OnTriggerEnter(Collider coll)
    {
        //6-8 �浹�� Collider�� ������ PUNCH�̸� Player�� HP ���� #==--
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 20.0f;
            Debug.Log($"Player hp = {currHp / initHp}");

            //6-8 Player�� ������ 0 �����̸� ��� ó�� #==--
            if (currHp <= 0.0f)
            {               
                PlayerDie();
            }
        }
    }

    //6-8 Player�� ��� ó�� #==--
    void PlayerDie()
    {        
        Debug.Log("Player Die !");
    }
}




////    01. Transform ������Ʈ�� ��ġ�� ����
//// [������Ʈ.�Ӽ�(������Ƽ)]/ [+= ���� ���� ������] / [������ ��]
//transform.position += new Vector3(0, 0, 1);

//// ����ȭ ���͸� ����� �ڵ�
//transform.position += Vector3.forward * 1;

//// Transform ������Ʈ�� ������ ������ ������ ���� tr �� transform ������Ʈ�� ����Ͽ� �����Ѵ�.
//tr.position += Vector3.forward * 1;

//// Translate �Լ��� ����� �̵� ����
////  Space.Self �� ���� ������ �⺻������ ������ǥ���̴�.// Space.Self �� ���������ϴ�.
//// tr.Translate(Vector3.forward * 1); >> ������ǥ��� �̵��ϴ� ����
//// tr.Translate(Vector3.forward * 1, Space.World); >> ������ǥ��� �̵��ϴ� ����
//tr.Translate(Vector3.forward * 1, Space.Self);

//// translate �Լ��� ����� �̵� ������ time.deltatime �� ���� ����
//// vertical �� Ű���� �Է°��� w �� s �׸��� ����Ű up �� down �̴�.
//tr.Translate(Vector3.forward * Time.deltaTime * vertical * moveSpeed, Space.Self);
//// Translate �Լ��� ����� �̵� ������ Time.deltaTime �� ���� ����
//// horizontal �� Ű���� �Է°��� a �� d �׸��� ����Ű left �� right �̴�.
//tr.Translate(Vector3.right * Time.deltaTime * horizontal * moveSpeed, Space.Self);