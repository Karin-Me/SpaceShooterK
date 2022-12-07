using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // ĳ�� ó���� ����
    private float horizontal;   // ���� �̵� ����
    private float vertical;     // ���� �̵� ����
    private float rotate;       // ȸ�� �ӵ� ����
    private Transform tr;       // ������Ʈ�� ĳ�� ó���� ����. Transform �� Ŭ�����̴�.
    private Vector3 moveDir;    // Vector3 �� moveDir ������ �Ҵ�.

    // �̵��ӵ�, ȸ���ӵ� ���� (public ���� ����Ǿ� Inspector View �� �����.)
    public float moveSpeed = 10.0f, turnSpeed = 80.0f;


    void Start()
    {
        // Transform ������Ʈ�� ������ ������ ����(this.gameObject �� ���������ϴ�.)
        tr = this.GetComponent<Transform>();
    }


    void Update()
    {
        // float horizontal = ������ ���� ���������� �Ҵ��Ͽ� �� ������ ����ȭ.
        // float vertical = ������ ���� ���������� �Ҵ��Ͽ� �� ������ ����ȭ.
        // float rotate = ������ ���� 
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f
        rotate = Input.GetAxis("Mouse X");          // ���콺�� �̵����� �޾ƿ� �����Ѵ�.

        //Debug.Log("ȣ����Ż=" + horizontal);
        //Debug.Log("��ƼĮ=" + vertical);

        // 01.�ּ� ����


        // �����¿� �̵� ���� ���� ���
        moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);

        // Translate(�̵� ���� * �ӷ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up ���� �������� turnSpeed ��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rotate, Space.Self);
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