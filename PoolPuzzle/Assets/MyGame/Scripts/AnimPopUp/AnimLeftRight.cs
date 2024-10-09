using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimLeftRight : AnimBase
{
    [SerializeField] private E_TypeAnim TypeAnim;
    public float PosX;
    [SerializeField] private AnimationCurve OpenCurve;
    [SerializeField] private AnimationCurve CloseCurve;

    private Tweener TweenOpen;
    private Tweener TweenClose;

    public override void Close(RectTransform Content, float Duration)
    {
        if (TypeAnim == E_TypeAnim.Right_To_Left)
        {
            TweenClose = Content.transform.DOLocalMoveX(Content.position.x + 1000, Duration).SetUpdate(true).SetEase(CloseCurve).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

        if (TypeAnim == E_TypeAnim.Left_To_Right)
        {
            TweenClose = Content.transform.DOLocalMoveX(Content.position.x - 1000, Duration).SetUpdate(true).SetEase(CloseCurve).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    public override void Open(RectTransform Content, float Duration)
    {
        if (TypeAnim == E_TypeAnim.Right_To_Left)
        {
            Vector3 ContentPosition = Content.localPosition;

            ContentPosition.x += 1000;

            Content.localPosition = ContentPosition;

            TweenOpen = Content.transform.DOLocalMoveX(PosX, Duration).SetUpdate(true).SetEase(OpenCurve);
        }

        if (TypeAnim == E_TypeAnim.Left_To_Right)
        {
            Vector3 ContentPosition = Content.localPosition;

            ContentPosition.x -= 1000;

            Content.localPosition = ContentPosition;

            TweenOpen = Content.transform.DOLocalMoveX(PosX, Duration).SetUpdate(true).SetEase(OpenCurve);
        }
    }

    private void OnDisable()
    {
        TweenOpen?.Kill();
        TweenClose?.Kill();
    }
}

public enum E_TypeAnim
{
    Left_To_Right,
    Right_To_Left,
}
