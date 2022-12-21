using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //5-9 ���� ȿ�� ��ƼŬ�� ������ ����
    public GameObject expEffect;

    //5-10 �������� ������ �ؽ�ó �迭 -
    public Texture[] textures;

    //5-11 ���� �ݰ� --
    public float radius = 10.0f;

    //5-10 ������ �ִ� Mesh Renderer ������Ʈ�� ������ ���� -
    private new MeshRenderer renderer;

    //5-9 ������Ʈ�� ������ ����
    private Transform tr;
    private Rigidbody rb;

    //5-9 �Ѿ� ���� Ƚ����  ������ų ����
    private int hitCount = 0;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        //5-10 ������ �ִ� MeshRenderer ������Ʈ�� ���� -
        renderer = GetComponentInChildren<MeshRenderer>();

        //5-10 ���� �߻� -
        int idx = Random.Range(0, textures.Length);
        //5-10 �ؽ�ó ���� -
        renderer.material.mainTexture = textures[idx];
    }


    //5-9 �浹 �� �߻��ϴ� �ݹ� �Լ�
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //5-9 �Ѿ� ���� Ƚ���� ������Ű�� 3ȸ �̻��̸� ���� ó��
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }


    //5-9 �巳���� ���߽�ų �Լ�
    void ExpBarrel()
    {
        //5-9 ���� ȿ�� ��ƼŬ ����
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        //5-9 ���� ȿ�� ��ƼŬ 2�� �Ŀ� ����
        Destroy(exp, 2.0f);

        ////5-9 Rigidbody ������Ʈ�� mass �� 1.0 ���� ������ ���Ը� ������ ��
        //rb.mass = 1.0f;
        ////5-9 ���� �ڱ�ġ�� ���� ����
        //rb.AddForce(Vector3.up * 1500.0f);


        //5-11 ���� ���߷� ���� --
        IndirectDamage(tr.position);

        //5-9 3�� �Ŀ� �巳�� ����
        Destroy(gameObject, 3.0f);
    }


    //5-11 ���߷��� �ֺ��� �����ϴ� �Լ� --
    void IndirectDamage(Vector3 pos)
    {
        //5-11 �ֺ��� �ִ� �巳���� ��� ���� --
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        foreach (var coll in colls)
        {
            //5-11 ���� ������ ���Ե� �巳���� Rigidbody ������Ʈ ���� --
            rb = coll.GetComponent<Rigidbody>();
            //5-11 �巳���� ���Ը� ������ �� --
            rb.mass = 1.0f;
            //5-11 freezeRotation ���Ѱ��� ���� --
            rb.constraints = RigidbodyConstraints.None;
            //5-11 ���߷��� ���� --
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
        }
    }
}
