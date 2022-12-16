using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // ����ũ ��ƼŬ �������� ������ ����
    public GameObject sparkEffect;

    // �浹�� ������ �� �߻��ϴ� �̺�Ʈ
    void OnCollisionEnter(Collision coll)
    {
        //// �浹�� ���ӿ�����Ʈ�� �±װ� ��
        //if (coll.collider.tag == "BULLET")
        if (coll.collider.CompareTag("BULLET"))
        {
            // ����ũ ��ƼŬ�� �������� ����
            Instantiate(sparkEffect, coll.transform.position, Quaternion.identity);

            // �浹�� ���ӿ�����Ʈ ����
            // ù ��° ���ڿ��� ������ ����� �����ϰ� �� ��° ���ڿ����� �ð��� �����ϸ�
            // �ش� �ð��� ���� �� �����ȴ�.(�ð��� �� ������ ������ �����ϴ�.)
            // static void Destroy (Object gameObject, float t);
            // EX -> Destroy (coll.gameObject, 5); [5�� �� ������Ʈ ����]
            Destroy(coll.gameObject);
        }
    }
}
