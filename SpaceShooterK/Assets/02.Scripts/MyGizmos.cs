using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color color = Color.magenta;  
    public float radius = 0.5f;          

    void OnDrawGizmos()
    {
        // ����� ���� ����
        Gizmos.color = color;
        // ��ü ����� ����� ����. ���ڴ� (���� ��ġ, ������)
        Gizmos.DrawSphere(transform.position, radius);
    }
}
