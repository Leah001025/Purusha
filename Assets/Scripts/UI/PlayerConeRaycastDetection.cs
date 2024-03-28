using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Cinemachine;
using UnityEngine.UI;

public class PlayerConeRaycastDetection : MonoBehaviour
{
    public float detectionInterval = 0.1f; // ����ĳ��Ʈ�� ������ �ֱ�
    public float detectionAngle = 45.0f; // ������ ����
    private float detectionDistance = 1.5f; // ������ �Ÿ�
    public string BattleScene__; // ���� ���� �̸�
    public LayerMask monsterLayer; // ���� ���̾�
    private Vector3 startPos;
    private Vector3 targetPos;
    private float detectionTimer = 0.0f;
    public CinemachineBrain cinemachineBrain;
    public Image battleEffect;
    private bool isBattle = false;

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
        if (isBattle)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 1f * Time.deltaTime);
            battleEffect.color = Color.Lerp(battleEffect.color, new Color(1, 1, 1, 1), 1f * Time.deltaTime);
        }
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
                EnterBattle(hit);
                //SceneManager.LoadScene("BattleScene__");  ���� ������ ��ȯ
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
    private void EnterBattle(RaycastHit hit)
    {

        isBattle = true;
        targetPos = hit.collider.transform.position;
        cinemachineBrain.enabled = false;
        StartCoroutine(CameraControll());

    }
    private IEnumerator CameraControll()
    {
        yield return new WaitForSeconds(1.5f);
        isBattle = false;
        SceneLoadManager.Instance.ChangeScene("BattleScene__");
    }

    private IEnumerator StartDetect()
    {
        WaitForSeconds detectDelay = new WaitForSeconds(detectionInterval);
        while (true)
        {
            yield return detectDelay;
            if(!isBattle)
            PerformConeRaycast();
        }
    }
}