using UnityEngine;

public class HitBullet : MonoBehaviour
{
    //><
    private Rigidbody2D rb;
    private SpriteRenderer Sr;
    public static HitBullet Instance;
    

    //private bool randomBool;
    private bool HitRight;
    private bool HitLeft;
    //private bool HitUpDown;
   // private bool canBeHit=true;

    [Header("Temps du stun Default=120, Nombre Pair Only")]
    public int endTime;
    [Header("Vélocité de la chute Default=5 ")]
    public float fallVelocity;

    [Header("gravité pendant la chute  Default=5 ")]
    public float gravity;
    private int RandomNumber;
    private int StartTime = 0;
    private Movement movement;
    private Animator anim;
    private bool stunAnim;

    private void Awake()
    {
        Sr=GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Instance = this;
    }

    private void Update()
    {
        if (HitRight)
        {
            stunAnim = true;
            anim.SetBool("stun",true);
            movement.canMove = false;
            movement.canJump = false;
            movement.canDash = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(fallVelocity);
        }
        if (HitLeft)
        {
            stunAnim = true;
            anim.SetBool("stun",true);
            movement.canMove = false;
            movement.canJump = false;
            movement.canDash = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(-fallVelocity);
        }
        /*if (HitUpDown)
        {
            
            if (randomBool)
            {
                randomBool = false;
                RandomNumber = UnityEngine.Random.Range(0,2);
            }
            movement.canMove = false;
            movement.canJump = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            if (RandomNumber == 0)
            {
                FallPlayer(5);
            }
            else
            {
                FallPlayer(-5);
            }
        }*/

        
    }
    public void HitBulletSoldierRight()
    {
        HitRight = true;

    }
    public void HitBulletSoldierLeft()
    {
        HitLeft = true;

    }
    /*public void HitBulletSoldierUpDown()
    {
        HitUpDown = true;

    }*/

      void FallPlayer(float x)
    {
        if (StartTime < endTime)
        {
            Sr.enabled=false;
            rb.velocity = new Vector2(x/*1.5f*/, rb.velocity.y);
            rb.gravityScale = gravity;
            var moduloTwo = StartTime % 2;
            if (moduloTwo!=0)
            {
                Sr.enabled=true;  
            }
            StartTime++;
        }
        else
        {
            Sr.enabled=true; 
            movement.canJump = true;
            movement.canDash = true;
            anim.SetBool("stun",false);
            //HitUpDown = false;
            HitLeft = false;
            HitRight = false;
            movement.canMove = true;
           // randomBool = true;
            rb.gravityScale = 3;
            StartTime = 0;
        }
    }
}
 