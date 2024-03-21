using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerRaycastPlayerDetection : MonoBehaviour
{
    public float interval = 1.0f; // 레이캐스트를 수행할 주기
    public string battleSceneName; // 전투 씬의 이름

    private float timer = 0.0f;

    void Start()
    {
        InvokeRepeating("PerformRaycast", 0.0f, interval); // 타이머를 이용하여 주기적으로 PerformRaycast 메서드를 호출
    }

    void PerformRaycast()
    {
        // 레이캐스트 수행
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 레이가 어떤 물체와 충돌했을 때의 처리
            if (hit.collider.CompareTag("")) // 충돌한 물체가 "Player" 태그를 가지고 있는지 확인
            {
                Debug.Log("플레이어를 감지했습니다.");
                SceneManager.LoadScene(battleSceneName); // 전투 씬으로 전환
            }
        }
    }
}

