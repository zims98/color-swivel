using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 panelLocation;
    public float percentThreshold = 0.2f;

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.localPosition = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;

        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;

            if (percentage > 0)
            {
                HomeUIManager.Instance.HideFromStats();
                StatsUIManager.Instance.Show();
            }
            else if (percentage < 0)
            {               
                HomeUIManager.Instance.HideFromSettings();
                SettingsUIManager.Instance.Show();
            }

            transform.localPosition = newLocation;
            panelLocation = newLocation;
        }
        else
        {
            transform.localPosition = panelLocation;
        }
    }

}
