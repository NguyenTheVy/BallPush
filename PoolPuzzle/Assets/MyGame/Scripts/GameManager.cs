using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelManager CurrentLevel;

    public int LevelPlaying;


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

    private void Update()
    {

    }

    public void InitLevel()
    {
        GameObject LevelLoad = Resources.Load<GameObject>("Level/Level_" + LevelPlaying);

        GameObject levelObj = Instantiate(LevelLoad, Vector3.zero, Quaternion.identity);

        LevelManager CurrentLevel = levelObj.GetComponent<LevelManager>();

        this.CurrentLevel = CurrentLevel;
    }


    public void IncreaseLevel(int level)
    {
        int ToltalLevel = DataLevel1.CountAmoutFolderInResources("Level");

         Destroy(CurrentLevel.gameObject);

        if (level == ToltalLevel)
        {
            LevelPlaying = 1;
            DataLevel1.SetLevel(1);
            InitLevel();

            UiGamePlay.instance.InitLevel();

            return;
        }

        int currentLevel = 1;
        level++;

        LevelPlaying = level;

        if (level > currentLevel)
        {
            DataLevel1.SetLevel(level);
        }

        InitLevel();
        UiGamePlay.instance.InitLevel();
    }
}
