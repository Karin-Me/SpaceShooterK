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

        // Transform 컴포넌트의 position 속성값을 변경
      // 컴포넌트/속성(프로퍼티)/+=누적 대입 연산자/저장할 값
        transform.position += new Vector3(0, 0, 1);
    }
}
