using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVisuals : MonoBehaviour
{

    Animator levelAnim;

    LevelSystemAnimated levelSystemAnimated;

    void Awake()
    {
        levelAnim = GetComponent<Animator>();
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;

        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        levelAnim.SetTrigger("levelingUp");
    }
}
