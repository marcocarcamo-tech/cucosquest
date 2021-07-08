using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public int attackCount;
    public int scoreIncrease = 25;
    private int score;
    private bool isAttacking;
    private Animator animator;
    
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        attackCount = 0;
        score = 0;
        SetScoreCounter();
    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        //Animator
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (isAttacking == true) {
            if(collision.CompareTag("Enemy")) {
                collision.SendMessageUpwards("AddDamage", damage);
                attackCount++;
                score = attackCount * scoreIncrease;

                
                SetScoreCounter();

            }

        }

        if (isAttacking == true)
        {
            if (collision.CompareTag("Bullet"))
            {
                collision.SendMessageUpwards("AddDamage", damage);
            
            }
        }


    }

    private void SetScoreCounter()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void OnEnable()
    {
        attackCount = 0;
        score = 0;
        SetScoreCounter();
    }
}
