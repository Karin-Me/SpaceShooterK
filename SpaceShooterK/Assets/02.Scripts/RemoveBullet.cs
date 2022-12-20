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
            // ù ��° �浹 ������ ���� ����
            ContactPoint cp = coll.GetContact(0);

            // �浹�� �Ѿ��� ���� ���͸� ���ʹϾ� Ÿ������ ��ȯ
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            //// ����ũ ��ƼŬ�� �������� ����
            //Instantiate(sparkEffect, coll.transform.position, Quaternion.identity);

            //// ����ũ ��ƼŬ�� �������� ����
            //Instantiate(sparkEffect, cp.point, rot);

            // ����ũ ��ƼŬ�� �������� ����
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            // ���� �ð��� ���� �� ����ũ ��ƼŬ�� ����
            Destroy(spark, 0.5f);

            // �浹�� ���ӿ�����Ʈ ����
            // ù ��° ���ڿ��� ������ ����� �����ϰ� �� ��° ���ڿ����� �ð��� �����ϸ�
            // �ش� �ð��� ���� �� �����ȴ�.(�ð��� �� ������ ������ �����ϴ�.)
            // static void Destroy (Object gameObject, float t);
            // EX -> Destroy (coll.gameObject, 5); [5�� �� ������Ʈ ����]
            Destroy(coll.gameObject);
        }
    }
}
