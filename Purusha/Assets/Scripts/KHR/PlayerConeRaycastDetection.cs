using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConeRaycastDetection : MonoBehaviour
{
    public float detectionInterval = 1.0f; // ����ĳ��Ʈ�� ������ �ֱ�
    public float detectionAngle = 45.0f; // ������ ����
    public float detectionDistance = 1.5f; // ������ �Ÿ�
    public string BattleScene__; // ���� ���� �̸�
    public LayerMask monsterLayer; // ���� ���̾�
    private Vector3 startPos;
    private float detectionTimer = 0.0f;

    private void Start()
    {
        startPos = transform.position + (transform.up * 1f) + (transform.forward * 0.2f);
    }
    void Update()
    {
        detectionTimer += Time.deltaTime; // ��� �ð� ����

        if (detectionTimer >= detectionInterval)
        {
            PerformConeRaycast();
            detectionTimer = 0.0f; // Ÿ�̸� �ʱ�ȭ
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
                Debug.Log("���̿��н��� �����߽��ϴ�. ���̿��н� �̸�: " + hit.collider.name);
                SceneManager.LoadScene("BattleScene__"); // ���� ������ ��ȯ
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
