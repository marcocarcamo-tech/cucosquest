using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    /*public void AddDamage()
    {
        gameObject.SetActive(false);
    }*/

    public int totalHealth = 2;
   // private int score;
  //  public int scoreIncrease = 50;
    private int health;
    private SpriteRenderer renderer;
    //public TextMeshProUGUI scoreText;


    // Start is called before the first frame update

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        
    }
    void Start()
    {
        health = totalHealth;
       // score = 0;
       // ScoreCounter();
    }

    /*private void Update()
    {
        
        if(health == 0) {
            gameObject.SetActive(false);
        }
    }*/
    // Update is called once per frame
    public void AddDamage(int amount)
    {
        health = health - amount;
        

        //Visual Feedback
        StartCoroutine("VisualFeedback");

        //GO
        if (health <= 0) {
            health = 0;
            gameObject.SetActive(false);
         /*   score++;
            score = score * scoreIncrease;
            ScoreCounter();*/
        }

        Debug.Log("Enemy got damaged his current health is " + health);

        
    } 

    
    private IEnumerator VisualFeedback()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        renderer.color = Color.white;
    }

    /*public void ScoreCounter()
    {
        
        scoreText.text = "Score: " + score.ToString();

    }*/

    private void OnEnable()
    {
        health = totalHealth;
        renderer.color = Color.white;

    }
}
