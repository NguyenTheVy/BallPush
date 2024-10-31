using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelManager CurrentLevel;

    public int LevelPlaying;

    int levelToLoad;

    public ParticleSystem Traifx;

    private void Awake()
    {
        LevelPlaying = DataLevel1.GetCurrentLevel();

        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgM);
        AdManager.instance.ShowBanner();

        InitLevel();
    }

    public void InitLevel()
    {
        levelToLoad = PlayerDataManager.GetLevelLoad();

        if (levelToLoad == 0)
        {
            GameObject LevelLoad = Resources.Load<GameObject>("Level/Level_" + LevelPlaying);

            GameObject levelObj = Instantiate(LevelLoad, Vector3.zero, Quaternion.identity);
            LevelManager CurrentLevel = levelObj.GetComponent<LevelManager>();

            this.CurrentLevel = CurrentLevel;
        }
        else
        {
            GameObject LevelLoad = Resources.Load<GameObject>("Level/Level_" + levelToLoad);

            GameObject levelObj = Instantiate(LevelLoad, Vector3.zero, Quaternion.identity);
            LevelManager CurrentLevel = levelObj.GetComponent<LevelManager>();

            this.CurrentLevel = CurrentLevel;
        }
    }


    public void IncreaseLevel(int level)
    {
        int totalLevel = DataLevel1.CountAmoutFolderInResources("Level"); 

        Destroy(CurrentLevel.gameObject);

        level++;
        LevelPlaying = level; 


        if (level > totalLevel)
        {
            levelToLoad = UnityEngine.Random.Range(20, 51);
            PlayerDataManager.SetLevelLoad(levelToLoad);
        }

        DataLevel1.SetLevel(LevelPlaying);

        InitLevel();
        UiGamePlay.instance.InitLevel();
    }
}
