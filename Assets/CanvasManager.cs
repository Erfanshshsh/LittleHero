using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject star;
    public GameObject iceCream;
    public VoidChannelEventSO onGetStar;
    public VoidChannelEventSO onGetIceCream;

    private void Awake()
    {
        star.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        onGetStar.OnEventRaised += InstantiateStar;
        onGetIceCream.OnEventRaised += EnableIceCream;
    }


    private void OnDisable()
    {
        onGetStar.OnEventRaised -= InstantiateStar;
        onGetIceCream.OnEventRaised -= EnableIceCream;
    }
    
    
    private void InstantiateStar()
    {
        GameObject starInstance = Instantiate(star, star.transform.parent);
        starInstance.gameObject.SetActive(true);
        StaticTweeners.AnimateUp(starInstance.transform, 1f, GS.INS.CBButtonsAnimateTime, GS.INS.CBButtonsOnEase);
    }
    
    private void EnableIceCream()
    {
        iceCream.SetActive(true);
        StaticTweeners.AnimateUp(iceCream.transform, 1f, GS.INS.CBButtonsAnimateTime, GS.INS.CBButtonsOnEase);
    }


}
