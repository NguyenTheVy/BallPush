using DG.Tweening;
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

        AimBtn();
    }


    public void AimBtn()
    {
        btn_Skip.transform.localScale = Vector3.one;
        btn_Skip.transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnReplay()
    {
        if(AdsController.ins.TimeShowInter >= 120)
        {
            AdManager.instance.ShowInter(()=>
            {
                SceneManager.LoadScene(1);
                AdsController.ins.TimeShowInter = 0;

            }, ()=>
            {
                SceneManager.LoadScene(1);
                AdsController.ins.TimeShowInter = 0;
            }, "ShowInter");
        }
        else
        {
            SceneManager.LoadScene(1);
        }

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
        DOTween.Kill(transform);
    }
}
