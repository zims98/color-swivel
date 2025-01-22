using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class SettingsUIManager : MonoBehaviour
{

    RectTransform rectTransform;

    static SettingsUIManager instance;
    public static SettingsUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SettingsUIManager>();
            //if (instance == null)
                //Debug.LogError("SettingsUIManager not found");
            return instance;
        }
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosX(rectTransform.rect.width * -1, 0f);
    }

    public void Show(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(0, 0.3f).SetDelay(delay);
    }

    public void Hide(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(rectTransform.rect.width * -1, 0.3f).SetDelay(delay);
    }

    public void ShowHomeMenu()
    {
        Hide();
        HomeUIManager.Instance.Show();
    }

}
