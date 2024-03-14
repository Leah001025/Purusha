using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;

    private bool joystickActive = false;
    private Vector2 touchStart;
    private Vector2 direction;

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    joystickActive = touch.position.x < screenCenterX;
                    touchStart = touch.position;
                    break;
                case TouchPhase.Moved:
                    if (joystickActive)
                        direction = (touch.position - touchStart) / sensitivity;
                    break;
                case TouchPhase.Ended:
                    joystickActive = false;
                    direction = Vector2.zero;
                    break;
            }
        }
        MovePlayer();
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
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z), Vector3.up);
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

        cameraTransform.position = new Vector3(playerTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
    }
}
