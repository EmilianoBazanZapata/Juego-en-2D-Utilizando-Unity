using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "King") 
        {
            //Debug.Log("el jugador murio");
            //metodo que matara al jugador
            PlayerController.SharedInstance.Kill();
        }
    }
}
