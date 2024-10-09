using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimBase : MonoBehaviour
{
    public abstract void Open(RectTransform content, float duration);
    public abstract void Close(RectTransform content, float duration);
}
