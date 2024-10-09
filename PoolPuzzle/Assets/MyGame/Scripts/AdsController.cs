using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsController : MonoBehaviour
{
    public static AdsController ins;

    public float TimeShowInter;

    private void Awake()
    {
        if (ins == null)
            ins = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (TimeShowInter >= 120 || SceneManager.GetActiveScene().buildIndex == 0) return;
        TimeShowInter += Time.deltaTime;
    }
}
