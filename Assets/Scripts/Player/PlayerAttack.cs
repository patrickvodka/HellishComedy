using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float  animTime;
    public float attackCd;
    private Movement movement;
    private GameObject attackRight;
    private GameObject attackLeft;
    private SpriteRenderer sR;

    private bool canAttack=true;
    private bool Left;
    private bool AttackClick;
    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
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
        CheckLR();
        if (AttackClick && !Left&& canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackCd(attackCd));
            attackRight.SetActive(true);
            StopGravity(animTime);
            StartCoroutine(AttackAnimTime(animTime));
        }

        if (AttackClick && Left && canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackCd(attackCd));
            attackLeft.SetActive(true);
            StopGravity(animTime);
            StartCoroutine(AttackAnimTime(animTime));
        }
    }

    private void CheckLR()
    {
        var check = sR.flipX ? Left = true : Left = false;
    }

    private void StopGravity(float x)
    {
        movement.canMove = false;
        movement.canJump = false;
        movement.rb.velocity = new Vector2(0, 0);
        movement.rb.gravityScale = 0;
    }

    IEnumerator AttackAnimTime(float x)
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

    private IEnumerator AttackCd(float x)
    {
        yield return new WaitForSeconds(x);
        canAttack = true;

    }
}
