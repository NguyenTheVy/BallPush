using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimUI
{
    public void Open(Transform content, float duration);
    public void Close(Transform content, float duration);
}
