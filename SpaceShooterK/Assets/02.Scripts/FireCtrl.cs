using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//5-12 �ݵ�� �ʿ��� ������Ʈ�� ����� �ش� ������Ʈ�� �����Ǵ� ���� �����ϴ� ��Ʈ����Ʈ --
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // �Ѿ� ������
    public GameObject bullet;
    // �Ѿ� �߻� ��ǥ
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
        // ���콺 ���� ��ư�� Ŭ������ �� Fire �Լ� ȣ��
        // (Project Settings ���� Input Manager -> Fire1 ���� ����
        // Alt Positive Button �� mouse 0 ���� �Ҵ��� �Ǿ��ִ�.)
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet �������� �������� ����(������ ��ü, ��ġ, ȸ��)
        Instantiate(bullet, firePos.position, firePos.rotation);
        //5-12 �ѼҸ� �߻� --
        audio.PlayOneShot(fireSfx, 1.0f);
    }
}
