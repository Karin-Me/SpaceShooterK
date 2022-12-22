using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //5-6 스파크 파티클 프리팹을 연결할 변수
    public GameObject sparkEffect;

    //5-2 충돌이 시작할 때 발생하는 이벤트 ===
    void OnCollisionEnter(Collision coll)
    {
        ////5-2 충돌한 게임오브젝트의 태그값 비교 ===
        //if (coll.collider.tag == "BULLET")

        //5-3 충돌한 게임오브젝트의 태그값 비교 -
        if (coll.collider.CompareTag("BULLET"))
        {
            //5-7 첫 번째 충돌 지점의 정보 추출
            ContactPoint cp = coll.GetContact(0);

            //5-7 충돌한 총알의 법선 백터를 쿼터니언 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            ////5-6 스파크 파티클을 동적으로 생성
            //Instantiate(sparkEffect, coll.transform.position, Quaternion.identity);

            ////5-7 스파크 파티클을 동적으로 생성
            //Instantiate(sparkEffect, cp.point, rot);

            //5-8 스파크 파티클을 동적으로 생성
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            //5-8 일정 시간이 지난 후 스파크 파티클을 삭제
            Destroy(spark, 0.5f);


            //5-2 충돌한 게임오브젝트 삭제 ===
            // 첫 번째 인자에는 제거할 대상을 지정하고 두 번째 인자에서는 시간을 지정하면
            // 해당 시간이 지난 후 삭제된다.(시간은 초 단위로 지정이 가능하다.)
            // static void Destroy (Object gameObject, float t);
            // EX -> Destroy (coll.gameObject, 5); [5초 뒤 오브젝트 삭제]
            Destroy(coll.gameObject);
        }
    }
}
