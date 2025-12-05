using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Move : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private float jumpPower = 5f;
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    private int currentLane = 1;
    private bool isJumping = false;

    private void Awake()
    {
        TryGetComponent(out playerRigid);
        TryGetComponent(out playerAnimator);
        //AudioManager.Instance.PlayBGM("BGM_Kart"); // BGM을 보스 테마로 변경
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
        if(value.isPressed && !isJumping)
        {
            isJumping = true;

            playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            playerAnimator.SetBool("IsJump", true);
        }
        //AudioManager.Instance.PlaySFX("SFX_Jump");
    }

    private void MoveLane(int direction)
    {
        int targetLane = currentLane + direction;

        if(targetLane >= 0 && targetLane <= lanes.Length && targetLane <= 2)
        {
            currentLane = targetLane;

            Vector3 targetPos = lanes[currentLane].position;

            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;

        playerAnimator.SetBool("IsJump", false);
    }

    private IEnumerator MoveAnimation_co(string para_name)
    {
        playerAnimator.SetBool(para_name, true);

        yield return new WaitForSeconds(0.05f);

        playerAnimator.SetBool(para_name, false);
    }

}
