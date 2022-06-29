using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool AttackOn;
    private GameObject attackRight;
    private GameObject attackLeft;
    private Transform Player;
    private bool Left;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        attackRight = transform.Find("AttackRight").gameObject;
        attackLeft = transform.Find("AttackLeft").gameObject;
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        { 
            AttackOn = true;
        }
        if(ctx.canceled) 
            AttackOn = false;
    }

    private void Update()
    {
        CheckLR();
        if (AttackOn && !Left)
        {
            attackRight.SetActive(true);
            StartCoroutine(AttackCd());
        }

        if (AttackOn && Left)
        {
            attackLeft.SetActive(true);
            StartCoroutine(AttackCd());
        }
    }

    private void CheckLR()
    {
        if (Player.eulerAngles ==new Vector3(0, 0, 0))
        {
           Left = false;
        }
        else
        {
            Left = true;
        }
    }

    IEnumerator AttackCd()
    {
        yield return new WaitForSeconds(.2f);
        if (attackRight)
        {
            attackRight.SetActive(false);
        }
        if(attackLeft)
        {
            attackLeft.SetActive(false);
        }
    }
}
