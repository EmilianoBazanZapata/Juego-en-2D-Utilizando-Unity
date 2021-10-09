using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public AudioSource _As;
    //arreglo para agregar los temas
    public AudioClip[] AudioClipArray;

    private void Awake() {
        _As = GetComponent<AudioSource>();
    }

    private void Start() {
        _As.clip = AudioClipArray[Random.Range(0,AudioClipArray.Length)];
    }
}
