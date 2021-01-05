using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaoTrue : MonoBehaviour
{
    void OnCollisionEnter(Collision col) 
    {
        if(col.gameObject.name.Equals("Player"))
        {
            GetComponent<Renderer> ().material.color = Color.green;
        }
    }

    void OnCollisionExit(Collision col) {
        if(col.gameObject.name.Equals("Player"))
        {
            GetComponent<Renderer> ().material.color = Color.white;
        }
    }
}
