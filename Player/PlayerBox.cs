using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBox : MonoBehaviour
{
    public int targetScore = 10;

    public float rotationDuration;
    public bool isRotating;
    public bool canRotate;

    public bool justDestroyedMimic;

    public bool destroyedSecret;
    public bool countingSecret;

    public bool destroyedBlind;

    public bool canSpawnMimic;

    public bool playerDies = false;

    [HideInInspector]
    public bool canRotateRight;
    [HideInInspector]
    public bool canRotateLeft;

    [SerializeField] float timeSinceLastTap = 0f;
    bool checkLastTap;

    public Animator animArrowsRight;
    public Animator animArrowsLeft;

    public float rayDistance;
    public LayerMask obstacleMask;

    public Animator scoreAnim;
    public ObstacleSpawner spawnerScript;
    public Currency currencyScript;

    public Animator blindAnim;

    public CamShake camShakeScript; // New Camera Shake

    Animator anim;

    // Player Death Effect
    public ParticleSystem newDeathEffect;
    public ParticleSystem defaultDeathEffect;

    // Obstacle Destroy Effect
    public ParticleSystem obstacleGreen;
    public ParticleSystem obstacleOrange;
    public ParticleSystem obstacleCyan;
    public ParticleSystem obstaclePurple;
    public ParticleSystem obstacleMimic;

    public ParticleSystem newGreenExplosion, newOrangeExplosion, newCyanExplosion, newPurpleExplosion, newMimicExplosion;

    public GameObject whiteSquare;   

    RaycastHit2D hitTop;
    RaycastHit2D hitLeft;
    RaycastHit2D hitBottom;
    RaycastHit2D hitRight;

    public Unlockables unlockableScript;

    public SpriteRenderer whiteCoreSR;
    public Sprite whiteCoreCircle, whiteCoreStar, whiteCoreSquare, whiteCoreTriangle;

    public GameObject playerRippleEffect;
    public Dissolve playerDissolveScript;
    public GameObject orbitingBall;
    public GameObject particlesSuckedIn;
    public GameObject rotateTrailEffect;

    public static bool extraLifeConsumed = false;

    bool hitWrong; // If Player's rotation is between two colors (raycasts), kill player.
    [HideInInspector]
    public int direction;

    float targetTime = 1.25f;
    float currentTime = 0f;

    public bool obstacleHasSlowSpeed;

    public Score scoreScript;

    void Start()
    {
        unlockableScript.obstacleExplosionUnlocked = true;

        canSpawnMimic = true;

        unlockableScript.coreCircleSelected = ES3.Load<bool>("coreCircleSelected", false);
        unlockableScript.coreStarSelected = ES3.Load<bool>("coreStarSelected", false);
        unlockableScript.coreSquareSelected = ES3.Load<bool>("coreSquareSelected", false);
        unlockableScript.coreTriangleSelected = ES3.Load<bool>("coreTriangleSelected", false);

        
        unlockableScript.playerRippleSelected = ES3.Load<bool>("playerRippleSelected", false);
        unlockableScript.playerDeathSelected = ES3.Load<bool>("playerDeathSelected", false);
        unlockableScript.playerDissolveSelected = ES3.Load<bool>("playerDissolveSelected", false);

        unlockableScript.obstacleExplosionSelected = ES3.Load<bool>("obstacleExplosionSelected", false);
        unlockableScript.playerBlackHoleSelected = ES3.Load<bool>("playerBlackHoleSelected", false);
        unlockableScript.playerOrbitingBallSelected = ES3.Load<bool>("playerOrbitingBallSelected", false);
        unlockableScript.playerTrailEffectSelected = ES3.Load<bool>("playerTrailEffectSelected", false);

        if (unlockableScript.coreCircleSelected)
            whiteCoreSR.sprite = whiteCoreCircle;
        if (unlockableScript.coreStarSelected)
            whiteCoreSR.sprite = whiteCoreStar;
        if (unlockableScript.coreSquareSelected)
            whiteCoreSR.sprite = whiteCoreSquare;
        if (unlockableScript.coreTriangleSelected)
            whiteCoreSR.sprite = whiteCoreTriangle;

        if (unlockableScript.playerRippleSelected)
            playerRippleEffect.SetActive(true);

        if (unlockableScript.playerDissolveSelected)
            playerDissolveScript.enabled = true;

        if (unlockableScript.playerBlackHoleSelected)
            particlesSuckedIn.SetActive(true);

        if (unlockableScript.playerOrbitingBallSelected)
            orbitingBall.SetActive(true);

        if (unlockableScript.playerTrailEffectSelected)
            rotateTrailEffect.SetActive(true);

        anim = GetComponent<Animator>();

        direction = Random.Range(1, 3);

        if (extraLifeConsumed)
            targetScore = ES3.Load<int>("targetScore", 0);

        int randomStartRotation = Random.Range(1, 5);

        hitWrong = false;

        if (randomStartRotation == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (randomStartRotation == 2)
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (randomStartRotation == 3)
            transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (randomStartRotation == 4)
            transform.rotation = Quaternion.Euler(0, 0, 270);

        if (direction == 1)
            animArrowsRight.SetTrigger("ArrowsRightStart");
        else if (direction == 2)
            animArrowsLeft.SetTrigger("ArrowsLeftStart");
    }

    void Update()
    {

        if (isRotating)
            canRotate = false;
        else canRotate = true;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (direction == 1) // Rotate Right/Clockwise
        {           // Right
            canRotateRight = true;
            canRotateLeft = false;
        }
        else if (direction == 3) // Rotate Left/CounterClockwise
        {           // Flipped Left (= Right)
            canRotateRight = false;
            canRotateLeft = true;
        }
        else if (direction == 2) // Rotate Left/CounterClockwise
        {           // Left
            canRotateLeft = true;
            canRotateRight = false;
        }
        else if (direction == 4) // Rotate Left/CounterClockwise
        {           // Flipped Right (= Left)
            canRotateLeft = false;
            canRotateRight = true;
        }

        #region Mobile Input (Touch Half Screen, not currently used)

            // MOBILE INPUT, Prevent Input when clicking buttons
            if (Input.GetMouseButtonDown(0) && canRotate)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                        return;
                }

                // ---Mobile Touch Half Screen---
                if (currentScene.name == "GM2")
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.position.x < Screen.width / 2) // Left
                        {
                            //canRotateRight = false;
                            //imgRotateRight.color = new Color(255, 255, 255, 0.05f);
                            StartCoroutine(RotateZ(Vector3.forward, 90, rotationDuration));
                        }
                        else if (touch.position.x > Screen.width / 2) // Right
                        {
                            //canRotateLeft = false;
                            //imgRotateLeft.color = new Color(255, 255, 255, 0.05f);
                            StartCoroutine(RotateZ(Vector3.forward, -90, rotationDuration));
                        }
                    }
                }
                    

                //StartCoroutine(Rotate(Vector3.forward, -90, rotationDuration));
            }

        #endregion

        // INPUT, PC + Mobile Touch
        if (Input.GetMouseButtonDown(0) && canRotate)
        {
            if (canRotateRight)
                StartCoroutine(RotateZ(Vector3.forward, -90, rotationDuration));

            if (canRotateLeft)
                StartCoroutine(RotateZ(Vector3.forward, 90, rotationDuration));
        }     

        // Wait X Frame(s) to see if player has finished rotating - execute coroutine.
        if (Input.GetMouseButtonDown(0) && !canRotate)
        {
            checkLastTap = true;
        }

        if (checkLastTap)
        {
            RotateZDelay();
        }

        hitTop = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), rayDistance, obstacleMask);
        hitLeft = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), rayDistance, obstacleMask);
        hitBottom = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), rayDistance, obstacleMask);
        hitRight = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), rayDistance, obstacleMask);

        // ------

        if (destroyedBlind)
            blindAnim.SetBool("Blinded", true);
        else
            blindAnim.SetBool("Blinded", false);


        #region Top Layer (Green)
        // DEFAULT TOP LAYER
        if (hitTop.collider != null)
        {
            if (hitTop.collider.tag == "Green" && !hitWrong)
            {
                HitTopGreen();
                Scoring();
            }

            if (hitTop.collider.tag == "Green_Mimic" && !hitWrong)
            {
                HitMimic();
                Scoring();
            }

            if (hitTop.collider.tag == "Green_Secret" && !hitWrong)
            {
                HitTopGreenSecret();
            }

            if (hitTop.collider.tag == "Green_Blind" && !hitWrong)
            {
                HitTopGreenBlind();
                Scoring();
            }

            if (hitTop.collider.tag == "Orange_Secret" || hitTop.collider.tag == "Cyan_Secret" || hitTop.collider.tag == "Purple_Secret")
            {
                HitTopGreenSecretFailed();
            }

            if (hitTop.collider.tag == "Green_Left" && !hitWrong)
            {
                HitTopGreen();
                Scoring();

                // Checking the current rotating direction and plays the "Rotating Arrows"-animation for the new/next/upcoming direction.
                if (direction == 2 || direction == 4) // If Left
                    animArrowsRight.SetTrigger("ArrowsRight"); // Play Right Animation
                else if (direction == 1 || direction == 3) // If Right
                    animArrowsLeft.SetTrigger("ArrowsLeft"); // Play Left Animation

                // Checking the previous/current direction and changes to the new/next/upcoming direction.
                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right
            }

            if (hitTop.collider.tag == "Green_Right" && !hitWrong)
            {
                HitTopGreen();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right
            }

            if (hitTop.collider.tag == "Green_Swap_X" && !hitWrong)
            {
                HitTopGreen();
                Scoring();
                SwapColorsHorizontal();
            }

            if (hitTop.collider.tag == "Green_Swap_Y" && !hitWrong)
            {
                HitTopGreen();
                Scoring();
                SwapColorsVertical();
            }

            if (hitTop.collider.tag == "Orange_Deadly" || hitTop.collider.tag == "Cyan_Deadly" || hitTop.collider.tag == "Purple_Deadly")
            {
                HitTopGreen();
                Scoring();
            }
                
            if (hitTop.collider.tag == "Green_Deadly")
            {
                //HitTopGreen();
                KillPlayer();
            }

            if (hitTop.collider.gameObject.layer == LayerMask.NameToLayer("Deadly"))
                return;

            if (hitTop.collider.tag == "Orange_Secret" || hitTop.collider.tag == "Cyan_Secret" || hitTop.collider.tag == "Purple_Secret")
                return;

            if (hitTop.collider.gameObject.layer != LayerMask.NameToLayer("Green"))
            {
                hitWrong = true;
                KillPlayer();
            }
        }
        #endregion

        #region Left Layer (Orange)
        // DEFAULT LEFT LAYER
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.tag == "Orange" && !hitWrong)
            {
                HitLeftOrange();
                Scoring();
            }

            if (hitLeft.collider.tag == "Orange_Mimic" && !hitWrong)
            {
                HitMimic();
                Scoring();
            }

            if (hitLeft.collider.tag == "Orange_Secret" && !hitWrong)
            {
                HitLeftOrangeSecret();
                // do the thinga
            }

            if (hitLeft.collider.tag == "Orange_Blind" && !hitWrong)
            {
                HitLeftOrangeBlind();
                Scoring();
            }

            if (hitLeft.collider.tag == "Green_Secret" || hitLeft.collider.tag == "Cyan_Secret" || hitLeft.collider.tag == "Purple_Secret")
            {
                HitLeftOrangeSecretFailed();
            }

            if (hitLeft.collider.tag == "Orange_Left" && !hitWrong)
            {
                HitLeftOrange();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right
            }

            if (hitLeft.collider.tag == "Orange_Right" && !hitWrong)
            {
                HitLeftOrange();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right
            }

            if (hitLeft.collider.tag == "Orange_Swap_X" && !hitWrong)
            {
                HitLeftOrange();
                Scoring();
                SwapColorsHorizontal();
            }

            if (hitLeft.collider.tag == "Orange_Swap_Y" && !hitWrong)
            {
                HitLeftOrange();
                Scoring();
                SwapColorsVertical();
            }

            if (hitLeft.collider.tag == "Cyan_Deadly" || hitLeft.collider.tag == "Purple_Deadly" || hitLeft.collider.tag == "Green_Deadly")
            {
                HitLeftOrange();
                Scoring();
            }
                
            if (hitLeft.collider.tag == "Orange_Deadly")
            {
                //HitLeftOrange();
                KillPlayer();
            }

            if (hitLeft.collider.gameObject.layer == LayerMask.NameToLayer("Deadly"))
                return;

            if (hitLeft.collider.tag == "Green_Secret" || hitLeft.collider.tag == "Cyan_Secret" || hitLeft.collider.tag == "Purple_Secret")
                return;

            if (hitLeft.collider.gameObject.layer != LayerMask.NameToLayer("Orange"))
            {
                hitWrong = true;
                KillPlayer();
            }
        }
        #endregion

        #region Bottom Layer (Cyan)
        // DEFAULT BOTTOM LAYER
        if (hitBottom.collider != null)
        {
            if (hitBottom.collider.tag == "Cyan" && !hitWrong)
            {
                HitBottomCyan();
                Scoring();
            }

            if (hitBottom.collider.tag == "Cyan_Mimic" && !hitWrong)
            {
                HitMimic();
                Scoring();
            }

            if (hitBottom.collider.tag == "Cyan_Secret" && !hitWrong)
            {
                HitBottomCyanSecret();
            }

            if (hitBottom.collider.tag == "Cyan_Blind" && !hitWrong)
            {
                HitBottomCyanBlind();
                Scoring();
            }

            if (hitBottom.collider.tag == "Green_Secret" || hitBottom.collider.tag == "Orange_Secret" || hitBottom.collider.tag == "Purple_Secret")
            {
                HitBottomCyanSecretFailed();
            }

            if (hitBottom.collider.tag == "Cyan_Left" && !hitWrong)
            {
                HitBottomCyan();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right

                //Debug.Log("Current Direction: " + direction);
            }

            if (hitBottom.collider.tag == "Cyan_Right" && !hitWrong)
            {
                HitBottomCyan();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right

                //Debug.Log("Current Direction: " + direction);
            }

            if (hitBottom.collider.tag == "Cyan_Swap_X" && !hitWrong)
            {
                HitBottomCyan();
                Scoring();
                SwapColorsHorizontal();
            }

            if (hitBottom.collider.tag == "Cyan_Swap_Y" && !hitWrong)
            {
                HitBottomCyan();
                Scoring();
                SwapColorsVertical();
            }

            if (hitBottom.collider.tag == "Purple_Deadly" || hitBottom.collider.tag == "Green_Deadly" || hitBottom.collider.tag == "Orange_Deadly")
            {
                HitBottomCyan();
                Scoring();
            }
                
            if (hitBottom.collider.tag == "Cyan_Deadly")
            {
                //HitBottomCyan();
                KillPlayer();
                
            }

            if (hitBottom.collider.gameObject.layer == LayerMask.NameToLayer("Deadly"))
                return;

            if (hitBottom.collider.tag == "Green_Secret" || hitBottom.collider.tag == "Orange_Secret" || hitBottom.collider.tag == "Purple_Secret")
                return;

            if (hitBottom.collider.gameObject.layer != LayerMask.NameToLayer("Cyan"))
            {
                hitWrong = true;
                KillPlayer();
            }
        }
        #endregion

        #region Right Layer (Purple)
        // DEFAULT RIGHT LAYER
        if (hitRight.collider != null)
        {
            if (hitRight.collider.tag == "Purple" && !hitWrong)
            {
                HitRightPurple();
                Scoring();
            }

            if (hitRight.collider.tag == "Purple_Mimic" && !hitWrong)
            {
                HitMimic();
                Scoring();
            }

            if (hitRight.collider.tag == "Purple_Secret" && !hitWrong)
            {
                HitRightPurpleSecret();
            }

            if (hitRight.collider.tag == "Purple_Blind" && !hitWrong)
            {
                HitRightPurpleBlind();
                Scoring();
            }

            if (hitRight.collider.tag == "Green_Secret" || hitRight.collider.tag == "Orange_Secret" || hitRight.collider.tag == "Cyan_Secret")
            {
                HitRightPurpleSecretFailed();
            }

            if (hitRight.collider.tag == "Purple_Left" && !hitWrong)
            {
                HitRightPurple();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right

            }

            if (hitRight.collider.tag == "Purple_Right" && !hitWrong)
            {
                HitRightPurple();
                Scoring();

                if (direction == 2 || direction == 4)
                    animArrowsRight.SetTrigger("ArrowsRight");
                else if (direction == 1 || direction == 3)
                    animArrowsLeft.SetTrigger("ArrowsLeft");

                if (direction == 1) // Right
                    direction = 2; // Left
                else if (direction == 2) // Left
                    direction = 1; // Right
                else if (direction == 3) // Right
                    direction = 4; // Left
                else if (direction == 4) // Left
                    direction = 3; // Right

            }

            if (hitRight.collider.tag == "Purple_Swap_X" && !hitWrong)
            {
                HitRightPurple();
                Scoring();
                SwapColorsHorizontal();
            }

            if (hitRight.collider.tag == "Purple_Swap_Y" && !hitWrong)
            {
                HitRightPurple();
                Scoring();
                SwapColorsVertical();
            }

            if (hitRight.collider.tag == "Green_Deadly" || hitRight.collider.tag == "Orange_Deadly" || hitRight.collider.tag == "Cyan_Deadly")
            {
                HitRightPurple();
                Scoring();
            }
                
            if (hitRight.collider.tag == "Purple_Deadly")
            {
                //HitRightPurple();
                KillPlayer();
            }

            if (hitRight.collider.gameObject.layer == LayerMask.NameToLayer("Deadly"))
                return;

            if (hitRight.collider.tag == "Green_Secret" || hitRight.collider.tag == "Orange_Secret" || hitRight.collider.tag == "Cyan_Secret")
                return;

            if (hitRight.collider.gameObject.layer != LayerMask.NameToLayer("Purple"))
            {
                hitWrong = true;
                KillPlayer();
            }
        }
        #endregion

        // ------       

        #region Debugging
        // RAYCAST DEBUGGING
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * rayDistance, Color.green);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * rayDistance, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * rayDistance, Color.cyan);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * rayDistance, Color.blue);

        //Debug.Log("Current Direction: " + direction);

        #endregion

        if (countingSecret) // Secret Obstacle
        {
            currentTime += Time.deltaTime;

            if (currentTime >= targetTime)
            {
                countingSecret = false;
                spawnerScript.SpawnObstacle();
                currentTime = 0;
            }
        }
    }

    #region RotationZ
    IEnumerator RotateZ(Vector3 axis, float angle, float duration)
    {
        isRotating = true;

        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
        isRotating = false;

        Stats.timesRotated += 1;
    }
    #endregion

    #region RotateZDelay
    void RotateZDelay() // Om spelaren "tapar" precis innan objektet roterat färdigt
    {
        timeSinceLastTap += Time.deltaTime;

        if (timeSinceLastTap < 0.075f && !isRotating) // If timer has NOT reached target and object has finished rotating.
        {
            if (canRotateRight)
                StartCoroutine(RotateZ(Vector3.forward, -90, rotationDuration));

            if (canRotateLeft)
                StartCoroutine(RotateZ(Vector3.forward, 90, rotationDuration));
        }
        else if (timeSinceLastTap >= 0.1f && !isRotating)
        {
            timeSinceLastTap = 0f;
            checkLastTap = false;
        }
    }
    #endregion

    #region Scoring
    void Scoring()
    {        
        GameMaster.watchedAd = false;
        FindObjectOfType<AudioManager>().Play("PlayerScores");
        scoreAnim.SetTrigger("ScoreUp");
        anim.SetTrigger("Flash");
        Score.currentScore++;
        Stats.totalScoreStats++;

        //currencyScript.currencyToGive += Score.currentScore;
        currencyScript.totalCurrencyToGive += currencyScript.currencyToGive;

        if (targetScore <= 30)
        {
            if (Score.currentScore >= targetScore)
            {
                targetScore += 10;
                currencyScript.currencyToGive += 1;
            }
        }      
    }
    #endregion

    #region RotationXY (Swap)
    IEnumerator RotateXY(Vector3 axis, float angle, float duration)
    {
        while (isRotating)
            yield return null;

        isRotating = true;    
        
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {          
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
        isRotating = false;

        //Debug.Log("Direction: " + direction);
    }
    #endregion

    #region KillPlayer
    void KillPlayer()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDies");

        if (Vibrator.vibratorSelected)
            Vibrator.Vibrate();

        ES3.Save("targetScore", targetScore);

        if (unlockableScript.playerDeathSelected)
        {
            Instantiate(newDeathEffect, transform.position, transform.rotation); // Unlockable
        }
        else
        {
            Instantiate(defaultDeathEffect, transform.position, transform.rotation); // Default
        }
               
        ES3.Save("timesRotated", Stats.timesRotated);
        camShakeScript.Trauma = 1f;

        FindObjectOfType<GameMaster>().GameOver();

        Destroy(whiteSquare);
        Destroy(orbitingBall);
        Destroy(particlesSuckedIn);
        Destroy(gameObject);
    }
    #endregion

    #region Instantiate Particles
    void InstantiateParticlesGreen()
    {
        if (!unlockableScript.obstacleExplosionSelected)
        {
            // hitTop - GREEN
            if (hitTop.collider.gameObject.layer == LayerMask.NameToLayer("Green"))
                Instantiate(obstacleGreen, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);

            else if (hitTop.collider.tag == "Orange_Deadly")
                Instantiate(obstacleOrange, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);

            else if (hitTop.collider.tag == "Cyan_Deadly")
                Instantiate(obstacleCyan, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);

            else if (hitTop.collider.tag == "Purple_Deadly")
                Instantiate(obstaclePurple, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);
        }
        else
        {
            // hitTop - GREEN
            if (hitTop.collider.gameObject.layer == LayerMask.NameToLayer("Green"))
                Instantiate(newGreenExplosion, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);

            else if (hitTop.collider.tag == "Orange_Deadly")
                Instantiate(newOrangeExplosion, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);

            else if (hitTop.collider.tag == "Cyan_Deadly")
                Instantiate(newCyanExplosion, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);

            else if (hitTop.collider.tag == "Purple_Deadly")
                Instantiate(newPurpleExplosion, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation);
        }
          
    }

    void InstantiateParticlesOrange()
    {
        if (!unlockableScript.obstacleExplosionSelected)
        {
            // hitLeft - ORANGE
            if (hitLeft.collider.gameObject.layer == LayerMask.NameToLayer("Orange"))
                Instantiate(obstacleOrange, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);

            else if (hitLeft.collider.tag == "Cyan_Deadly")
                Instantiate(obstacleCyan, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);

            else if (hitLeft.collider.tag == "Purple_Deadly")
                Instantiate(obstaclePurple, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);

            else if (hitLeft.collider.tag == "Green_Deadly")
                Instantiate(obstacleGreen, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);
        }
        else
        {
            // hitLeft - ORANGE
            if (hitLeft.collider.gameObject.layer == LayerMask.NameToLayer("Orange"))
                Instantiate(newOrangeExplosion, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);

            else if (hitLeft.collider.tag == "Cyan_Deadly")
                Instantiate(newCyanExplosion, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);

            else if (hitLeft.collider.tag == "Purple_Deadly")
                Instantiate(newPurpleExplosion, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);

            else if (hitLeft.collider.tag == "Green_Deadly")
                Instantiate(newGreenExplosion, hitLeft.collider.gameObject.transform.position, hitLeft.collider.gameObject.transform.rotation);
        }

    }

    void InstantiateParticlesCyan()
    {
        if (!unlockableScript.obstacleExplosionSelected)
        {
            // hitBottom - CYAN
            if (hitBottom.collider.gameObject.layer == LayerMask.NameToLayer("Cyan"))
                Instantiate(obstacleCyan, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);

            else if (hitBottom.collider.tag == "Purple_Deadly")
                Instantiate(obstaclePurple, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);

            else if (hitBottom.collider.tag == "Green_Deadly")
                Instantiate(obstacleGreen, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);

            else if (hitBottom.collider.tag == "Orange_Deadly")
                Instantiate(obstacleOrange, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);
        }
        else
        {
            // hitBottom - CYAN
            if (hitBottom.collider.gameObject.layer == LayerMask.NameToLayer("Cyan"))
                Instantiate(newCyanExplosion, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);

            else if (hitBottom.collider.tag == "Purple_Deadly")
                Instantiate(newPurpleExplosion, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);

            else if (hitBottom.collider.tag == "Green_Deadly")
                Instantiate(newGreenExplosion, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);

            else if (hitBottom.collider.tag == "Orange_Deadly")
                Instantiate(newOrangeExplosion, hitBottom.collider.gameObject.transform.position, hitBottom.collider.gameObject.transform.rotation);
        }

    }

    void InstantiateParticlesPurple()
    {
        if (!unlockableScript.obstacleExplosionSelected)
        {
            // hitRight - PURPLE
            if (hitRight.collider.gameObject.layer == LayerMask.NameToLayer("Purple"))
                Instantiate(obstaclePurple, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);

            else if (hitRight.collider.tag == "Green_Deadly")
                Instantiate(obstacleGreen, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);

            else if (hitRight.collider.tag == "Orange_Deadly")
                Instantiate(obstacleOrange, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);

            else if (hitRight.collider.tag == "Cyan_Deadly")
                Instantiate(obstacleCyan, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);
        }
        else
        {
            // hitRight - PURPLE
            if (hitRight.collider.gameObject.layer == LayerMask.NameToLayer("Purple"))
                Instantiate(newPurpleExplosion, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);

            else if (hitRight.collider.tag == "Green_Deadly")
                Instantiate(newGreenExplosion, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);

            else if (hitRight.collider.tag == "Orange_Deadly")
                Instantiate(newOrangeExplosion, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);

            else if (hitRight.collider.tag == "Cyan_Deadly")
                Instantiate(newCyanExplosion, hitRight.collider.gameObject.transform.position, hitRight.collider.gameObject.transform.rotation);
        }

    }
    #endregion

    #region HitSideMethods
    void HitTopGreen()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitTop.collider.gameObject);
        InstantiateParticlesGreen();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;        

        canSpawnMimic = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }

    void HitLeftOrange()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitLeft.collider.gameObject);
        InstantiateParticlesOrange();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }

    void HitBottomCyan()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitBottom.collider.gameObject);
        InstantiateParticlesCyan();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }

    void HitRightPurple()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitRight.collider.gameObject);
        InstantiateParticlesPurple();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }

    void HitMimic()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(GameObject.FindWithTag("Obstacle_Mimic"));
        if (!unlockableScript.obstacleExplosionSelected)
            Instantiate(obstacleMimic, GameObject.FindWithTag("Obstacle_Mimic").transform.position, Quaternion.identity);
        else Instantiate(newMimicExplosion, GameObject.FindWithTag("Obstacle_Mimic").transform.position, Quaternion.identity);

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = true;
        destroyedBlind = false;

        canSpawnMimic = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }
    #endregion

    #region HitSidesSecret
    void HitTopGreenSecret()
    {
        hitWrong = false;
        Destroy(hitTop.collider.gameObject);
        InstantiateParticlesGreen();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretGainScore();
        currencyScript.totalCurrencyToGive += 5;
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitTopGreenSecretFailed()
    {
        hitWrong = false;
        Destroy(hitTop.collider.gameObject);
        InstantiateParticlesGreen();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretLoseScore();
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitLeftOrangeSecret()
    {
        hitWrong = false;
        Destroy(hitLeft.collider.gameObject);
        InstantiateParticlesOrange();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretGainScore();
        currencyScript.totalCurrencyToGive += 5;
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitLeftOrangeSecretFailed()
    {
        hitWrong = false;
        Destroy(hitLeft.collider.gameObject);
        InstantiateParticlesOrange();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretLoseScore();
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitBottomCyanSecret()
    {
        hitWrong = false;
        Destroy(hitBottom.collider.gameObject);
        InstantiateParticlesCyan();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretGainScore();
        currencyScript.totalCurrencyToGive += 5;
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitBottomCyanSecretFailed()
    {
        hitWrong = false;
        Destroy(hitBottom.collider.gameObject);
        InstantiateParticlesCyan();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretLoseScore();
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitRightPurpleSecret()
    {
        hitWrong = false;
        Destroy(hitRight.collider.gameObject);
        InstantiateParticlesPurple();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretGainScore();
        currencyScript.totalCurrencyToGive += 5;
        countingSecret = true;
        destroyedSecret = true;
    }

    void HitRightPurpleSecretFailed()
    {
        hitWrong = false;
        Destroy(hitRight.collider.gameObject);
        InstantiateParticlesPurple();

        obstacleHasSlowSpeed = false;

        if (destroyedBlind)
            obstacleHasSlowSpeed = true;

        justDestroyedMimic = false;
        destroyedBlind = false;

        canSpawnMimic = false;

        spawnerScript.IncrementObstacleSpeed();

        scoreScript.SecretLoseScore();
        countingSecret = true;
        destroyedSecret = true;
    }
    #endregion

    #region HitSidesBlind
    void HitTopGreenBlind()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitTop.collider.gameObject);
        InstantiateParticlesGreen();

        justDestroyedMimic = false;
        destroyedBlind = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }
    void HitLeftOrangeBlind()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitLeft.collider.gameObject);
        InstantiateParticlesOrange();

        justDestroyedMimic = false;
        destroyedBlind = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }
    void HitBottomCyanBlind()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitBottom.collider.gameObject);
        InstantiateParticlesCyan();

        justDestroyedMimic = false;
        destroyedBlind = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }
    void HitRightPurpleBlind()
    {
        destroyedSecret = false;
        hitWrong = false;
        Destroy(hitRight.collider.gameObject);
        InstantiateParticlesPurple();

        justDestroyedMimic = false;
        destroyedBlind = true;

        spawnerScript.IncrementObstacleSpeed();
        spawnerScript.SpawnObstacle();
    }
    #endregion

    #region SwapColorsX (Horizontal)
    void SwapColorsHorizontal()
    {
        if (transform.rotation.eulerAngles.z > 45 && transform.rotation.eulerAngles.z < 135 // 90, between 0 and 180
            || transform.rotation.eulerAngles.z > 225 && transform.rotation.eulerAngles.z < 315) // 270. between 180 and 0 (360)
        {
            StartCoroutine(RotateXY(Vector3.right, 180f, rotationDuration));
        }
        else StartCoroutine(RotateXY(Vector3.up, 180, rotationDuration));

        if (direction == 1)
            direction = 3;
        else if (direction == 2)
            direction = 4;
        else if (direction == 3)
            direction = 1;
        else if (direction == 4)
            direction = 2;

        Debug.Log("Current Direction: " + direction);
        //Debug.Log("void SwapColorsHorizontal");
    }
    #endregion

    #region SwapColorsY (Vertical)
    void SwapColorsVertical()
    {
        if (transform.rotation.eulerAngles.z > 45 && transform.rotation.eulerAngles.z < 135 // 90, between 0 and 180
            || transform.rotation.eulerAngles.z > 225 && transform.rotation.eulerAngles.z < 315) // 270, between 180 and 0 (360)
        {
            StartCoroutine(RotateXY(Vector3.up, 180f, rotationDuration));
        }
        else StartCoroutine(RotateXY(Vector3.right, 180, rotationDuration));

        if (direction == 1)
            direction = 3;
        else if (direction == 2)
            direction = 4;
        else if (direction == 3)
            direction = 1;
        else if (direction == 4)
            direction = 2;

        //Debug.Log("Current Direction: " + direction);
        //Debug.Log("void SwapColorsVertical");
    }
    #endregion
}
