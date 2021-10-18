using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBarCollider : MonoBehaviour
{
    public Collider2D collider2D;
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "King") 
        {
            collider2D.enabled = true;
        }
    }
}
