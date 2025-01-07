using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContlorer : MonoBehaviour
{
    float doubleClickTimer;
    public float doubleClickTimerMax = 0.5f;
    bool isClickedOnce;
    void Awake()
    {
        doubleClickTimer = doubleClickTimerMax;
        isClickedOnce=false;
    }
    void Update ()
    {
        if(isClickedOnce)
        {
            doubleClickTimer -= Time.deltaTime;
            if(doubleClickTimer <=0f) 
            {
                isClickedOnce = false;
                doubleClickTimer= doubleClickTimerMax;
            }
            
        }
    }
    public void Open(GameObject shop)
    {
        if(isClickedOnce)
        {
            shop.SetActive(true);
            isClickedOnce = false;
            doubleClickTimer= doubleClickTimerMax;
        }
        else
        {
            isClickedOnce=true;
        }

    }
    public void Close(GameObject shop)
    {
        shop.SetActive(false);
    }

}
