using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animArrow : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalMoveY(0.2f, 0.7f).SetLoops(-1, LoopType.Restart);
    }

    private void Update()
    {
        if(GameManager.instance.CurrentLevel.IsMoving)
        {
            DOTween.Kill(transform);
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        DOTween.Kill(transform);
    }
}
