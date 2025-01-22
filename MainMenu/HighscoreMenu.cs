using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreMenu : MonoBehaviour
{

    public TextMeshProUGUI highscoreText;

    private void Update()
    {
        highscoreText.text = Score.highscore.ToString("0");
    }

}
