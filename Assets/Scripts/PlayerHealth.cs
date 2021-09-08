using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 5;
    public RectTransform healthUI;
    public RectTransform startMenu;
    public RectTransform gameOverMenu;
    public GameObject hordes;
    public TextMeshProUGUI _textMesh;

    
    

    private int health;
    private float heartSize = 18f; 

    private SpriteRenderer renderer;
    private Animator animator;
    private PlayerController controller;
    private PlayerAttack playerAttack;



    private void Awake()
    {

        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    // Update is called once per frame
    public void AddDamage(int amount)
    {
        health = health - amount;
        //Visual Feedback
        StartCoroutine("VisualFeedback");

        //Game Over
        if (health <= 0) {
            health = 0;
            gameObject.SetActive(false);
            
        }

        healthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        Debug.Log("Player got damaged. His current health is " + health);
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        //Max health
        if(health > totalHealth ) {healthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
            health = totalHealth;
        }

        healthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        Debug.Log("Player got some life. His current health is " + health);
    }

    private IEnumerator VisualFeedback()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        renderer.color = Color.white;
    }

    private void OnEnable()
    {
        health = totalHealth;
        healthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        renderer.color = Color.white;
        gameOverMenu.gameObject.SetActive(false);

        if (startMenu.gameObject.activeSelf == true)
        {
            _textMesh.gameObject.SetActive(false);
        } else
        {
            _textMesh.gameObject.SetActive(true);
        }
        
    }

    private void OnDisable()
    {
         
        //
        gameOverMenu.gameObject.SetActive(true);

        hordes.gameObject.SetActive(false);
        
        animator.enabled = false;
        controller.enabled = false;
        playerAttack.enabled = false;
        _textMesh.gameObject.SetActive(false);
    }

    
}
