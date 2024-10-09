using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTopDown : AnimBase
{
    public float PosY;
    [SerializeField] private AnimationCurve OpenCurve;
    [SerializeField] private AnimationCurve CloseCurve;

    private Tweener TweenOpen;
    private Tweener TweenClose;

    public override void Open(RectTransform Content, float Duration)
    {
        Vector3 ThePos = Content.anchoredPosition;
        ThePos.y = -2000;
        Content.localPosition = ThePos;

        TweenOpen = Content.DOAnchorPosY(PosY, Duration).SetEase(OpenCurve).SetUpdate(true);
    }

    public override void Close(RectTransform Content, float Duration)
    {
        TweenClose = Content.transform.DOMoveY(Content.anchoredPosition.y - 1000, Duration).SetUpdate(true).SetEase(CloseCurve).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }


    private void OnDisable()
    {
        TweenOpen?.Kill();
        TweenClose?.Kill();
    }
}
