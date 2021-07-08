using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 2f;
    public Vector2 direction;

    public float livingTime = 3f;
    public Color initialColor = Color.white;
    public Color finalColor;
    private Rigidbody2D rigidboy;
    private Animator animator;

    private SpriteRenderer renderer;
    private float startingTime;
    private bool facingRight;
    private bool returning;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidboy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //Save initial time
        startingTime = Time.time;

        // Destroy the bullet after some time
        Destroy(gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        /* This section commented because we eill move by rigirbody, not by transform, but let this fragment of code for learning purposes
        // Move object
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.Translate(movement); //transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);*/

        // Change bullet´s color over time
        float timeSinceStarted = Time.time - startingTime;
        float percentageCompleted = timeSinceStarted / livingTime;

        renderer.color = Color.Lerp(initialColor, finalColor, percentageCompleted);

    }

    private void FixedUpdate()
    {
        Vector2 movement = direction.normalized * speed;
        rigidboy.velocity = movement;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(returning == false && collision.CompareTag("Player")) {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(gameObject);
        }
        if (returning == true && collision.CompareTag("Enemy")) {
            Debug.Log("Le hago daño al payer");
            //Tell player to get hurt
            //collision.gameObject.getComponent<PlayerHealth>().addDamage---este código ya especifica el script en donde debe buscarse el metodo
            collision.SendMessageUpwards("AddDamage", damage);//este codigo solamente indica que se busque un metodo y si no se encuentra que se busque en el padre
            Destroy(gameObject);
        }
    }

    public void AddDamage()
    {
        returning = true;
        direction = direction * -1;
    }
}
