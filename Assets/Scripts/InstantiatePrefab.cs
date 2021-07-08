using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject player;
    public GameObject startingPoint;
   

   
    public void OnEnable()   

    {
        player.transform.position = startingPoint.transform.position;
    }
    
}
