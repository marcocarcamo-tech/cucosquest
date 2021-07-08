using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //Quantities
    public float speed = 1f;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public float playerAware = 3f;
    public float aimingTime = 0.5f;
    public float shootingTime = 2f;

    //Components
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Weapon weapon;

    //Movement
    private Vector2 movement;
    private bool facingRight;
    private bool isAttacking;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x < 0f)
        {
            facingRight = false;
        }
        else if (transform.localScale.x > 0f)
        {
            facingRight = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.right;

        if (facingRight == false) {
            direction = Vector2.left;
        }

        
        if (isAttacking == false) {
            if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer)) {
                //Debug.Log("Character is fliping");
                Flip();
            }
        }

    }



    private void FixedUpdate()
    {
        float horizontalVelocity = speed;

        if (facingRight == false) {
            horizontalVelocity = horizontalVelocity * -1f;
        }

        if(isAttacking) {
            horizontalVelocity = 0f;
        }

        rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
    }


    private void LateUpdate()
    {
        animator.SetBool("Idle", rigidbody.velocity == Vector2.zero);
    }
    //This method flip the enemy
    void Flip()
    {
            facingRight = !facingRight;
            float localScaleX = transform.localScale.x;
            localScaleX = localScaleX * -1f;
            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        
        
    
    }

    //This method check if the enemy finds the player, if so starts a couroutine for aiming and shooting
    private void OnTriggerStay2D (Collider2D collision)
    {
        if (isAttacking == false && collision.CompareTag("Player") && gameObject == enabled) {  
            StartCoroutine("AimAndShoot");
        }
    }

    //This method make the enemy aiming and shooting
    private IEnumerator AimAndShoot() 
    {
        //float speedBackup = speed;
        //speed = 0f;

        isAttacking = true;

        yield return new WaitForSeconds(aimingTime);

        animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootingTime);

        isAttacking = false;
        //speed = speedBackup;
    
    }

    void CanShoot()
    {
        if (weapon != null)
        {
            weapon.Shoot();
        }
    }

    private void OnEnable()
    {
        isAttacking = false;
    }

    private void OnDisable()
    {
        StopCoroutine("AimAndShoot");
        isAttacking = false;
    }

}
