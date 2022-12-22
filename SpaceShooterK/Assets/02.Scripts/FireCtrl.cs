using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//5-12 반드시 필요한 컴포넌트를 명시해 해당 컴포넌트가 삭제되는 것을 방지하는 어트리뷰트 --
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    //5-5 총알 프리팹
    public GameObject bullet;
    //5-6 총알 발사 좌표
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
        //5-5 마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        // (Project Settings 에서 Input Manager -> Fire1 탭을 보면
        // Alt Positive Button 이 mouse 0 으로 할당이 되어있다.)
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        //5-5 Bullet 프리팹을 동적으로 생성(생성할 객체, 위치, 회전)
        Instantiate(bullet, firePos.position, firePos.rotation);
        //5-12 총소리 발생 --
        audio.PlayOneShot(fireSfx, 1.0f);

        //5-14 총구 화염 효과 코루틴 함수 호출 ----
        StartCoroutine(ShowMuzzleFlash());
    }

    // ================================================================== //

    IEnumerator ShowMuzzleFlash()
    {
        //5-15 오프셋 좌푯값을 랜덤 함수로 생성
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //5-15 텍스처의 오프셋 값 설정
        muzzleFlash.material.mainTextureOffset = offset;

        //5-15 MuzzleFlash의 회전 변경
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //5-15 MuzzleFlash의 크기 조절
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //5-14 MuzzleFlash 활성화 ----
        muzzleFlash.enabled = true;

        //5-14_ 0.2초 동안 대기(정지)하는 동안 메시지 루프로 제어권을 양보 ----
        yield return new WaitForSeconds(0.2f);

        //5-14 MuzzleFlash 비활성화 ----
        muzzleFlash.enabled = false;
    }
    // ================================================================== //
}
