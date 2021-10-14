﻿using System.Diagnostics;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumerado
//posibles estado del juego
public enum GameState
{
    Menu,
    InGame,
    GameOver,
    Pause,
    Options
}

public class GameManager : MonoBehaviour
{
    private bool EstadoPausa;
    //variable que hace referencia al manager
    public static GameManager SharedInstance;

    //variable para indicar en que estado se encuentra el juego
    public GameState CurrentGameState = GameState.Menu;

    //referencia al canvas del menu
    public Canvas MenuCanvas;
    //referencia al canvas del juego
    public Canvas GameCanvas;
    //referencia al canvas del gameover
    public Canvas GameOverCanvas;
    //referencia la canvas de pausa
    public Canvas PauseCanvas;
    //referencia la canvas de Opciones en el menu
    public Canvas OptionsMenuCanvas;
    //cantidad de coleccionales recogidos
    public float CollectedObjects = 0;

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        BackToMenu();
    }
    private void Update()
    {
        /*if (Input.GetButton("Start"))
        {
            StartGame();
        }*/
        if (Input.GetButton("Pause"))
        {
            Pause();
        }
        // if (Input.GetButton("Restart") && CurrentGameState == GameState.GameOver)
        // {
        //     RestartGame();
        // }
    }
    //metodo para iniciar el juego
    public void StartGame()
    {
        SetGameState(GameState.InGame);
        //CameraFollow.SharedInstance.ResetCameraPosition();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();
        //si el jugador no supera la distancia de 13 el primer bloque sera el mismo
        if (PlayerController.SharedInstance.transform.position.x > 13)
        {
            LevelGenerator.SharedInstance.RemoveAllTheBlock();
            LevelGenerator.SharedInstance.GenerateInitialBlock();
        }
        //despues de generar el bloque genero el jugador
        PlayerController.SharedInstance.StartGame();
        this.CollectedObjects = 0f;

    }
    //metodo para reiniciar el juego
    public void RestartGame()
    {
        SetGameState(GameState.InGame);
        PlayerController.SharedInstance.StartGame();
    }
    //metodo que se llamara al morir
    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        this.EstadoPausa = false;
    }
    //metodo que llamara para volver al menu
    public void BackToMenu()
    {
        //UnityEngine.Debug.Log(EstadoPausa);
        if (this.EstadoPausa == true)
        {
            Pause();
        }
        else
        {
            SetGameState(GameState.Menu);
        }
        //UnityEngine.Debug.Log(EstadoPausa);
    }
    //metodo que llamare para volver al juego desde las Opciones
    public void BackToGame()
    {
        SetGameState(GameState.InGame);
    }
    public void Pause()
    {
        if (CurrentGameState != GameState.GameOver && CurrentGameState != GameState.Menu)
        {
            SetGameState(GameState.Pause);
            this.EstadoPausa = true;
        }
    }
    //metodo para continuar el juego
    public void ContinueGame()
    {
        SetGameState(GameState.InGame);
    }
    //metodo para entrar al menu del juego desde el menu de inicio
    public void OptionGameInMenu()
    {
        SetGameState(GameState.Options);
    }
    //metodo para finalizar la aplicacion
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    //metodo encargado de cambiar el estado del juego
    private void SetGameState(GameState NewGameState)
    {
        if (NewGameState == GameState.Menu)
        {
            //codigo para mostrar el menu
            //aqui activare el canvas
            MenuCanvas.enabled = true;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = false;
            Time.timeScale = 1;
        }
        else if (NewGameState == GameState.InGame)
        {
            //codigo para jugar
            MenuCanvas.enabled = false;
            GameCanvas.enabled = true;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = false;
            Time.timeScale = 1;
            this.EstadoPausa = true;
        }
        else if (NewGameState == GameState.GameOver)
        {
            //codigo para mostrar la escena de muerte
            MenuCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = true;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = false;
            Time.timeScale = 0;
        }
        else if (NewGameState == GameState.Pause)
        {
            //codigo para mostrar el menu de pausa
            MenuCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = true;
            OptionsMenuCanvas.enabled = false;
            Time.timeScale = 0;
        }
        else if (NewGameState == GameState.Options)
        {
            //codigo para mostrar el menu de pausa desde las opciones desde el menu del juego
            MenuCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = true;
            Time.timeScale = 0;
        }
        //asignamos el estado de juego actual desde el parametro
        this.CurrentGameState = NewGameState;
    }
    public void CollectObject(float ObjectValue)
    {
        this.CollectedObjects += ObjectValue;
        //UnityEngine.Debug.Log("recogimos : " + CollectedObjects);
    }
}