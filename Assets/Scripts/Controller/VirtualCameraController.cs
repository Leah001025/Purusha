using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualCameraController : MonoBehaviour
{
    public Transform v_CameraTarget;
    public Transform vCamera;
    public Transform playerTransform;
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineInputProvider InputProvider;
    CinemachineBrain brain;
    public bool isVirtualCameraOn;
    private bool isClickUI;
    Vector3 playerFoward;
    Vector3 cameraFoward;
    Vector3 crossProduct;
    Vector3 targetCameraPos;
    Vector3 cameraRot;
    Quaternion targetCameraRot;
    int crossproductNum;
    float dotProduct;
    float angle;
    float rad;

    private void Awake()
    {
        v_CameraTarget = GetComponent<Transform>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        brain = GetComponent<CinemachineBrain>();
        isVirtualCameraOn = false;

    }

    private void FixedUpdate()
    {
        IsClickUI();
        if (Input.GetMouseButton(1))
        {
            if (!isClickUI)
            {
                InputProvider.enabled = true;
                isVirtualCameraOn = true;
            }
            else
            {
                InputProvider.enabled = false;
                isVirtualCameraOn = false;
            }
        }
        else
        {
            InputProvider.enabled = false;
            isVirtualCameraOn = false;
        }
    }
    public void MoveCameraBackward()
    {
        if (!isVirtualCameraOn)
        {

            playerFoward = playerTransform.forward;
            cameraFoward = playerTransform.position - transform.position;
            cameraFoward.y = 0;
            playerFoward.y = 0;
            rad = cameraFoward.magnitude;
            crossProduct = Vector3.Cross(cameraFoward.normalized, playerFoward.normalized);
            dotProduct = Vector3.Dot(cameraFoward.normalized, playerFoward.normalized);
            if (dotProduct < 0)
                angle = Vector3.Angle(cameraFoward.normalized, playerFoward.normalized) * (crossProduct.y >= 0 ? 1 : -1);
            //if (dotProduct < 0)
            if (crossProduct.y < 0) crossproductNum = 1; else crossproductNum = -1;
            ChangeLocalPosAndRot(crossproductNum, dotProduct);

        }
    }
    private void ChangeLocalPosAndRot(int num, float dot)
    {
        if (dot > 0.98f) angle = 0; else angle = angle + 2 * num;
        targetCameraPos.x = rad * Mathf.Sin(angle * Mathf.Deg2Rad);
        targetCameraPos.y = transform.localPosition.y;
        targetCameraPos.z = -rad * Mathf.Cos(angle * Mathf.Deg2Rad);
        cameraRot = transform.localRotation.eulerAngles;
        targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
        transform.SetLocalPositionAndRotation(targetCameraPos, targetCameraRot);
    }
    private void IsClickUI()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            EventSystem eventSystem = EventSystem.current;
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(eventData, results);

            if (results.Count > 0 && results[0].gameObject != null && results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                Debug.Log("UI가 클릭되었습니다.");
                isClickUI = true;
            }
            else
            {
                Debug.Log("UI가 아닌 다른 오브젝트가 클릭되었습니다.");
                isClickUI = false;
            }
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            isClickUI = false;
        }
    }
}
