using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image fill_img;
    [SerializeField] private TMP_Text loading_txt;

    [Header("Icon")]
    [SerializeField] private Image Icon;
    [SerializeField] private CanvasGroup Light;

    Tweener T_Loading, T_Icon, T_light;

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        AnimIcon();
       // AnimLight();

        fill_img.fillAmount = 0f;
        OnLoading();

    }

    private void Ontest()
    {
        Debug.Log("test");
    }

    private void OnLoading()
    {
        T_Loading = fill_img.DOFillAmount(1f, 8f).SetEase(Ease.Linear).OnUpdate(() =>
        {
            loading_txt.text = $"Loading {(int)(fill_img.fillAmount * 100)}%";
        }).OnComplete(() =>
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        });
    }

    private void AnimIcon()
    {
        T_Icon = Icon.transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void AnimLight()
    {
        //T_light = Light.DOFade(0.7f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        T_Loading?.Kill();
        T_Icon?.Kill();
        T_light?.Kill();
    }
}
