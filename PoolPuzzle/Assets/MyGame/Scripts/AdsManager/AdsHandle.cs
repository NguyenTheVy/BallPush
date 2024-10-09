using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class AdsHandle : MonoBehaviour
{
    public static AdsHandle instance;

    [Header("inter Always")]
    public bool CanShowInterAlways;

    [Header("interAFK")]
    public bool Detected;
    public bool CanShowInterAFK;

    [Header("inter back")]
    public bool canShowInterBack;

    [Header("Go Back Inter")]
    public bool CanShowGoBackInter;

    Coroutine coAFK, coAlways, CoAftertime, coGoBackInter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        ShowAllAds();
    }

    public void InterAlwaysGamePlay()
    {
        /*        if (!GameManager.instance.isOpenShop)
                {
                    coAlways = StartCoroutine(IE_CountTimeShowPlayInter(40));
                }*/
    }


    public IEnumerator IE_CountTimeShowPlayInter(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        CanShowInterAlways = true;
        //GameManager.instance.UiController.PanelAds.gameObject.SetActive(true);
    }

    public void StopInterAlwaysGamePlay()
    {
        /*        if (coAlways != null)
                {
                    CanShowInterAlways = false;
                    StopCoroutine(coAlways);
                }*/
    }


    public void ShowInterAFK()
    {
        if (Detected || coAFK != null)
        {
            StopIEShowInterAFK();
        }

        Detected = false;

        coAFK = StartCoroutine(IE_InterAFK(25));
    }


    public void StopIEShowInterAFK()
    {
        CanShowInterAFK = false;
        if (coAFK != null)
        {
            Debug.Log("Stop adsAFK");
            StopCoroutine(coAFK);
        }
    }

    public IEnumerator IE_InterAFK(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        CanShowInterAFK = true;

/*        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            UiHome.Instance.remainingShowAds.gameObject.SetActive(true);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameManager.instance.UiController.UiGamePlay.remainingShowAds.gameObject.SetActive(true);
        }*/
    }


    public void ShowInterAfterTime()
    {
        CoAftertime = StartCoroutine(IE_InterAfterTime(40));
    }

    public void StopInterAfterTime()
    {
        if (CoAftertime != null)
        {
            canShowInterBack = false;
            StopCoroutine(CoAftertime);
        }

        CoAftertime = StartCoroutine(IE_InterAfterTime(40));
    }

    public IEnumerator IE_InterAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        canShowInterBack = true;
    }

    public void ChangeInterAFK()
    {
        StartCoroutine(IE_DelayChangeCanShowAds());
    }
    IEnumerator IE_DelayChangeCanShowAds()
    {
        yield return new WaitForSeconds(0.5f);
        CanShowInterAFK = false;
    }

    public void ShowAllAds()
    {
        Debug.Log("show All");

        //ShowGobBackInter();
        //InterAlwaysGamePlay();
        //ShowInterAfterTime();
        ShowInterAFK();
    }

    public void ShowGobBackInter()
    {
        coGoBackInter = StartCoroutine(IE_GoBackIner(40f));
    }
    public void StopGobBackInter()
    {
        if (coGoBackInter != null)
        {
            CanShowGoBackInter = false;
            StopCoroutine(coGoBackInter);
        }

        //StartCoroutine(IE_GoBackIner(40));
    }

    public IEnumerator IE_GoBackIner(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        CanShowGoBackInter = true;
        //GameManager.instance.UiController.PanelAds.gameObject.SetActive(true);
    }



    public void ResetAds()
    {
        Debug.Log("reset All");
        /*        if(GameManager.instance.UiController.PanelAds.gameObject.activeSelf)
                {
                    GameManager.instance.UiController.PanelAds.gameObject.SetActive(false);
                }*/
/*
        if(PlayerDataManager.GetCurrentLevel() >= 2)
        {
            StopInterAfterTime();
        }*/

        StopInterAlwaysGamePlay();
        StopIEShowInterAFK();
        ShowInterAFK();
        StopGobBackInter();
    }
}
