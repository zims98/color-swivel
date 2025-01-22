using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HomeUIManager : MonoBehaviour
{
    //public bool darkTintShown;
    //public Animator darkBGAnim;

    RectTransform rectTransform;

    public MainMenu mainMenuScript;

    static HomeUIManager instance;
    public static HomeUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HomeUIManager>();
            //if (instance == null)
                //Debug.LogError("HomeUIManager not found");
            return instance;
        }
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosX(0, 0f);
    }

    private void Update()
    {
        /*if (darkTintShown)
        {
            StartCoroutine(AnimDelay());
        }            
        else
        {
            darkBGAnim.SetBool("showDarkTint", false);          
        }*/
            
    }

    /*IEnumerator AnimDelay()
    {
        yield return new WaitForSeconds(0.25f);
        darkBGAnim.SetBool("showDarkTint", true);
    }*/

    public void Show(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(0, 0.3f).SetDelay(delay);
    }

    public void ShowVertical(float delay = 0f)
    {
        rectTransform.DOAnchorPosY(0, 0.3f).SetDelay(delay);
    }

    public void HideFromSettings(float delay = 0f)
    {
        if (mainMenuScript.buttonsInteractable)
            rectTransform.DOAnchorPosX(rectTransform.rect.width, 0.3f).SetDelay(delay);
    }

    public void HideFromStats(float delay = 0f)
    {
        if (mainMenuScript.buttonsInteractable)
            rectTransform.DOAnchorPosX(rectTransform.rect.width * -1, 0.3f).SetDelay(delay);
    }
    
    public void HideFromShop(float delay = 0f)
    {
        if (mainMenuScript.buttonsInteractable)
            rectTransform.DOAnchorPosY(rectTransform.rect.width * 2, 0.3f).SetDelay(delay);
    }

    public void ShowSettingsMenu()
    {
        if (mainMenuScript.buttonsInteractable)
        {
            HideFromSettings();
            SettingsUIManager.Instance.Show();
        }         
    }

    public void ShowStatsMenu()
    {
        if (mainMenuScript.buttonsInteractable)
        {
            HideFromStats();
            StatsUIManager.Instance.Show();
        }       
    }

    public void ShowShopMenu()
    {
        if (mainMenuScript.buttonsInteractable)
        {
            HideFromShop();
            ShopUIManager.Instance.Show();
        }        
    }

}
