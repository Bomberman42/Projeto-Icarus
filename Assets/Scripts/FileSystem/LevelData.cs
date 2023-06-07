using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<Level> levels = new List<Level>();


    public LevelData(GameControle gameControler)
    {
        this.levels = gameControler.GetLevel();
    }
}
