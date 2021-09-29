using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QualityManager : MonoBehaviour
{
    //tomo el cbo de la calidad
    public TMP_Dropdown dropdown;
    public int calidad;
    private void Start()
    {
        //el valor por defecto de la calidad es 3
        calidad = PlayerPrefs.GetInt("NumeroCalidad", 3);
    }
    public void AjustarClidad()
    {
        //paso el valor de la calidad que concuerda con el cbo 
        QualitySettings.SetQualityLevel(dropdown.value);
        //actualizo la calidad del uego
        PlayerPrefs.SetInt("NumeroCalidad", dropdown.value);
        calidad = dropdown.value;
    }
}