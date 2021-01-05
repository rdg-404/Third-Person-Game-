using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaoFalse : MonoBehaviour
{
    
    void OnCollisionEnter(Collision col) 
    {
        if(col.gameObject.name.Equals("Player"))
        {
            GetComponent<Renderer>().material.color = Color.red;
            Destroy(gameObject);
        }
    }
}
