using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // �浹�� ������ �� �߻��ϴ� �̺�Ʈ
    void OnCollisionEnter(Collision coll)
    {
        //// �浹�� ���ӿ�����Ʈ�� �±װ� ��
        //if (coll.collider.tag == "BULLET")
        if (coll.collider.CompareTag("BULLET"))
        {
            // �浹�� ���ӿ�����Ʈ ����
            // ù ��° ���ڿ��� ������ ����� �����ϰ� �� ��° ���ڿ����� �ð��� �����ϸ�
            // �ش� �ð��� ���� �� �����ȴ�.(�ð��� �� ������ ������ �����ϴ�.)
            // static void Destroy (Object gameObject, float t);
            // EX -> Destroy (coll.gameObject, 5); [5�� �� ������Ʈ ����]
            Destroy(coll.gameObject);
        }
    }
}
