﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //variable para saber si la variable fue recogida o no
    bool IsCollected = false;
    //falor de los coleccionables
    public float value = 0;
    private void Start() {
        Show();    
    }
    //metodo para activar la moneda
    private void Show()
    {
        //activamos la imagen de la moneda => por ende inicia la animacion
        this.GetComponent<SpriteRenderer>().enabled = true;
        //activamos el collider de la moneda
        this.GetComponent<CircleCollider2D>().enabled = true;
        //por ahora no recogimos la moneda
        IsCollected = false;
    }
    //metodo para desactivar la moneda
    private void Hide()
    {
        //desactivamos la imagen de la moneda => por ende inicia la animacion
        this.GetComponent<SpriteRenderer>().enabled = false;
        //desactivamos el collider de la moneda
        this.GetComponent<CircleCollider2D>().enabled = false;
    }
    //metodo para colectar la moneda
    private void Collect()
    {
        IsCollected = true;
        //lamaremos el metodo hide para que se oculte
        Hide();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "King")
        {
            Collect();
        }
        //pasamos el valor de loa moneda al metodo contador 
        GameManager.SharedInstance.CollectObject(value);
    }
}