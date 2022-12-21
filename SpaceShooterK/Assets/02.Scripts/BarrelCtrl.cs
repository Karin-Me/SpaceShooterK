using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //5-9 폭발 효과 파티클을 연결할 변수
    public GameObject expEffect;

    //5-10 무작위로 적용할 텍스처 배열 -
    public Texture[] textures;

    //5-11 폭발 반경 --
    public float radius = 10.0f;

    //5-10 하위에 있는 Mesh Renderer 컴포넌트를 저장할 변수 -
    private new MeshRenderer renderer;

    //5-9 컴포넌트를 저장할 변수
    private Transform tr;
    private Rigidbody rb;

    //5-9 총알 맞은 횟수를  누적시킬 변수
    private int hitCount = 0;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        //5-10 하위에 있는 MeshRenderer 컴포넌트를 추출 -
        renderer = GetComponentInChildren<MeshRenderer>();

        //5-10 난수 발생 -
        int idx = Random.Range(0, textures.Length);
        //5-10 텍스처 지정 -
        renderer.material.mainTexture = textures[idx];
    }


    //5-9 충돌 시 발생하는 콜백 함수
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //5-9 총알 맞은 횟수를 증가시키고 3회 이상이면 폭발 처리
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }


    //5-9 드럼통을 폭발시킬 함수
    void ExpBarrel()
    {
        //5-9 폭발 효과 파티클 생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        //5-9 폭발 효과 파티클 2초 후에 제거
        Destroy(exp, 2.0f);

        ////5-9 Rigidbody 컴포넌트의 mass 를 1.0 으로 수정해 무게를 가볍게 함
        //rb.mass = 1.0f;
        ////5-9 위로 솟구치는 힘을 가함
        //rb.AddForce(Vector3.up * 1500.0f);


        //5-11 간접 폭발력 전달 --
        IndirectDamage(tr.position);

        //5-9 3초 후에 드럼통 제거
        Destroy(gameObject, 3.0f);
    }


    //5-11 폭발력을 주변에 전달하는 함수 --
    void IndirectDamage(Vector3 pos)
    {
        //5-11 주변에 있는 드럼통을 모두 추출 --
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        foreach (var coll in colls)
        {
            //5-11 폭발 범위에 포함된 드럼통의 Rigidbody 컴포넌트 추출 --
            rb = coll.GetComponent<Rigidbody>();
            //5-11 드럼통의 무게를 가볍게 함 --
            rb.mass = 1.0f;
            //5-11 freezeRotation 제한값을 해제 --
            rb.constraints = RigidbodyConstraints.None;
            //5-11 폭발력을 전달 --
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
        }
    }
}
