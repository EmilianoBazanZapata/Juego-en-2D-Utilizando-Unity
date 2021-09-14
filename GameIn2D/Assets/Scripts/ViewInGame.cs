using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    //etiqueta para ver la puntuacion actual
    public Text CollectableLabel;

    public Text ScoreLabel;

    public Text MaxScoreLabel;
    private void Update()
    {
        if (GameManager.SharedInstance.CurrentGameState == GameState.InGame) 
        {
            float CurrentObjects  = GameManager.SharedInstance.CollectedObjects;
            //mostrare en el canvas la cantidad de monedas recogidas
            this.CollectableLabel.text = CurrentObjects.ToString(); 

        }
    }
}