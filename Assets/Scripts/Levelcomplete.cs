using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Levelcomplete : MonoBehaviour
{

    public GameObject _gameObject;
    public GameObject _text;
    public bool allItemsCollected;
    public TextMeshProUGUI _textMesh;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        hasAllItems();
    }

    private bool hasAllItems()
    {
        if(_gameObject.transform.childCount == 0) {
            allItemsCollected = true;
            Debug.Log("You have all items");
        }

        return allItemsCollected;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && allItemsCollected == true)
        {
            //Level completed
            _textMesh.text = "Level completed";
            _text.SetActive(true);
        }
    }
}
