using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupLose : MonoBehaviour
{
    [SerializeField] private Button btn_Replay;

    private void Start()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Lose);
        btn_Replay.onClick.AddListener(OnReplay);
    }
    private void OnEnable()
    {
        btn_Replay.transform.localScale = Vector3.one;
        btn_Replay.transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnReplay()
    {
        if(AdsController.ins.TimeShowInter >= 120)
        {
            AdManager.instance.ShowInter(()=>
            {
                AdsController.ins.TimeShowInter = 0;
                SceneManager.LoadScene(1);
            }, ()=>
            {
                AdsController.ins.TimeShowInter = 0;
                SceneManager.LoadScene(1);
            }, "showInter");
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
    }
}
