using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Movement movement;
    private GameObject attackRight;
    private GameObject attackLeft;
    
    private bool Left;
    private bool AttackClick;
    private void Awake()
    {
        movement = GetComponent<Movement>();
        attackRight = transform.Find("AttackRight").gameObject;
        attackLeft = transform.Find("AttackLeft").gameObject;
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        { 
            AttackClick = true;
        }
        if(ctx.canceled) 
            AttackClick = false;
    }

    private void Update()
    {
        var AnimTime = .2f;
        CheckLR();
        if (AttackClick && !Left)
        {
            attackRight.SetActive(true);
            StopGravity(AnimTime);
            StartCoroutine(AttackCd(AnimTime));
        }

        if (AttackClick && Left)
        {
            attackLeft.SetActive(true);
            StopGravity(AnimTime);
            StartCoroutine(AttackCd(AnimTime));
        }
    }

    private void CheckLR()
    {
        var check = gameObject.transform.eulerAngles == new Vector3(0, 0, 0) ? Left = false : Left = true;
    }

    private void StopGravity(float x)
    {
        movement.canMove = false;
        movement.canJump = false;
        movement.rb.velocity = new Vector2(0, 0);
        movement.rb.gravityScale = 0;
    }

    IEnumerator AttackCd(float x)
    {
        yield return new WaitForSeconds(x);
        if (attackRight)
        {
            attackRight.SetActive(false);
        }
        if(attackLeft)
        {
            attackLeft.SetActive(false);
        }
        movement.canMove = true;
        movement.canJump = true;
        movement.rb.gravityScale = 3;
    }
}
