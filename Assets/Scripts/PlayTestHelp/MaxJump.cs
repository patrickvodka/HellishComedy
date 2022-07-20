using System.Collections;
using UnityEngine;
using Unity.Mathematics;

public class MaxJump : MonoBehaviour
{
    private Movement move;
    
    [Header("blanc=Saut max")]
    public GameObject headWhite;
    
    [Header("bleu=Saut mini")]
    public GameObject headBlue;
    
    [Header("que 1 booléen peut etre active pour l'instant")]
    public bool maxJump;
    public bool miniJump;
    [Space] 
    public bool invisibleHeads;
    public bool destroyAllCopy;
    
    private float timerMax=.5f;
    private float timerMini=0.4f;
    private GameObject headStacks;
    private bool waitingSys;
    void Awake()
    {
        headStacks = GameObject.FindWithTag("HeadStacks");
            move = GetComponent<Movement>();
    }

    void Update()
    {
        if (invisibleHeads)
        {
            foreach (Transform child in headStacks.transform) {
                child.gameObject.SetActive(false);
            } 
            
        }
        else
        {
            foreach (Transform child in headStacks.transform) 
                child.gameObject.SetActive(true);
            
        }
        if (destroyAllCopy)
        {
            foreach (Transform child in headStacks.transform) 
                Destroy(child.gameObject);
            
        }

        if (!waitingSys)
        {
            if (maxJump && move.maxJumpBool)
            {
                waitingSys = true;
                move.maxJumpBool = false;
                StartCoroutine(WaitingSysteme());
                StartCoroutine(SpawnMax());

            }

            if (miniJump && move.maxJumpBool)
            {
                waitingSys = true;
                move.maxJumpBool = false;
                StartCoroutine(WaitingSysteme());
                StartCoroutine(SpawnMini());
            }
        }
    }
    private IEnumerator SpawnMini()
    {
        yield return new WaitForSeconds(timerMini);
        Instantiate(headBlue,(transform.position+new Vector3(0,.5f,0)), quaternion.Euler(0, 0, 0),headStacks.transform);
    }
    private IEnumerator SpawnMax()
    {
        yield return new WaitForSeconds(timerMax);
        Instantiate(headWhite,(transform.position+new Vector3(0,.5f,0)), quaternion.Euler(0, 0, 0),headStacks.transform);
    }

    private IEnumerator WaitingSysteme()
    {
       yield  return new WaitForSeconds(0.1f);
       waitingSys = false;
    }
}
