using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private SpriteRenderer sR;
    private Movement movement;
    private Animator anim;
    void Awake()
    {
        sR = transform.GetComponent<SpriteRenderer>();
        movement = transform.GetComponent<Movement>();
        anim = transform.GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
