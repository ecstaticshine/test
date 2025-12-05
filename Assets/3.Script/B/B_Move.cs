using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class B_Move : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private float jumpPower = 5f;
    private Rigidbody playerRigid;
    private int currentLane = 1;
    private bool isJumping = false;

    private void Awake()
    {
        TryGetComponent(out playerRigid);
    }

    public void OnMove(InputValue value)
    {
        float input = value.Get<float>();

        if (input > 0)
        {
            MoveLane(1);
        }
        else if (input < 0)
        {
            MoveLane(-1);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && !isJumping)
        {
            isJumping = true;

            playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void MoveLane(int direction)
    {
        int targetLane = currentLane + direction;

        if (targetLane >= 0 && targetLane <= lanes.Length && targetLane <= 2)
        {
            currentLane = targetLane;

            Vector3 targetPos = lanes[currentLane].position;

            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }
}
