using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConeRaycastDetection : MonoBehaviour
{
    public float detectionInterval = 1.0f; // 레이캐스트를 수행할 주기
    public float detectionAngle = 45.0f; // 레이의 각도
    public float detectionDistance = 1.5f; // 레이의 거리
    public string BattleScene__; // 전투 씬의 이름
    public LayerMask monsterLayer; // 몬스터 레이어
    private Vector3 startPos;
    private float detectionTimer = 0.0f;

    private void Start()
    {
        startPos = transform.position + (transform.up * 1f) + (transform.forward * 0.2f);
    }
    void Update()
    {
        detectionTimer += Time.deltaTime; // 경과 시간 누적

        if (detectionTimer >= detectionInterval)
        {
            PerformConeRaycast();
            detectionTimer = 0.0f; // 타이머 초기화
        }
    }

    void PerformConeRaycast()
    {
        Vector3 forward = transform.forward;
        RaycastHit[] hits = Physics.SphereCastAll(startPos, detectionDistance, forward, detectionAngle, monsterLayer);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("바이오닉스를 감지했습니다. 바이오닉스 이름: " + hit.collider.name);
                SceneManager.LoadScene("BattleScene__"); // 전투 씬으로 전환
                return;
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 forward = transform.forward;
    //    Quaternion spreadRotation = Quaternion.AngleAxis(-detectionAngle, Vector3.up);
    //    Vector3 leftSpread = spreadRotation * forward;
    //    spreadRotation = Quaternion.AngleAxis(detectionAngle, Vector3.up);
    //    Vector3 rightSpread = spreadRotation * forward;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(startPos, forward * detectionDistance);
    //    Gizmos.DrawRay(startPos, leftSpread * detectionDistance);
    //    Gizmos.DrawRay(startPos, rightSpread * detectionDistance);
    //}
}
