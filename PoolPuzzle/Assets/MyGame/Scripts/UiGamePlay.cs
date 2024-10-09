using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlay : MonoBehaviour
{
    public static UiGamePlay instance;

    [SerializeField] private TMP_Text Level_txt;

    [SerializeField] private Button btn_setting;
    [SerializeField] private Button btn_Skip;
    [SerializeField] private Button btn_Replay;

    public PopupWin popupWin;
    public PopupPause PopupSetting;
    public PopupLose popupLose;
    public PopupNoInternet PopupNoInternet;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        btn_setting.onClick.AddListener(OnOpenSetting);
        btn_Skip.onClick.AddListener(OnSkipGame);
        btn_Replay.onClick.AddListener(OnReplay);
        InitLevel();
    }

    private void OnReplay()
    {
        SceneManager.LoadScene(1);
    }

    public void InitLevel()
    {
        Level_txt.text = $"Level {GameManager.instance.LevelPlaying}";
    }

    private void OnSkipGame()
    {
        AdManager.instance.ShowReward(() =>
        {
            GameManager.instance.IncreaseLevel(GameManager.instance.LevelPlaying);
        }, ()=>
        {
            PopupNoInternet.gameObject.SetActive(true);
        }, "ShowReward");
    }

    private void OnOpenSetting()
    {
        PopupSetting.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SoundManager.Instance.StopPlayMusic();
    }
}