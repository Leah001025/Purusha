using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerRaycastPlayerDetection : MonoBehaviour
{
    public float interval = 1.0f; // ����ĳ��Ʈ�� ������ �ֱ�
    public string battleSceneName; // ���� ���� �̸�

    private float timer = 0.0f;

    void Start()
    {
        InvokeRepeating("PerformRaycast", 0.0f, interval); // Ÿ�̸Ӹ� �̿��Ͽ� �ֱ������� PerformRaycast �޼��带 ȣ��
    }

    void PerformRaycast()
    {
        // ����ĳ��Ʈ ����
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // ���̰� � ��ü�� �浹���� ���� ó��
            if (hit.collider.CompareTag("")) // �浹�� ��ü�� "Player" �±׸� ������ �ִ��� Ȯ��
            {
                Debug.Log("�÷��̾ �����߽��ϴ�.");
                SceneManager.LoadScene(battleSceneName); // ���� ������ ��ȯ
            }
        }
    }
}

