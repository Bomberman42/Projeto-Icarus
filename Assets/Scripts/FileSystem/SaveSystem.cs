using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEditor;

public static class SaveSystem
{
    private static string fileName = "/game1.flx";
    private static string levelFileName = "/level1.flx";

    public static void SaveGame(GameControle gameControler)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(filePath, FileMode.Create);

        GameData data = new GameData(gameControler);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveLevelGame(GameControle gameControler)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string countPath = Application.persistentDataPath + "/level_count.flx";
        FileStream stream = new FileStream(countPath, FileMode.Create);
        LevelData data = new LevelData(gameControler);
        formatter.Serialize(stream, data.levels.Count);
        stream.Close();

        using (BinaryWriter writer = new BinaryWriter(File.Open(levelFileName, FileMode.Create)))
        {
            writer.Write(data.levels.Count);
            foreach (Level level in data.levels)
            {
                writer.Write(level.type);
                writer.Write(level.star);
                writer.Write(level.coins);
                writer.Write(level.time);
            }
        }
    }

    public static List<Level> LoadLevelGame()
    {
        List<Level> levels = new List<Level>();
        using (BinaryReader reader = new BinaryReader(File.Open(levelFileName, FileMode.Open)))
        {
            int count = reader.ReadInt32();
            for (int index = 0; index < count; index++)
            {
                string type = reader.ReadString();
                string star = reader.ReadString();
                string coins = reader.ReadString();
                string time = reader.ReadString();
                Level level = new Level { coins = coins, time = time, star = star, type = type };
                levels.Add(level);
            }
        }

        return levels;

        //BinaryFormatter formatter = new BinaryFormatter();
        //string countPath = Application.persistentDataPath + "/level_count.flx";
        //int levelCount = 0;

        //if(File.Exists(countPath))
        //{
        //    FileStream stream = new FileStream(countPath, FileMode.Open);
        //    levelCount = (int)formatter.Deserialize(stream);
        //    stream.Close();
        //}
        //else
        //{
        //    Debug.LogError("Error");
        //    return null;
        //}

        //for(int index = 0; index < levelCount; index++)
        //{
        //    string filePath = Application.persistentDataPath + levelFileName;
        //    if(File.Exists(filePath + index))
        //    {
        //        FileStream stream = new FileStream(filePath + index, FileMode.Open);

        //        LevelData data = formatter.Deserialize(stream) as LevelData;
        //        stream.Close();
        //        return data;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //return null;

    }

    public static GameData LoadGame()
    {
        string filePath = Application.persistentDataPath + fileName;

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Error");
            return null;
        }
    }

    public static void DeleteGame()
    {
        GameControle gameControler = new GameControle();
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(filePath, FileMode.Create);

        GameData data = new GameData(gameControler);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
