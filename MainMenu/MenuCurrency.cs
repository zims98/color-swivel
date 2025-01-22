using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuCurrency : MonoBehaviour
{

    public TextMeshProUGUI currencyText;
    public int displayCurrency; // Reference in Shop Manager

    float timer = 0f;
    public float duration = 2f;

    void Start()
    {
        Currency.myCurrency = ES3.Load<int>("myCurrency", 0);
        currencyText.text = "<sprite index=0>" + Currency.myCurrency.ToString();
    }

    public IEnumerator ConsumeCurrency()
    {
        while (true)
        {
            if (displayCurrency > Currency.myCurrency)
            {
                if (timer >= duration)
                {
                    timer = 0f;
                    break;
                }

                if (timer <= duration)
                {
                    timer += Time.deltaTime;

                    int finalCurrency = Mathf.CeilToInt(Mathf.Lerp(displayCurrency, Currency.myCurrency, (timer / duration)));

                    currencyText.text = "<sprite index=0>" + finalCurrency.ToString();
                }             
            }

            yield return null;
        }
    }

}
