using System.Collections;
using UnityEngine;
using Unity.Mathematics;

public class MaxJump : MonoBehaviour
{
    private Movement move;
    private bool canSpawn=true;
    [Header("blanc=Saut max")]
    public GameObject HeadWhite;
    [Header("bleu=Saut mini")]
    public GameObject HeadBlue;
    [Header("que 1 booléen peut etre active pour l'instant")]
    public bool maxJump;
    public bool miniJump;
    private float timerMax=.5f;
    private float timerMini=0.4f;
    void Awake()
    {
            move = GetComponent<Movement>();
    }

    void Update()
    {
        if (maxJump && move.JumpBool)
        {
           // if (maxJumpBool)
           // {
                StartCoroutine(SpawnMax());
           // }
            
        }
        if (miniJump && move.JumpBool)
        {
          //  if (miniJumpBool)
          //  {
                StartCoroutine(SpawnMini());
          //  }
        }
    }
    private IEnumerator SpawnMini()
    {
        move.JumpBool = false;
        yield return new WaitForSeconds(timerMini);
        Instantiate(HeadWhite,(transform.position+new Vector3(0,.5f,0)), quaternion.Euler(0, 0, 0));
    }
    private IEnumerator SpawnMax()
    {
        move.JumpBool = false;
        yield return new WaitForSeconds(timerMax);
        Instantiate(HeadBlue,(transform.position+new Vector3(0,.5f,0)), quaternion.Euler(0, 0, 0));

    }
}
