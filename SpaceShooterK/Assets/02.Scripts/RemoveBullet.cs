using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //5-6 ����ũ ��ƼŬ �������� ������ ����
    public GameObject sparkEffect;

    //5-2 �浹�� ������ �� �߻��ϴ� �̺�Ʈ ===
    void OnCollisionEnter(Collision coll)
    {
        ////5-2 �浹�� ���ӿ�����Ʈ�� �±װ� �� ===
        //if (coll.collider.tag == "BULLET")

        //5-3 �浹�� ���ӿ�����Ʈ�� �±װ� �� -
        if (coll.collider.CompareTag("BULLET"))
        {
            //5-7 ù ��° �浹 ������ ���� ����
            ContactPoint cp = coll.GetContact(0);

            //5-7 �浹�� �Ѿ��� ���� ���͸� ���ʹϾ� Ÿ������ ��ȯ
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            ////5-6 ����ũ ��ƼŬ�� �������� ����
            //Instantiate(sparkEffect, coll.transform.position, Quaternion.identity);

            ////5-7 ����ũ ��ƼŬ�� �������� ����
            //Instantiate(sparkEffect, cp.point, rot);

            //5-8 ����ũ ��ƼŬ�� �������� ����
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            //5-8 ���� �ð��� ���� �� ����ũ ��ƼŬ�� ����
            Destroy(spark, 0.5f);


            //5-2 �浹�� ���ӿ�����Ʈ ���� ===
            // ù ��° ���ڿ��� ������ ����� �����ϰ� �� ��° ���ڿ����� �ð��� �����ϸ�
            // �ش� �ð��� ���� �� �����ȴ�.(�ð��� �� ������ ������ �����ϴ�.)
            // static void Destroy (Object gameObject, float t);
            // EX -> Destroy (coll.gameObject, 5); [5�� �� ������Ʈ ����]
            Destroy(coll.gameObject);
        }
    }
}
