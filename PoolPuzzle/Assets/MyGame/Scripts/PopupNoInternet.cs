using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupNoInternet : MonoBehaviour
{
    Tweener ScaleTween;
    private void OnEnable()
    {
        OpenScaleAnim();

        StartCoroutine(IE_DelayDeActive());
    }


    public void OpenScaleAnim()
    {
        Vector3 Scale = transform.localScale * 0.4f;
        transform.localScale = Scale;

        ScaleTween = transform.DOScale(1f, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
    }

    IEnumerator IE_DelayDeActive()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        ScaleTween?.Kill();
    }
}
