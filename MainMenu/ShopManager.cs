using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Unlockables unlockableScript;
    public MenuCurrency menuCurrencyScript;
    public PlayerDeathPreview deathPreviewScript;
    public DissolvePreview playerDissolveScript;
    public ObstacleExplosionPreview obstacleExplosionScript;
    public PlayerPreviewRaycast playerPreviewRaycast;
    public RotateRightWithDelay rotateRightWithDelayScript;

    bool itemOnePreviewed, itemTwoPreviewed, itemThreePreviewed, itemFourPreviewed, itemFivePreviewed, itemSixPreviewed, itemSevenPreviewed, itemEightPreviewed,
        itemNinePreviewed, itemTenPreviewed, itemElevenPreviewed, itemTwelvePreviewed;

    bool buttonsInteractable = true;

    public GameObject playerPreview;
    public GameObject playerRipple;
    public GameObject circleCore, starCore, squareCore, triangleCore;
    public GameObject obstacleObject;
    public GameObject playerDissolveObject;

    public GameObject blackHolePlayer;

    public GameObject playerOrbitingBall;

    public GameObject playerRotateTrail;

    public Image itemFrameOne, itemFrameTwo, itemFrameThree, itemFrameFour, itemFrameFive, itemFrameSix, itemFrameSeven, itemFrameEight,
        itemFrameNine, itemFrameTen, itemFrameEleven, itemFrameTwelve;
    public Sprite borderFrame;
    public Sprite normalFrame;
    public Animator frameAnim;

    public TextMeshProUGUI itemPreviewCostText;
    int itemCost;

    public TextMeshProUGUI itemTextDescription;

    public GameObject homeButton;
    public GameObject previewWindow;
    public Animator darkTintAnim;
    public Animator previewWindowAnim;

    public GameObject coreCircleTextCost, coreStarTextCost, coreSquareTextCost, coreTriangleTextCost; // Not needed?
    public GameObject obstacleTrailTextCost, playerRippleTextCost, playerDeathTextCost, playerDissolveTextCost;
    public GameObject obstacleExplosionTextCost, playerBlackHoleTextCost, playerOrbitingBallTextCost, playerRotateTrailTextCost;

    public GameObject coreCircleCheckmark, coreStarCheckmark, coreSquareCheckmark, coreTriangleCheckmark;
    public GameObject obstacleTrailCheckmark, playerRippleCheckmark, playerDeathCheckmark, playerDissolveCheckmark;
    public GameObject obstacleExplosionCheckmark, playerBlackHoleCheckmark, playerOrbitingBallCheckmark, playerRotateTrailCheckmark;

    private IEnumerator explosionCoroutine;
    private IEnumerator playerDissolveCoroutine;
    IEnumerator obstacleExplosionCoroutine;
    

    void Start()
    {
        LoadSelectionStatus();
        LoadUnlockStatus();
    }

    void Update()
    {
        UpdateSelectionStatus();
        UpdateUnlockStatus();

        // RESET ALL, DEBUGGING, REMOVE BEFORE PUBLISH
        if (Input.GetKeyDown(KeyCode.R))
        {
            unlockableScript.coreCircleUnlocked = false;
            unlockableScript.coreStarUnlocked = false;
            unlockableScript.coreSquareUnlocked = false;
            unlockableScript.coreTriangleUnlocked = false;

            unlockableScript.coreCircleSelected = false;
            unlockableScript.coreStarSelected = false;
            unlockableScript.coreSquareSelected = false;
            unlockableScript.coreTriangleSelected = false;

            unlockableScript.obstacleTrailUnlocked = false;
            unlockableScript.playerRippleUnlocked = false;
            unlockableScript.playerDeathUnlocked = false;
            unlockableScript.playerDissolveUnlocked = false;

            unlockableScript.obstacleTrailSelected = false;
            unlockableScript.playerRippleSelected = false;
            unlockableScript.playerDeathSelected = false;
            unlockableScript.playerDissolveSelected = false;

            Currency.myCurrency = 0;

            SaveSelectionStatus();
            ES3.Save("myCurrency", Currency.myCurrency);

            ES3.Save("coreCircleUnlocked", unlockableScript.coreCircleUnlocked);
            ES3.Save("coreStarUnlocked", unlockableScript.coreStarUnlocked);
            ES3.Save("coreSquareUnlocked", unlockableScript.coreSquareUnlocked);
            ES3.Save("coreTriangleUnlocked", unlockableScript.coreTriangleUnlocked);

            ES3.Save("obstacleTrailUnlocked", unlockableScript.obstacleTrailUnlocked);
            ES3.Save("playerRippleUnlocked", unlockableScript.playerRippleUnlocked);
            ES3.Save("playerDeathUnlocked", unlockableScript.playerDeathUnlocked);
            ES3.Save("playerDissolveUnlocked", unlockableScript.playerDissolveUnlocked);

            LoadSelectionStatus();
            LoadUnlockStatus();
            Currency.myCurrency = ES3.Load<int>("myCurrency", 0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Currency.myCurrency = 20000;
            ES3.Save("myCurrency", Currency.myCurrency);
            Currency.myCurrency = ES3.Load<int>("myCurrency", 0);
        }
        
    }

    #region Update Unlock Status
    void UpdateUnlockStatus()
    {
        // Core Circle
        if (unlockableScript.coreCircleUnlocked)
        {
            coreCircleTextCost.SetActive(false);
            coreCircleCheckmark.SetActive(true);
        }
        else
        {
            coreCircleTextCost.SetActive(true);
            coreCircleCheckmark.SetActive(false);
        }

        // Core Star
        if (unlockableScript.coreStarUnlocked)
        {
            coreStarTextCost.SetActive(false);
            coreStarCheckmark.SetActive(true);
        }
        else
        {
            coreStarTextCost.SetActive(true);
            coreStarCheckmark.SetActive(false);
        }

        // Core Square
        if (unlockableScript.coreSquareUnlocked)
        {
            coreSquareTextCost.SetActive(false);
            coreSquareCheckmark.SetActive(true);
        }
        else
        {
            coreSquareTextCost.SetActive(true);
            coreSquareCheckmark.SetActive(false);
        }

        // Core Triangle
        if (unlockableScript.coreTriangleUnlocked)
        {
            coreTriangleTextCost.SetActive(false);
            coreTriangleCheckmark.SetActive(true);
        }
        else
        {
            coreTriangleTextCost.SetActive(true);
            coreTriangleCheckmark.SetActive(false);
        }

        // Obstacle Trail
        if (unlockableScript.obstacleTrailUnlocked)
        {
            obstacleTrailTextCost.SetActive(false);
            obstacleTrailCheckmark.SetActive(true);
        }
        else
        {
            obstacleTrailTextCost.SetActive(true);
            obstacleTrailCheckmark.SetActive(false);
        }

        // Player Ripple
        if (unlockableScript.playerRippleUnlocked)
        {
            playerRippleTextCost.SetActive(false);
            playerRippleCheckmark.SetActive(true);
        }
        else
        {
            playerRippleTextCost.SetActive(true);
            playerRippleCheckmark.SetActive(false);
        }

        // Player Death
        if (unlockableScript.playerDeathUnlocked)
        {
            playerDeathTextCost.SetActive(false);
            playerDeathCheckmark.SetActive(true);
        }
        else
        {
            playerDeathTextCost.SetActive(true);
            playerDeathCheckmark.SetActive(false);
        }

        // Player Dissolve
        if (unlockableScript.playerDissolveUnlocked)
        {
            playerDissolveTextCost.SetActive(false);
            playerDissolveCheckmark.SetActive(true);
        }
        else
        {
            playerDissolveTextCost.SetActive(true);
            playerDissolveCheckmark.SetActive(false);
        }

        // Obstacle Explosion
        if (unlockableScript.obstacleExplosionUnlocked)
        {
            obstacleExplosionTextCost.SetActive(false);
            obstacleExplosionCheckmark.SetActive(true);
        }
        else
        {
            obstacleExplosionTextCost.SetActive(true);
            obstacleExplosionCheckmark.SetActive(false);
        }

        // Player Black Hole
        if (unlockableScript.playerBlackHoleUnlocked)
        {
            playerBlackHoleTextCost.SetActive(false);
            playerBlackHoleCheckmark.SetActive(true);
        }
        else
        {
            playerBlackHoleTextCost.SetActive(true);
            playerBlackHoleCheckmark.SetActive(false);
        }

        // Player Orbiting Ball
        if (unlockableScript.playerOrbitingBallUnlocked)
        {
            playerOrbitingBallTextCost.SetActive(false);
            playerOrbitingBallCheckmark.SetActive(true);
        }
        else
        {
            playerOrbitingBallTextCost.SetActive(true);
            playerOrbitingBallCheckmark.SetActive(false);
        }

        // Player Rotate Trail
        if (unlockableScript.playerTrailEffectUnlocked)
        {
            playerRotateTrailTextCost.SetActive(false);
            playerRotateTrailCheckmark.SetActive(true);
        }
        else
        {
            playerRotateTrailTextCost.SetActive(true);
            playerRotateTrailCheckmark.SetActive(false);
        }
    }
    #endregion

    #region Update Selection Status
    void UpdateSelectionStatus()
    {
        if (unlockableScript.coreTriangleSelected) // Triangle
        {
            itemFrameOne.sprite = borderFrame;
        }
        else if (!unlockableScript.coreTriangleSelected)
        {
            itemFrameOne.sprite = normalFrame;
        }

        if (unlockableScript.coreStarSelected) // Star
        {
            itemFrameTwo.sprite = borderFrame;
        }
        else if (!unlockableScript.coreStarSelected)
        {
            itemFrameTwo.sprite = normalFrame;
        }

        if (unlockableScript.coreCircleSelected) // Circle
        {
            itemFrameThree.sprite = borderFrame;
        }
        else if (!unlockableScript.coreCircleSelected)
        {
            itemFrameThree.sprite = normalFrame;
        }

        if (unlockableScript.coreSquareSelected) // Square
        {
            itemFrameFour.sprite = borderFrame;            
        }
        else if (!unlockableScript.coreSquareSelected)
        {
            itemFrameFour.sprite = normalFrame;
        }

        if (unlockableScript.obstacleTrailSelected) // Obstacle Trail
        {
            itemFrameFive.sprite = borderFrame;
        }
        else if (!unlockableScript.obstacleTrailSelected)
        {
            itemFrameFive.sprite = normalFrame;
        }
        
        if (unlockableScript.playerDeathSelected) // Player Death Effect
        {
            itemFrameSix.sprite = borderFrame;
        }
        else if (!unlockableScript.playerDeathSelected)
        {
            itemFrameSix.sprite = normalFrame;
        }

        if (unlockableScript.playerDissolveSelected) // Player Dissolve Effect
        {
            itemFrameSeven.sprite = borderFrame;
        }
        else if (!unlockableScript.playerDissolveSelected)
        {
            itemFrameSeven.sprite = normalFrame;
        }

        if (unlockableScript.playerRippleSelected) // Player Ripple
        {
            itemFrameEight.sprite = borderFrame;
        }
        else if (!unlockableScript.playerRippleSelected)
        {
            itemFrameEight.sprite = normalFrame;
        }

        if (unlockableScript.obstacleExplosionSelected) // Obstacle Explosion
        {
            itemFrameNine.sprite = borderFrame;
        }
        else if (!unlockableScript.obstacleExplosionSelected)
        {
            itemFrameNine.sprite = normalFrame;
        }

        if (unlockableScript.playerBlackHoleSelected) // Player Black Hole
        {
            itemFrameTen.sprite = borderFrame;
        }
        else if (!unlockableScript.playerBlackHoleSelected)
        {
            itemFrameTen.sprite = normalFrame;
        }

        if (unlockableScript.playerOrbitingBallSelected) // Player Orbiting Ball
        {
            itemFrameEleven.sprite = borderFrame;
        }
        else if (!unlockableScript.playerOrbitingBallSelected)
        {
            itemFrameEleven.sprite = normalFrame;
        }

        if (unlockableScript.playerTrailEffectSelected) // Player Trail Effect
        {
            itemFrameTwelve.sprite = borderFrame;
        }
        else if (!unlockableScript.playerTrailEffectSelected)
        {
            itemFrameTwelve.sprite = normalFrame;
        }
    }
    #endregion

    public void ItemIndexOneButton() // Core TRIANGLE
    {
        if (buttonsInteractable && !unlockableScript.coreTriangleUnlocked)
        {
            itemOnePreviewed = true;
            ShowPreviewWindow();
            itemCost = 500;

            playerPreview.SetActive(true);
            triangleCore.SetActive(true);
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;            
        }

        if (unlockableScript.coreTriangleUnlocked && buttonsInteractable)
        {
            unlockableScript.coreTriangleSelected = !unlockableScript.coreTriangleSelected;
            frameAnim.SetTrigger("FrameOne");

            unlockableScript.coreStarSelected = false;
            unlockableScript.coreSquareSelected = false;
            unlockableScript.coreCircleSelected = false;

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }    

    public void ItemIndexTwoButton() // Core STAR
    {
        if (buttonsInteractable && !unlockableScript.coreStarUnlocked)
        {
            itemTwoPreviewed = true;
            ShowPreviewWindow();
            itemCost = 500;

            playerPreview.SetActive(true);
            starCore.SetActive(true);
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.coreStarUnlocked && buttonsInteractable)
        {
            unlockableScript.coreStarSelected = !unlockableScript.coreStarSelected;
            frameAnim.SetTrigger("FrameTwo");

            unlockableScript.coreCircleSelected = false;
            unlockableScript.coreSquareSelected = false;
            unlockableScript.coreTriangleSelected = false;

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexThreeButton() // Core CIRCLE
    {
        if (buttonsInteractable && !unlockableScript.coreCircleUnlocked)
        {
            itemThreePreviewed = true;
            ShowPreviewWindow();
            itemCost = 500;

            playerPreview.SetActive(true);
            circleCore.SetActive(true);
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.coreCircleUnlocked && buttonsInteractable)
        {
            unlockableScript.coreCircleSelected = !unlockableScript.coreCircleSelected;
            frameAnim.SetTrigger("FrameThree");

            unlockableScript.coreStarSelected = false;
            unlockableScript.coreSquareSelected = false;
            unlockableScript.coreTriangleSelected = false;

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }

        
    }

    public void ItemIndexFourButton() // Core SQUARE
    {
        if (buttonsInteractable && !unlockableScript.coreSquareUnlocked)
        {
            itemFourPreviewed = true;
            ShowPreviewWindow();
            itemCost = 500;

            playerPreview.SetActive(true);
            squareCore.SetActive(true);
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.coreSquareUnlocked && buttonsInteractable)
        {
            unlockableScript.coreSquareSelected = !unlockableScript.coreSquareSelected;
            frameAnim.SetTrigger("FrameFour");

            unlockableScript.coreStarSelected = false;
            unlockableScript.coreCircleSelected = false;
            unlockableScript.coreTriangleSelected = false;

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
        
    }

    public void ItemIndexFiveButton() // Obstacle Trail
    {
        if (buttonsInteractable && !unlockableScript.obstacleTrailUnlocked)
        {
            itemFivePreviewed = true;
            ShowPreviewWindow();
            itemCost = 2500;

            playerPreview.SetActive(false);
            obstacleObject.SetActive(true);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "OBSTACLE TRAIL EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.obstacleTrailUnlocked && buttonsInteractable)
        {
            unlockableScript.obstacleTrailSelected = !unlockableScript.obstacleTrailSelected;
            frameAnim.SetTrigger("FrameFive");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexSixButton() // Explosion Effect
    {
        if (buttonsInteractable && !unlockableScript.playerDeathUnlocked)
        {
            itemSixPreviewed = true;
            ShowPreviewWindow();
            itemCost = 2500;

            explosionCoroutine = deathPreviewScript.PreviewPlayerDeath();
            StartCoroutine(explosionCoroutine);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "PLAYER EXPLOSION EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.playerDeathUnlocked && buttonsInteractable)
        {
            unlockableScript.playerDeathSelected = !unlockableScript.playerDeathSelected;
            frameAnim.SetTrigger("FrameSix");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexSevenButton() // Dissolve
    {
        if (buttonsInteractable && !unlockableScript.playerDissolveUnlocked)
        {
            itemSevenPreviewed = true;
            ShowPreviewWindow();
            itemCost = 2500;

            playerDissolveObject.SetActive(true);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "PLAYER EMERGE EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.playerDissolveUnlocked && buttonsInteractable)
        {
            unlockableScript.playerDissolveSelected = !unlockableScript.playerDissolveSelected;
            frameAnim.SetTrigger("FrameSeven");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexEightButton() // Ripple Effect
    {
        if (buttonsInteractable && !unlockableScript.playerRippleUnlocked)
        {
            itemEightPreviewed = true;
            ShowPreviewWindow();
            itemCost = 2500;

            playerPreview.SetActive(true);
            playerRipple.SetActive(true);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "PLAYER VISUAL EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.playerRippleUnlocked && buttonsInteractable)
        {
            unlockableScript.playerRippleSelected = !unlockableScript.playerRippleSelected;
            frameAnim.SetTrigger("FrameEight");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }       
    }

    public void ItemIndexNineButton() // Obstacle Explosion
    {
        if (buttonsInteractable && !unlockableScript.obstacleExplosionUnlocked)
        {
            itemNinePreviewed = true;
            ShowPreviewWindow();
            itemCost = 3500;

            playerPreviewRaycast.enabled = true;
            obstacleExplosionScript.parentObject.SetActive(true);
            obstacleExplosionCoroutine = obstacleExplosionScript.ObstacleExplosion();
            StartCoroutine(obstacleExplosionCoroutine);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "OBSTACLE EXPLOSION EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.obstacleExplosionUnlocked && buttonsInteractable)
        {
            unlockableScript.obstacleExplosionSelected = !unlockableScript.obstacleExplosionSelected;
            frameAnim.SetTrigger("FrameNine");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexTenButton() // Black Hole
    {
        if (buttonsInteractable && !unlockableScript.playerBlackHoleUnlocked)
        {
            itemTenPreviewed = true;
            ShowPreviewWindow();
            itemCost = 3500;

            blackHolePlayer.SetActive(true);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "PLAYER VISUAL EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.playerBlackHoleUnlocked && buttonsInteractable)
        {
            unlockableScript.playerBlackHoleSelected = !unlockableScript.playerBlackHoleSelected;
            frameAnim.SetTrigger("FrameTen");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexElevenButton() // Orbiting Ball
    {
        if (buttonsInteractable && !unlockableScript.playerOrbitingBallUnlocked)
        {
            itemElevenPreviewed = true;
            ShowPreviewWindow();
            itemCost = 3500;

            playerOrbitingBall.SetActive(true);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "PLAYER VISUAL EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.playerOrbitingBallUnlocked && buttonsInteractable)
        {
            unlockableScript.playerOrbitingBallSelected = !unlockableScript.playerOrbitingBallSelected;
            frameAnim.SetTrigger("FrameEleven");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    public void ItemIndexTwelveButton() // Rotate Trail
    {
        if (buttonsInteractable && !unlockableScript.playerTrailEffectUnlocked)
        {
            itemTwelvePreviewed = true;
            ShowPreviewWindow();
            itemCost = 3500;

            playerRotateTrail.SetActive(true);

            itemTextDescription.enabled = true;
            itemTextDescription.text = "PLAYER VISUAL EFFECT";
            itemPreviewCostText.text = "<sprite index=0>" + itemCost;
        }

        if (unlockableScript.playerTrailEffectUnlocked && buttonsInteractable)
        {
            unlockableScript.playerTrailEffectSelected = !unlockableScript.playerTrailEffectSelected;
            frameAnim.SetTrigger("FrameTwelve");

            UpdateSelectionStatus();
            SaveSelectionStatus();
        }
    }

    void ShowPreviewWindow()
    {
        buttonsInteractable = false;
        darkTintAnim.SetBool("showDarkTint", true);
        homeButton.SetActive(false);
        previewWindow.SetActive(true);
        previewWindowAnim.SetTrigger("showPreview");
    }

    public void PurchaseItemButton()
    {
        if (Currency.myCurrency >= itemCost)
        {
            buttonsInteractable = true;
            darkTintAnim.SetBool("showDarkTint", false);
            previewWindow.SetActive(false);
            homeButton.SetActive(true);

            menuCurrencyScript.displayCurrency = Currency.myCurrency;
            Currency.myCurrency -= itemCost;

            ES3.Save("myCurrency", Currency.myCurrency);
           
            StartCoroutine(menuCurrencyScript.ConsumeCurrency());
            itemTextDescription.enabled = false;
                                

            if (itemOnePreviewed) // Core Triangle
            {
                unlockableScript.coreTriangleUnlocked = true;
                unlockableScript.coreTriangleSelected = true;
                frameAnim.SetTrigger("FrameOne");
                itemFrameOne.sprite = borderFrame;
                itemOnePreviewed = false;
                ES3.Save("coreTriangleUnlocked", unlockableScript.coreTriangleUnlocked);

                unlockableScript.coreStarSelected = false;
                unlockableScript.coreSquareSelected = false;
                unlockableScript.coreCircleSelected = false;

                circleCore.SetActive(false);
                starCore.SetActive(false);
                squareCore.SetActive(false);
                triangleCore.SetActive(false);
                playerPreview.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemTwoPreviewed) // Core Star
            {
                unlockableScript.coreStarUnlocked = true;
                unlockableScript.coreStarSelected = true;
                frameAnim.SetTrigger("FrameTwo");
                itemFrameTwo.sprite = borderFrame;
                itemTwoPreviewed = false;
                ES3.Save("coreStarUnlocked", unlockableScript.coreStarUnlocked);

                unlockableScript.coreCircleSelected = false;
                unlockableScript.coreSquareSelected = false;
                unlockableScript.coreTriangleSelected = false;

                circleCore.SetActive(false);
                starCore.SetActive(false);
                squareCore.SetActive(false);
                triangleCore.SetActive(false);
                playerPreview.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemThreePreviewed) // Core Circle
            {
                unlockableScript.coreCircleUnlocked = true;
                unlockableScript.coreCircleSelected = true;
                frameAnim.SetTrigger("FrameThree");
                itemFrameThree.sprite = borderFrame;
                itemThreePreviewed = false;
                ES3.Save("coreCircleUnlocked", unlockableScript.coreCircleUnlocked);

                unlockableScript.coreStarSelected = false;
                unlockableScript.coreSquareSelected = false;
                unlockableScript.coreTriangleSelected = false;

                circleCore.SetActive(false);
                starCore.SetActive(false);
                squareCore.SetActive(false);
                triangleCore.SetActive(false);
                playerPreview.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();                
            }
            if (itemFourPreviewed) // Core Square
            {                
                unlockableScript.coreSquareUnlocked = true;
                unlockableScript.coreSquareSelected = true;
                frameAnim.SetTrigger("FrameFour");
                itemFrameFour.sprite = borderFrame;
                itemFourPreviewed = false;
                ES3.Save("coreSquareUnlocked", unlockableScript.coreSquareUnlocked);

                unlockableScript.coreStarSelected = false;
                unlockableScript.coreCircleSelected = false;
                unlockableScript.coreTriangleSelected = false;

                circleCore.SetActive(false);
                starCore.SetActive(false);
                squareCore.SetActive(false);
                triangleCore.SetActive(false);
                playerPreview.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemFivePreviewed) // Obstacle Trail
            {
                unlockableScript.obstacleTrailUnlocked = true;
                unlockableScript.obstacleTrailSelected = true;
                frameAnim.SetTrigger("FrameFive");
                itemFrameFive.sprite = borderFrame;
                itemFivePreviewed = false;
                ES3.Save("obstacleTrailUnlocked", unlockableScript.obstacleTrailUnlocked);

                obstacleObject.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemSixPreviewed) // Explosion Effect
            {
                unlockableScript.playerDeathUnlocked = true;
                unlockableScript.playerDeathSelected = true;
                frameAnim.SetTrigger("FrameSix");
                itemFrameSix.sprite = borderFrame;
                itemSixPreviewed = false;
                ES3.Save("playerDeathUnlocked", unlockableScript.playerDeathUnlocked);

                if (explosionCoroutine != null)
                    StopCoroutine(explosionCoroutine);
                Destroy(deathPreviewScript.clone);
                deathPreviewScript.playerObject.SetActive(false);

                if (rotateRightWithDelayScript.rotateCoroutine != null)
                {
                    StopCoroutine(rotateRightWithDelayScript.rotateCoroutine);
                    rotateRightWithDelayScript.ResetPosiiton();
                }
                     

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemSevenPreviewed) // Dissolve
            {
                unlockableScript.playerDissolveUnlocked = true;
                unlockableScript.playerDissolveSelected = true;
                frameAnim.SetTrigger("FrameSeven");
                itemFrameSeven.sprite = borderFrame;
                itemSevenPreviewed = false;
                ES3.Save("playerDissolveUnlocked", unlockableScript.playerDissolveUnlocked);

                playerDissolveObject.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemEightPreviewed) // Ripple Effect
            {
                unlockableScript.playerRippleUnlocked = true;
                unlockableScript.playerRippleSelected = true;
                frameAnim.SetTrigger("FrameEight");
                itemFrameEight.sprite = borderFrame;
                itemEightPreviewed = false;
                ES3.Save("playerRippleUnlocked", unlockableScript.playerRippleUnlocked);

                playerRipple.SetActive(false);
                playerPreview.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemNinePreviewed) // Obstacle Explosion
            {
                unlockableScript.obstacleExplosionUnlocked = true;
                unlockableScript.obstacleExplosionSelected = true;
                frameAnim.SetTrigger("FrameNine");
                itemFrameNine.sprite = borderFrame;

                ES3.Save("obstacleExplosionUnlocked", unlockableScript.obstacleExplosionUnlocked);
                itemNinePreviewed = false;
                playerPreviewRaycast.enabled = false;
                obstacleExplosionScript.parentObject.SetActive(false);

                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemTenPreviewed) // Black Hole
            {
                unlockableScript.playerBlackHoleUnlocked = true;
                unlockableScript.playerBlackHoleSelected = true;
                frameAnim.SetTrigger("FrameTen");
                itemFrameTen.sprite = borderFrame;

                ES3.Save("playerBlackHoleUnlocked", unlockableScript.playerBlackHoleUnlocked);
                itemTenPreviewed = false;
                blackHolePlayer.SetActive(false);
                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemElevenPreviewed) // Orbiting Ball
            {
                unlockableScript.playerOrbitingBallUnlocked = true;
                unlockableScript.playerOrbitingBallSelected = true;
                frameAnim.SetTrigger("FrameEleven");
                itemFrameEleven.sprite = borderFrame;

                ES3.Save("playerOrbitingBallUnlocked", unlockableScript.playerOrbitingBallUnlocked);
                playerOrbitingBall.SetActive(false);
                itemElevenPreviewed = false;
                UpdateSelectionStatus();
                SaveSelectionStatus();
            }
            if (itemTwelvePreviewed) // Rotate Trail
            {
                unlockableScript.playerTrailEffectUnlocked = true;
                unlockableScript.playerTrailEffectSelected = true;
                frameAnim.SetTrigger("FrameTwelve");
                itemFrameTwelve.sprite = borderFrame;

                ES3.Save("playerTrailEffectUnlocked", unlockableScript.playerTrailEffectUnlocked);
                playerRotateTrail.SetActive(false);
                itemTwelvePreviewed = false;
                UpdateSelectionStatus();
                SaveSelectionStatus();
            }

            UpdateUnlockStatus();
        }       
    }

    public void CloseWindowButton()
    {
        itemTextDescription.enabled = false;

        // Resets Explosion Preview
        if (explosionCoroutine != null)
            StopCoroutine(explosionCoroutine);
        Destroy(deathPreviewScript.clone);
        deathPreviewScript.playerObject.SetActive(false);

        if (rotateRightWithDelayScript.rotateCoroutine != null)
        {
            StopCoroutine(rotateRightWithDelayScript.rotateCoroutine);
            rotateRightWithDelayScript.ResetPosiiton();
        }

        playerPreviewRaycast.enabled = false;
        if (obstacleExplosionCoroutine != null)
        {
            StopCoroutine(obstacleExplosionCoroutine);
        }
        obstacleExplosionScript.parentObject.SetActive(false);

        // Resets Dissolve Preview
        playerDissolveScript.fade = 0;
        playerDissolveScript.isDissolving = false;

        buttonsInteractable = true;
        darkTintAnim.SetBool("showDarkTint", false);
        previewWindow.SetActive(false);
        homeButton.SetActive(true);

        circleCore.SetActive(false);
        starCore.SetActive(false);
        squareCore.SetActive(false);
        triangleCore.SetActive(false);
        playerPreview.SetActive(false);

        obstacleObject.SetActive(false);
        playerRipple.SetActive(false);
        playerDissolveObject.SetActive(false);

        blackHolePlayer.SetActive(false);

        playerOrbitingBall.SetActive(false);

        playerRotateTrail.SetActive(false);

        itemOnePreviewed = false;
        itemTwoPreviewed = false;
        itemThreePreviewed = false;
        itemFourPreviewed = false;

        itemFivePreviewed = false;
        itemSixPreviewed = false;
        itemSevenPreviewed = false;
        itemEightPreviewed = false;
        itemNinePreviewed = false;
        itemTenPreviewed = false;
        itemElevenPreviewed = false;
        itemTwelvePreviewed = false;
    }

    void LoadUnlockStatus()
    {
        unlockableScript.coreCircleUnlocked = ES3.Load<bool>("coreCircleUnlocked", false);
        unlockableScript.coreStarUnlocked = ES3.Load<bool>("coreStarUnlocked", false);
        unlockableScript.coreSquareUnlocked = ES3.Load<bool>("coreSquareUnlocked", false);
        unlockableScript.coreTriangleUnlocked = ES3.Load<bool>("coreTriangleUnlocked", false);

        unlockableScript.obstacleTrailUnlocked = ES3.Load<bool>("obstacleTrailUnlocked", false);
        unlockableScript.playerRippleUnlocked = ES3.Load<bool>("playerRippleUnlocked", false);
        unlockableScript.playerDeathUnlocked = ES3.Load<bool>("playerDeathUnlocked", false);
        unlockableScript.playerDissolveUnlocked = ES3.Load<bool>("playerDissolveUnlocked", false);

        unlockableScript.obstacleExplosionUnlocked = ES3.Load<bool>("obstacleExplosionUnlocked", false);
        unlockableScript.playerBlackHoleUnlocked = ES3.Load<bool>("playerBlackHoleUnlocked", false);
        unlockableScript.playerOrbitingBallUnlocked = ES3.Load<bool>("playerOrbitingBallUnlocked", false);
        unlockableScript.playerTrailEffectUnlocked = ES3.Load<bool>("playerTrailEffectUnlocked", false);
    }

    void SaveSelectionStatus()
    {
        ES3.Save("coreCircleSelected", unlockableScript.coreCircleSelected);
        ES3.Save("coreStarSelected", unlockableScript.coreStarSelected);
        ES3.Save("coreSquareSelected", unlockableScript.coreSquareSelected);
        ES3.Save("coreTriangleSelected", unlockableScript.coreTriangleSelected);

        ES3.Save("obstacleTrailSelected", unlockableScript.obstacleTrailSelected);
        ES3.Save("playerRippleSelected", unlockableScript.playerRippleSelected);
        ES3.Save("playerDeathSelected", unlockableScript.playerDeathSelected);
        ES3.Save("playerDissolveSelected", unlockableScript.playerDissolveSelected);

        ES3.Save("obstacleExplosionSelected", unlockableScript.obstacleExplosionSelected);
        ES3.Save("playerBlackHoleSelected", unlockableScript.playerBlackHoleSelected);
        ES3.Save("playerOrbitingBallSelected", unlockableScript.playerOrbitingBallSelected);
        ES3.Save("playerTrailEffectSelected", unlockableScript.playerTrailEffectSelected);
    }

    void LoadSelectionStatus()
    {
        unlockableScript.coreCircleSelected = ES3.Load<bool>("coreCircleSelected", false);
        unlockableScript.coreStarSelected = ES3.Load<bool>("coreStarSelected", false);
        unlockableScript.coreSquareSelected = ES3.Load<bool>("coreSquareSelected", false);
        unlockableScript.coreTriangleSelected = ES3.Load<bool>("coreTriangleSelected", false);

        unlockableScript.obstacleTrailSelected = ES3.Load<bool>("obstacleTrailSelected", false);
        unlockableScript.playerRippleSelected = ES3.Load<bool>("playerRippleSelected", false);
        unlockableScript.playerDeathSelected = ES3.Load<bool>("playerDeathSelected", false);
        unlockableScript.playerDissolveSelected = ES3.Load<bool>("playerDissolveSelected", false);

        unlockableScript.obstacleExplosionSelected = ES3.Load<bool>("obstacleExplosionSelected", false);
        unlockableScript.playerBlackHoleSelected = ES3.Load<bool>("playerBlackHoleSelected", false);
        unlockableScript.playerOrbitingBallSelected = ES3.Load<bool>("playerOrbitingBallSelected", false);
        unlockableScript.playerTrailEffectSelected = ES3.Load<bool>("playerTrailEffectSelected", false);
    }
}
