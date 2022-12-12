using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color color = Color.magenta;  
    public float radius = 0.5f;          

    void OnDrawGizmos()
    {
        // 기즈모 색상 설정
        Gizmos.color = color;
        // 구체 모양의 기즈모 생성. 인자는 (생성 위치, 반지름)
        Gizmos.DrawSphere(transform.position, radius);
    }
}
