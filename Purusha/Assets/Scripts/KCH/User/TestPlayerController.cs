using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestPlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
    public CinemachineBrain cinemachineBrain;
    public Image battleEffect;
    [SerializeField]public LayerMask Enemy;
    public GameObject worldCanvas;

    private bool joystickActive = false;
    private bool isBattle = false;
    private Vector2 touchStart;
    private Vector2 direction;
    Vector3 targetPos;

    public float minMoveSpeed = 5.0f;
    public float maxMoveSpeed = 15.0f;
    public float rotationSpeed = 5.0f;

    public float sensitivity = 1.0f;

    private Animator animator;

    private float screenCenterX;

    void Start()
    {
        animator = GetComponent<Animator>();
        screenCenterX = Screen.width * 0.5f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isBattle)
        {
        Ray ray = new Ray(transform.position + (transform.up * 1f) + (transform.forward * 0.2f), transform.forward);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 2f, Enemy);
            if (Physics.Raycast(ray, out hit, 2f, Enemy))
            {
                EnterBattle(hit);
            }
        }

        //if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼이 클릭되었는지 확인
        //{
        //    Vector3 mousePosition = Input.mousePosition;

        //    if (mousePosition.x < screenCenterX)
        //    {
        //        joystickActive = true;
        //        touchStart = mousePosition;
        //    }
        //}

        //if (Input.GetMouseButtonUp(0)) // 마우스 왼쪽 버튼이 떼어졌는지 확인
        //{
        //    joystickActive = false;
        //    direction = Vector2.zero;
        //}

        //if (joystickActive && Input.GetMouseButton(0)) // 조이스틱이 활성화되었고 마우스 왼쪽 버튼이 계속 눌린 상태인지 확인
        //{
        //    Vector3 mousePosition = Input.mousePosition;
        //    Vector2 currentMousePos = new Vector2(mousePosition.x, mousePosition.y); // mousePosition을 Vector2로 변환
        //    direction = (currentMousePos - touchStart) / sensitivity;
        //}
        //if(!isBattle)
        //MovePlayer();
        if (isBattle)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 1f * Time.deltaTime);
            battleEffect.color = Color.Lerp(battleEffect.color, new Color(1, 1, 1, 1), 1f * Time.deltaTime);
        }
    }

    void MovePlayer()
    {
        if (joystickActive)
        {
            float distance = Vector2.Distance(touchStart, touchStart + direction);
            float speedFactor = Mathf.Clamp(distance, 0f, 200f) / 200f;

            float moveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, speedFactor);

            Vector3 moveDirection = new Vector3(-direction.x, 0, -direction.y).normalized;
            playerTransform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z), Vector3.forward);
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (moveDirection.magnitude > 0.1f)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        //cameraTransform.position = new Vector3(playerTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
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
        battleEffect.color = new Color(1, 1, 1, 0);
        SceneLoadManager.Instance.ChangeScene("BattleTestTestScene");
        //SceneManager.LoadScene("BattleTestTestScene");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position  + (transform.up * 1f) + (transform.forward * 0.2f), Vector3.forward);
    }
}
