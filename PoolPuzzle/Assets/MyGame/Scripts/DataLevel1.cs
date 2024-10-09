using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;


public static class DataLevel1
{
    private static string ALL_DATA_LEVEL = "ALL_DATA_LEVEL";

    private static DataLevelModel dataLevelModel;


    static DataLevel1()
    {
        Debug.Log("Static");
        dataLevelModel = JsonConvert.DeserializeObject<DataLevelModel>(PlayerPrefs.GetString(ALL_DATA_LEVEL));

        if (dataLevelModel == null)
        {
            dataLevelModel = new DataLevelModel();
            dataLevelModel.CurrentLevel = 1;
        }
        SaveDataLevel();
    }

    #region Level
    private static void SaveDataLevel()
    {
        string json = JsonConvert.SerializeObject(dataLevelModel);
        PlayerPrefs.SetString(ALL_DATA_LEVEL, json);
    }

    public static void SetLevel(int level)
    {
        dataLevelModel.SetLevel(level);
        SaveDataLevel();
    }

    public static int GetCurrentLevel()
    {
        return dataLevelModel.GetCurrentLevel();
    }

    public static int CountAmoutFolderInResources(string path)
    {
        return dataLevelModel.CountAmoutFolderInResources(path);
    }

    #endregion

}

public class DataLevelModel
{

    public int CurrentLevel;

    public void SetLevel(int level)
    {
        CurrentLevel = level;
    }

    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public int CountAmoutFolderInResources(string path)
    {
        int _count = 0;

        GameObject[] Resources1 = Resources.LoadAll<GameObject>(path);
        _count = Resources1.Length;

        return _count;
    }
}