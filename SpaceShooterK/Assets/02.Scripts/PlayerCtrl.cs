using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float horizontal, vertical;


    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");   // -1.0f ~ 0.0f ~ +1.0f
        vertical = Input.GetAxis("Vertical");       // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log("horizontal=" + horizontal);
        Debug.Log("vertical=" + vertical);

        // Transform ������Ʈ�� position �Ӽ����� ����
      // ������Ʈ/�Ӽ�(������Ƽ)/+=���� ���� ������/������ ��
        transform.position += new Vector3(0, 0, 1);
    }
}
