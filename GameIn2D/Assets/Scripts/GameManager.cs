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
    //variable para indicar en que estado se encuentra el juego
    public GameState CurrentGameState = GameState.Menu;
    //metodo para iniciar el juego
    public void InciarPartida() 
    {
        SetGameState(GameState.InGame);
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
            //codigo para juegar
        }
        else if (NewGameState == GameState.GameOver)
        {
            //codigo para mostrar la escena de muerte
        }

        //asignamos el estado de juego actual desde el parametro
        this.CurrentGameState = NewGameState;
    }
}
