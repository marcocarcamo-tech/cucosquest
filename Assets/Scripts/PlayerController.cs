using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float longIdleTime = 15f;
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public float attackTime = 0.5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    //public Transform startingPoint;
   // public GameObject prefab;

    // References
    private Rigidbody2D rigidbody;
    private Animator animator;

    private float longIdleTimer;

    // Movement

    private Vector2 movement;
                       
    private bool facingRight = true;
    private bool isGrounded;
    public Vector2 rightMovement;
    public Vector2 leftMovement;

    // Attack
    private bool isAttacking;
    private bool isSecondAttacking;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //Movement

        
        rightMovement = new Vector2(1f, 0f);
        leftMovement = new Vector2(-1, 0f);



        // Is Grounded?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping'
        if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false) {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Wanna attack?
        if(Input.GetButtonDown("Fire1") && isGrounded == true) {
            movement = Vector2.zero;
            rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
         
        }

        //Second attack?

    }

    void FixedUpdate()
    {
        if(isAttacking == false)
        {
            //float horizontalVelocity = movement.normalized.x * speed;
            //rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
        }
        
    }

    void LateUpdate()
    {
        animator.SetBool("Idle", movement == Vector2.zero);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rigidbody.velocity.y);
        attackTime += Time.deltaTime;

        //Animator
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            isAttacking = true;
        } else {
            isAttacking = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack2") && attackTime >= 0.5f)
        {
            isSecondAttacking = true;
        }
        else
        {
            isSecondAttacking = false;
        }

        

        // Long Idle
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
            longIdleTimer += Time.deltaTime;

            if(longIdleTimer >= longIdleTime) {
                animator.SetTrigger("LongIdle");
            }

        } else {
            longIdleTimer = 0f;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Left()
    {
        float horizontalVelocity = rightMovement.normalized.x * speed;
        rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
    }

    public void Right()
    {
        float horizontalVelocity = rightMovement.normalized.x * speed;
        rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
    }


    /*private void Instantiate()
    {
        Instantiate(gameObject, startingPoint.position, Quaternion.identity);
    }*/

    /*private void OnDisable()   
    {
       GameObject player = Instantiate(prefab, startingPoint.transform.position, Quaternion.identity);

    }*/
}
