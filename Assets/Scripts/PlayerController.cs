using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float longIdleTime = 15f;
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public float attackTime = 0.5f;
    public float attackReactivateDelay = 0.2f;

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
    private bool moveLeft, moveRight;

    private bool facingRight = true;
    private bool isGrounded;
    private float horizontalPosition;
    private bool isJumping;

    // Attack
    private bool isAttacking;
    private bool isSecondAttacking;

    //UI Buttons
    public Button attackButton;

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

        if (moveLeft)
        {
            horizontalPosition = -1;
        }
        else if (moveRight)
        {
            horizontalPosition = 1;

        }
        else if (!moveLeft && !moveRight)
        {
            horizontalPosition = 0;
        }

        movement = new Vector2(horizontalPosition, 0f);

        //Flip

        if(facingRight == true && moveLeft)
        {
            Flip();
        } else if(facingRight == false && moveRight)
        {
            Flip();
        }

        // Is Grounded?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping'
        if (isJumping == true && isGrounded == true && isAttacking == false) {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }

        //Wanna attack?
        if(isAttacking == true && isGrounded == true) {
            movement = Vector2.zero;
            rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            isAttacking = false;
         
        } else if (isAttacking == false )
        {

        }

        //Second attack?

    }

    void FixedUpdate()
    {
        if(isAttacking == false)
        {
            float horizontalVelocity = movement.normalized.x * speed;
            rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
        }
        
    }

    void LateUpdate()
    {
        animator.SetBool("Idle", movement == Vector2.zero);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rigidbody.velocity.y);
        attackTime += Time.deltaTime;

        //Animator
        //if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
        //    isAttacking = true;
        //} else {
        //    isAttacking = false;
        //}

        //if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack2") && attackTime >= 0.5f)
        //{
        //    isSecondAttacking = true;
        //}
        //else
        //{
        //    isSecondAttacking = false;
        //}

        

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

    //IEnumerator WaitForNextAttack(Button button, float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    

    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Left()
    {
        moveLeft = true;

    }

    public void StopLeft()
    {
        moveLeft = false;
    }

    public void Right()
    {
        moveRight = true;

    }

    public void StopRight()
    {
        moveRight = false;
    }

    public void Attack()
    {
        isAttacking = true;
        
        

    }

    public void Jump()
    {
        isJumping = true;
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
