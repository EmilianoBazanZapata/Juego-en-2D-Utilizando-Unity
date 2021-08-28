using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enumerado
//posibles estado del juego
public enum GameState 
{
    Menu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    //variable que hace referencia al manager
    public static GameManager SharedInstance;

    //variable para indicar en que estado se encuentra el juego
    public GameState CurrentGameState = GameState.Menu;

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
        if (Input.GetButton("Start"))
        {
            StartGame();
        }
        if (Input.GetButton("Pause"))
        {
            BackToMenu();
        }
        if (Input.GetButton("Restart") && CurrentGameState == GameState.GameOver)
        {
            RestartGame();
        }
    }
    //metodo para iniciar el juego
    public void StartGame()
    {
        SetGameState(GameState.InGame);
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
    }
    //metodo que llamara para volver al menu
    public void BackToMenu() 
    {
        SetGameState(GameState.Menu);
    }
    //metodo encargado de cambiar el estado del juego
    private void SetGameState(GameState NewGameState) 
    {
        if (NewGameState == GameState.Menu) 
        {
            //codigo para mostrar el menu
        }
        else if (NewGameState == GameState.InGame)
        {
            //codigo para jugar
        }
        else if (NewGameState == GameState.GameOver)
        {
            //codigo para mostrar la escena de muerte
        }

        //asignamos el estado de juego actual desde el parametro
        this.CurrentGameState = NewGameState;
    }
}
