using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerTotalPoints;

    public GameData(GameControle gameControler)
    {
        this.playerTotalPoints = gameControler.GetPlayerTotalPoints();
    }
}
