using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerConeRaycastDetection : MonoBehaviour
{
    public float detectionInterval = 0.1f; // 레이캐스트를 수행할 주기
    public float detectionAngle = 15.0f; // 레이의 각도
    private float detectionDistance = 1.5f; // 레이의 거리
    public string BattleScene__; // 전투 씬의 이름
    public LayerMask monsterLayer; // 몬스터 레이어
    private Vector3 startPos;
    private Vector3 startPos2;
    private Vector3 targetPos;
    private float detectionTimer = 0.0f;
    public CinemachineBrain cinemachineBrain;
    public Image battleEffect;
    private bool isBattle = false;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        startPos = transform.position + (transform.up * 1f) + (transform.forward * 0.2f);
    }
    private void FixedUpdate()
    {
        if (isBattle)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 2f * Time.deltaTime);
            battleEffect.color = Color.Lerp(battleEffect.color, new Color(1, 1, 1, 1), 2f * Time.deltaTime);
        }
    }
    public void OnMouseButtonDown()
    {
        //detectionTimer += Time.deltaTime; // 경과 시간 누적

        //if (detectionTimer >= detectionInterval)
        //{
        //    //PerformConeRaycast();
        //    detectionTimer = 0.0f; // 타이머 초기화
        //}
        if (!isBattle)
        {

            player.ActionController.WorldAttack();
            PerformConeRaycast();

        }

    }

    void PerformConeRaycast()
    {

        startPos = transform.position + (transform.up * 1.5f) + (transform.forward * 0.2f);
        RaycastHit[] hits = Physics.SphereCastAll(startPos, 1f, transform.forward, 1.5f, monsterLayer);
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                SpawnManager.Instance.battleCanvas.gameObject.SetActive(true);
                Debug.Log("Wave : " + hit.collider.gameObject.transform.parent.name);
                EnterBattle(hit);
                return;
            }
        }
        //startPos2 = transform.position + (transform.up * 2f) + (transform.forward * 0.2f);
        //Vector3 forward = transform.forward;
        //Quaternion spreadRotation = Quaternion.AngleAxis(-detectionAngle, Vector3.up);
        //Vector3 leftSpread = spreadRotation * forward;
        //spreadRotation = Quaternion.AngleAxis(detectionAngle, Vector3.up);
        //Vector3 rightSpread = spreadRotation * forward;
        //Ray[] rays = new Ray[3];
        //RaycastHit hit;
        //rays[0] = new Ray(startPos, forward * detectionDistance);
        //rays[1] = new Ray(startPos, leftSpread * detectionDistance);
        //rays[2] = new Ray(startPos, rightSpread * detectionDistance);
        //foreach (var ray in rays)
        //{
        //    if (Physics.Raycast(ray, out hit, detectionDistance, monsterLayer))
        //    {
        //        SpawnManager.Instance.battleCanvas.gameObject.SetActive(true);
        //        Debug.Log("Wave : " + hit.collider.gameObject.transform.parent.name);
        //        EnterBattle(hit);
        //        return;
        //    }
        //}
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
        GameManager.Instance.waveID = int.Parse(hit.collider.gameObject.transform.parent.name);
        StartCoroutine(CameraControll());

    }
    private IEnumerator CameraControll()
    {
        SoundManager.Instance.EffentAudio("BattleStart");
        yield return new WaitForSeconds(1.5f);
        SceneLoadManager.Instance.ChangeScene("BattleScene");
    }
}
