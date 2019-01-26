using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void FadeToLevel(int level)
    {
        levelToLoad = level;
        animator.SetTrigger("Fade Out");
    }

    public void QuitGame ()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
