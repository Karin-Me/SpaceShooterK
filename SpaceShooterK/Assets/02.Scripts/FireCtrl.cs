using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//5-12 �ݵ�� �ʿ��� ������Ʈ�� ����� �ش� ������Ʈ�� �����Ǵ� ���� �����ϴ� ��Ʈ����Ʈ --
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    //5-5 �Ѿ� ������
    public GameObject bullet;
    //5-6 �Ѿ� �߻� ��ǥ
    public Transform firePos;
    //5-12 �ѼҸ��� ����� ����� ���� --
    public AudioClip fireSfx;

    //5-12 AudioSource ������Ʈ�� ������ ���� --
    private new AudioSource audio;
    //5-13 Muzzle Flash �� MeshRenderer ������Ʈ ---
    private MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        //5-13 FirePos ������ �ִ� MuzzleFlash �� Material ������Ʈ�� ���� ---
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        //5-13 ó�� ������ �� ��Ȱ��ȭ ---

        muzzleFlash.enabled = false;
    }


    void Update()
    {
        //5-5 ���콺 ���� ��ư�� Ŭ������ �� Fire �Լ� ȣ��
        // (Project Settings ���� Input Manager -> Fire1 ���� ����
        // Alt Positive Button �� mouse 0 ���� �Ҵ��� �Ǿ��ִ�.)
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        //5-5 Bullet �������� �������� ����(������ ��ü, ��ġ, ȸ��)
        Instantiate(bullet, firePos.position, firePos.rotation);
        //5-12 �ѼҸ� �߻� --
        audio.PlayOneShot(fireSfx, 1.0f);

        //5-14 �ѱ� ȭ�� ȿ�� �ڷ�ƾ �Լ� ȣ�� ----
        StartCoroutine(ShowMuzzleFlash());
    }

    // ================================================================== //

    IEnumerator ShowMuzzleFlash()
    {
        //5-15 ������ ��ǩ���� ���� �Լ��� ����
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //5-15 �ؽ�ó�� ������ �� ����
        muzzleFlash.material.mainTextureOffset = offset;

        //5-15 MuzzleFlash�� ȸ�� ����
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //5-15 MuzzleFlash�� ũ�� ����
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //5-14 MuzzleFlash Ȱ��ȭ ----
        muzzleFlash.enabled = true;

        //5-14_ 0.2�� ���� ���(����)�ϴ� ���� �޽��� ������ ������� �纸 ----
        yield return new WaitForSeconds(0.2f);

        //5-14 MuzzleFlash ��Ȱ��ȭ ----
        muzzleFlash.enabled = false;
    }
    // ================================================================== //
}
