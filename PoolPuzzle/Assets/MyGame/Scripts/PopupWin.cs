using DG.Tweening;
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

        AimBtn();

    }
    public void AimBtn()
    {
        btn_nextLevel.transform.localScale = Vector3.one;
        btn_nextLevel.transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnNextLevel()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        GameManager.instance.IncreaseLevel(GameManager.instance.LevelPlaying);

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.ClosePopup);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
    }
}
