using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerConeRaycastDetection : MonoBehaviour
{
    public float detectionInterval = 0.1f; // ����ĳ��Ʈ�� ������ �ֱ�
    public float detectionAngle = 45.0f; // ������ ����
    private float detectionDistance = 1.5f; // ������ �Ÿ�
    public string BattleScene__; // ���� ���� �̸�
    public LayerMask monsterLayer; // ���� ���̾�
    private Vector3 startPos;
    private float detectionTimer = 0.0f;

    private void Start()
    {
        startPos = transform.position + (transform.up * 1f) + (transform.forward * 0.2f);
        StartCoroutine(StartDetect());
    }
    void Update()
    {
        //detectionTimer += Time.deltaTime; // ��� �ð� ����

        //if (detectionTimer >= detectionInterval)
        //{
        //    //PerformConeRaycast();
        //    detectionTimer = 0.0f; // Ÿ�̸� �ʱ�ȭ
        //}
    }

    void PerformConeRaycast()
    {
        startPos = transform.position + (transform.up * 1f) + (transform.forward * 0.2f);
        Vector3 forward = transform.forward;
        Quaternion spreadRotation = Quaternion.AngleAxis(-detectionAngle, Vector3.up);
        Vector3 leftSpread = spreadRotation * forward;
        spreadRotation = Quaternion.AngleAxis(detectionAngle, Vector3.up);
        Vector3 rightSpread = spreadRotation * forward;
        Ray[] rays = new Ray[3];
        rays[0] = new Ray(startPos, forward * detectionDistance);
        rays[1] = new Ray(startPos, leftSpread * detectionDistance);
        rays[2] = new Ray(startPos, rightSpread * detectionDistance);
        RaycastHit hit;
        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, out hit, detectionDistance, monsterLayer))
            {
                Debug.Log("���̿��н��� �����߽��ϴ�. ���̿��н� �̸�: " + hit.collider.name);
                SceneManager.LoadScene("BattleScene__"); // ���� ������ ��ȯ
                return;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 forward = transform.forward;
        Quaternion spreadRotation = Quaternion.AngleAxis(-detectionAngle, Vector3.up);
        Vector3 leftSpread = spreadRotation * forward;
        spreadRotation = Quaternion.AngleAxis(detectionAngle, Vector3.up);
        Vector3 rightSpread = spreadRotation * forward;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startPos, forward * detectionDistance);
        Gizmos.DrawRay(startPos, leftSpread * detectionDistance);
        Gizmos.DrawRay(startPos, rightSpread * detectionDistance);

    }

    private IEnumerator StartDetect()
    {
        WaitForSeconds detectDelay = new WaitForSeconds(detectionInterval);
        while (true)
        {
            yield return detectDelay;
            PerformConeRaycast();
        }
    }
}
