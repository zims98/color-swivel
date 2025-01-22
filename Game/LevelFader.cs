using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{

    Animator anim;

    int levelToLoad;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
