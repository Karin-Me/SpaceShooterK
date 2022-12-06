using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float horizontal, vertical;

    // ������Ʈ�� ĳ�� ó���� ����
    private Transform tr;

    void Start()
    {
        // Transform ������Ʈ�� ������ ������ ����(this.gameObject �� ���������ϴ�.)
        tr = this.GetComponent<Transform>();
    }


    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical   = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log("horizontal=" + horizontal);
        Debug.Log("vertical=" + vertical);


        //// Transform ������Ʈ�� ��ġ�� ����
        //// [������Ʈ.�Ӽ�(������Ƽ)]/ [+= ���� ���� ������] / [������ ��]
        //transform.position += new Vector3(0, 0, 1);

        //// Transform ������Ʈ�� ������ ������ ������ ���� tr �� transform ����Ͽ� �����Ѵ�.
        //tr.position += Vector3.forward * 1;

        // Translate �Լ��� ����� �̵� ����
        // forward * 1 �ڿ� ���ڰ� ���� ������ �⺻������ ������ǥ���̴�.
        // tr.Translate(Vector3.forward * 1); >> ������ǥ��� �̵��ϴ� ����
        // tr.Translate(Vector3.forward * 1, Space.World); >> ������ǥ��� �̵��ϴ� ����
        tr.Translate(Vector3.forward * 1, Space.Self);  
    }
}
