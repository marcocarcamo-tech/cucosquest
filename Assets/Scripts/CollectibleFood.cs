using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleFood : MonoBehaviour
{

	[SerializeField]int addItem = 1;
	private Collider2D _collider;
	private SpriteRenderer _sprite;

	

    // Start is called before the first frame update
    void Awake()
    {
		_collider = GetComponent<Collider2D>();
		_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{

			//Send message

			collision.SendMessageUpwards("CounterItems", addItem);

			// Disable Collider
			_collider.enabled = false;

			//Disable sprite renderer

			_sprite.enabled = false;

			// Destroy after some  time
			Destroy(gameObject, 2f);

			
		}
	}

	

	
}
