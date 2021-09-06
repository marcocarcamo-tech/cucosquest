using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodCounter : MonoBehaviour
{
    public TextMeshProUGUI _count;
    [SerializeField] int foodCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CounterItems(int counter)
    {
        //Print count in canvas
        foodCounter += counter;
        _count.text = "Food: " + foodCounter.ToString();
    }
}
