using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimTopDown : AnimBase
{
    [SerializeField] private float posY;
    [SerializeField] private AnimationCurve animCurveOpen;
    [SerializeField] private AnimationCurve animCurveClose;

    private Tweener tweenScaleOpen;
    private Tweener tweenScaleClose;
    public override void Open(RectTransform content, float duration)
    {
        Vector3 ThePos = content.localPosition;
        ThePos.y = -500;
        content.localPosition = ThePos;
        tweenScaleOpen = content.DOLocalMoveY(posY, duration)
            .SetUpdate(true)
            .SetEase(animCurveOpen);
    }
    public override void Close(RectTransform content, float duration)
    {
        tweenScaleClose = content.DOLocalMoveY(-1000, duration)
           .SetUpdate(true)
           .SetEase(animCurveClose)
           .OnComplete(() =>
           {
               DestroyImmediate(gameObject);
           });
    }
    private void OnDisable()
    {
        tweenScaleClose?.Kill(true);
        tweenScaleOpen?.Kill(true);
    }
}
