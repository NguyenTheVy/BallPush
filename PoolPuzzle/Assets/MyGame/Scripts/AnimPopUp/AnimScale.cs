using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimScale : AnimBase
{
    [SerializeField] private AnimationCurve animCurveOpen;
    [SerializeField] private AnimationCurve animCurveClose;
    private Tweener tweenScaleOpen;
    private Tweener tweenScaleClose;
    public override void Open(RectTransform content, float duration)
    {
        content.localScale = Vector3.one * 0.4f;
        tweenScaleOpen = content.DOScale(Vector3.one, duration)
            .SetUpdate(true)
            .SetEase(animCurveOpen);
    }
    public override void Close(RectTransform content, float duration)
    {
        Vector3 theScale = Vector3.one * 0.4f;
        tweenScaleClose = content.DOScale(theScale, duration)
           .SetUpdate(true)
           .SetEase(animCurveClose)
           .OnComplete(() =>
           {
               gameObject.SetActive(false);
           });
    }
    private void OnDisable()
    {
        tweenScaleClose?.Kill(true);
        tweenScaleOpen?.Kill(true);
    }
}
