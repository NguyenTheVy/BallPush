using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : MonoBehaviour
{
    [SerializeField] private Button btn_nextLevel;

    private void Start()
    {
        btn_nextLevel.onClick.AddListener(OnNextLevel);
    }
    private void OnEnable()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Win);
    }

    private void OnNextLevel()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        GameManager.instance.IncreaseLevel(GameManager.instance.LevelPlaying);

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.ClosePopup);
        gameObject.SetActive(false);
    }
}
