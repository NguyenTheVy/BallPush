using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPause : AnimLeftRight
{
    [Header("Setting")]
    [SerializeField] private Button btn_toogleMusic;
    [SerializeField] private List<Image> L_toogleImgMusic;

    [SerializeField] private Button btn_sfx;
    [SerializeField] private List<Image> L_toogleImgSfx;

    [SerializeField] private Button btn_BackGame;
    [SerializeField] private Button btn_ok;

    [SerializeField] private AnimationUIController AnimationUIController;

    private void OnEnable()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.ClosePopup);

        btn_toogleMusic.onClick.AddListener(OnToogleMusic);
        btn_sfx.onClick.AddListener(OnToogleSfx);

        btn_BackGame.onClick.AddListener(OnBackGame);
        btn_ok.onClick.AddListener(OnClosePopup);


        InitMusic();
        InitSfx();
    }

    private void InitMusic()
    {
        bool isOn = PlayerDataManager.GetMusic();

        if (isOn)
        {
            ResetTurnMusic();
            L_toogleImgMusic[1].gameObject.SetActive(true);
        }
        else
        {
            ResetTurnMusic();
            L_toogleImgMusic[0].gameObject.SetActive(true);
        }
    }


    private void InitSfx()
    {
        bool isOn = PlayerDataManager.GetSound();

        if (isOn)
        {
            ResetTurnSfx();
            L_toogleImgSfx[1].gameObject.SetActive(true);
        }
        else
        {
            ResetTurnSfx();
            L_toogleImgSfx[0].gameObject.SetActive(true);
        }
    }

    private void OnToogleMusic()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);

        bool isOn = PlayerDataManager.GetMusic();

        if (!isOn)
        {
            ResetTurnMusic();
            L_toogleImgMusic[1].gameObject.SetActive(true);
            PlayerDataManager.SetMusic(!isOn);
        }
        else
        {
            ResetTurnMusic();
            L_toogleImgMusic[0].gameObject.SetActive(true);
            PlayerDataManager.SetMusic(!isOn);
        }

        SoundManager.Instance.SettingMusic(PlayerDataManager.GetMusic());
    }

    private void OnToogleSfx()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);

        bool isOn = PlayerDataManager.GetSound();

        if (!isOn)
        {
            ResetTurnSfx();
            L_toogleImgSfx[1].gameObject.SetActive(true);
            PlayerDataManager.SetSound(!isOn);
        }
        else
        {
            ResetTurnSfx();
            L_toogleImgSfx[0].gameObject.SetActive(true);
            PlayerDataManager.SetSound(!isOn);
        }

        SoundManager.Instance.SettingFxSound(PlayerDataManager.GetSound());
    }


    private void OnClosePopup()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        AnimationUIController.ClosePopUp();
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.ClosePopup);
    }

    private void OnBackGame()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        AnimationUIController.ClosePopUp();
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.ClosePopup);
    }


    private void ResetTurnMusic()
    {
        for (int i = 0; i < L_toogleImgMusic.Count; i++)
        {
            L_toogleImgMusic[i].gameObject.SetActive(false);
        }
    }

    private void ResetTurnSfx()
    {
        for (int i = 0; i < L_toogleImgSfx.Count; i++)
        {
            L_toogleImgSfx[i].gameObject.SetActive(false);
        }
    }

    private void RemoveAllButton()
    {
        btn_toogleMusic.onClick.RemoveListener(OnToogleMusic);
        btn_sfx.onClick.RemoveListener(OnToogleSfx);

        btn_BackGame.onClick.RemoveListener(OnBackGame);
        btn_ok.onClick.RemoveListener(OnClosePopup);


    }

    private void OnDisable()
    {
        RemoveAllButton();
    }
}
