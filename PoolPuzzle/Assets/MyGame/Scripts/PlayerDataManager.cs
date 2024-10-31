using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerDataManager
{
    private static string Sound = "SOUND";
    private static string Music = "MUSIC";


    private static string LevelLoad = "LEVELLOAD";

    public static bool GetSound()
    {
        return PlayerPrefs.GetInt(Sound, 1) == 1;
    }

    public static void SetSound(bool isOn)
    {
        PlayerPrefs.SetInt(Sound, isOn ? 1 : 0);
    }

    public static bool GetMusic()
    {
        return PlayerPrefs.GetInt(Music, 1) == 1;
    }

    public static void SetMusic(bool isOn)
    {
        PlayerPrefs.SetInt(Music, isOn ? 1 : 0);
    }

    public static int GetLevelLoad()
    {
        return PlayerPrefs.GetInt(LevelLoad, 0);
    }

    public static void SetLevelLoad(int Level)
    {
        PlayerPrefs.SetInt(LevelLoad, Level);
    }

}
