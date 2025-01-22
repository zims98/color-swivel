using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

    [RequireComponent(typeof(RectTransform))]
    public class ShopUIManager : MonoBehaviour
    {

        RectTransform rectTransform;

        static ShopUIManager instance;
        public static ShopUIManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<ShopUIManager>();
                //if (instance == null)
                //Debug.LogError("SettingsUIManager not found");
                return instance;
            }
        }

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.DOAnchorPosY(rectTransform.rect.width * -2, 0f);
        }

        public void Show(float delay = 0f)
        {
            rectTransform.DOAnchorPosY(0, 0.3f).SetDelay(delay);
        }

        public void Hide(float delay = 0f)
        {
            rectTransform.DOAnchorPosY(rectTransform.rect.width * -2, 0.3f).SetDelay(delay);
        }

        public void ShowHomeMenu()
        {
            Hide();
            HomeUIManager.Instance.ShowVertical();
            //HomeUIManager.Instance.darkTintShown = false;
        }

    }
