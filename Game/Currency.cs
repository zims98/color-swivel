using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    public float duration = 1f;
    public float timer = 0f;

    public static int myCurrency;
    public int currentDisplayCurrency;
    public int finalDisplayCurrency;

    public TextMeshProUGUI currencyText;
    [Tooltip("Currency gained after every score, increases every 5th score")]
    public int currencyToGive = 1;
    public int totalCurrencyToGive;

    public TextMeshProUGUI currencyToGiveText;

    public Animator currencyAnim;

    void Start()
    {
        if (PlayerBox.extraLifeConsumed == true)
        {
            currencyToGive = ES3.Load<int>("currencyToGive", 0);
            totalCurrencyToGive = ES3.Load<int>("totalCurrencyToGive", 0);
        }        

        finalDisplayCurrency = ES3.Load<int>("finalDisplayCurrency", 0);
        myCurrency = ES3.Load<int>("myCurrency", 0);

        finalDisplayCurrency = myCurrency;

        currentDisplayCurrency = finalDisplayCurrency;
        currentDisplayCurrency = ES3.Load<int>("currentDisplayCurrency", 0);       
    }

    public IEnumerator CurrencyUpdater()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (currentDisplayCurrency < myCurrency)
            {
                if (timer <= duration)
                {
                    timer += Time.deltaTime;

                    currencyAnim.SetBool("CurrencyIncreasing", true);

                    int finalCurrency = Mathf.CeilToInt(Mathf.Lerp(currentDisplayCurrency, finalDisplayCurrency, (timer / duration)));

                    currencyText.text = "<sprite index=0>" + finalCurrency.ToString();
                }
                if (timer >= duration)
                {
                    timer = duration;
                    currencyAnim.SetBool("CurrencyIncreasing", false);
                }               
            }
            else
            {
                currencyAnim.SetBool("CurrencyIncreasing", false);
                currencyText.text = "<sprite index=0>" + currentDisplayCurrency.ToString();
            }

            yield return null;
        }
    }

}
