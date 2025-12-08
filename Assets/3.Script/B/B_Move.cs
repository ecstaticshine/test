using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class B_Move : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private float gravityScale = 2.0f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpPower = 5f;
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    private int currentLane = 1;
    private bool isJumping = false;

    private void Awake()
    {
        TryGetComponent(out playerRigid);
        TryGetComponent(out playerAnimator);

        playerRigid.useGravity = false;
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerRigid.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }

    public void OnMove(InputValue value)
    {
        float input = value.Get<float>();

        if (input > 0)
        {
            MoveLane(1);
            StartCoroutine(MoveAnimation_co("MoveRight"));
        }
        else if (input < 0)
        {
            MoveLane(-1);
            StartCoroutine(MoveAnimation_co("MoveLeft"));
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && !isJumping)
        {
            playerAnimator.SetBool("IsJump", true);

            isJumping = true;

            playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void MoveLane(int direction)
    {
        int targetLane = currentLane + direction;

        if (targetLane >= 0 && targetLane < lanes.Length)
        {
            currentLane = targetLane;

            Vector3 targetPos = lanes[currentLane].position;

            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerAnimator.SetBool("IsJump", false);

        isJumping = false;
    }

    private IEnumerator MoveAnimation_co(string para_name)
    {
        playerAnimator.SetBool(para_name, true);

        yield return new WaitForSeconds(0.05f);

        playerAnimator.SetBool(para_name, false);
    }
}