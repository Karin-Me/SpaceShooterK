using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // ���󰡾� �� ����� ������ ����
    public Transform targetTr;
    // Main Camera �ڽ��� Transform ������Ʈ
    private Transform camTr;

    // ���� ������κ��� ������ �Ÿ�
    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    // Y ������ �̵��� ����
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // ���� �ӵ�
    public float damping = 10.0f;

    // SmoothDamp ���� ����� ����
    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        // Main Camera �ڽ��� Transform ������Ʈ�� ����
        camTr = GetComponent<Transform>();
    }


    void LateUpdate()
    {
        //// 01_����_�ʹ� ����.         

        //// �����ؾ� �� ����� �������� distance ��ŭ �̵�
        //// ���̸� height ��ŭ �̵�
        //camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);
        //// ��ǥ�踦 ���� ī�޶� �ȷο� 
        ////camTr.position = targetTr.position + (-(targetTr.position + new Vector3(0, 0, 1)) * distance) +
        ////(new Vector3(0,1,0) * height);

        //// Camera �� �ǹ� ��ǥ�� ���� ȸ��
        //camTr.LookAt(targetTr.position);



        //// 02_����_Slerp ����.

        //// �����ؾ� �� ����� �������� distance ��ŭ �̵�
        //// ���̸� height ��ŭ �̵�
        //Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        //// ���� ���� ���� �Լ��� ����� �ε巴�� ��ġ�� ����
        //camTr.position = Vector3.Slerp(camTr.position,              // ������ġ
        //                               pos,                         // ��ǥ ��ġ
        //                               Time.deltaTime * damping);   // �ð� t

        //// Camera �� �ǹ� ��ǥ�� ���� ȸ��
        //camTr.LookAt(targetTr.position);



        // 03_����_SmoothDamp ����.        

        // �����ؾ� �� ����� �������� distance ��ŭ �̵�
        // ���̸� height ��ŭ �̵�
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // SmoothDamp ��(��) �̿��� ��ġ ����.

        // camTr.position �� ���� ��ġ
        // pos �� ��ǥ ��ġ
        // ref velocity(current velocity) �� ���� �ӵ�
        // damping �� ��ǥ ��ġ���� ������ �ð�
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);

        // Camera �� �ǹ� ��ǥ�� ���� ȸ��
        camTr.LookAt(targetTr.position);
    }
}
