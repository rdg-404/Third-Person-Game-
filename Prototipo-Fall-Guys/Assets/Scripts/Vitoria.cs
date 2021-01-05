using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vitoria : MonoBehaviour
{
    public bool lockCursor;
     
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.name.Equals("Player")){
            SceneManager.LoadScene("Vitoria");

            if(!lockCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
