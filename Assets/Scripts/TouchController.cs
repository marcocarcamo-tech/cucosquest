using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    public Vector2 movement;
   
    
    public RectTransform rightButton;
    public  Button leftButton;
    private Touch rightTouch;

    public Rigidbody2D _rigidBody;

    private float horizontalPosition;

    private bool moveLeft, moveRight;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement

        if (moveLeft)
        {
            horizontalPosition = -1;
        } else if (moveRight)
        {
            horizontalPosition = 1;

        } else if (!moveLeft && !moveRight)
        {
            horizontalPosition = 0;
        }
        
        movement = new Vector2(horizontalPosition, 0f);
        

        
    }

    private void FixedUpdate()
    {

        float horizontalVelocity = movement.normalized.x * speed;
        _rigidBody.velocity = new Vector2(horizontalVelocity, _rigidBody.velocity.y);

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

}
