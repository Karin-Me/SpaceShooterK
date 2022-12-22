using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//5-12 반드시 필요한 컴포넌트를 명시해 해당 컴포넌트가 삭제되는 것을 방지하는 어트리뷰트 --
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // 총알 프리팹
    public GameObject bullet;
    // 총알 발사 좌표
    public Transform firePos;
    //5-12 총소리에 사용할 오디오 음원 --
    public AudioClip fireSfx;

    //5-12 AudioSource 컴포넌트를 저장할 변수 --
    private new AudioSource audio;
    //5-13 Muzzle Flash 의 MeshRenderer 컴포넌트 ---
    private MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        //5-13 FirePos 하위에 있는 MuzzleFlash 의 Material 컴포넌트를 추출 ---
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        //5-13 처음 시작할 때 비활성화 ---

        muzzleFlash.enabled = false;
    }


    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        // (Project Settings 에서 Input Manager -> Fire1 탭을 보면
        // Alt Positive Button 이 mouse 0 으로 할당이 되어있다.)
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet 프리팹을 동적으로 생성(생성할 객체, 위치, 회전)
        Instantiate(bullet, firePos.position, firePos.rotation);
        //5-12 총소리 발생 --
        audio.PlayOneShot(fireSfx, 1.0f);
    }
}
