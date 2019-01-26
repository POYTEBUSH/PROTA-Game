using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator mLevelAnim;
    private int mLevelToLoad;

    public void FadeToLevl(int level)
    {
        mLevelToLoad = level;
        mLevelAnim.SetTrigger("Fade Out");
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
        SceneManager.LoadScene(mLevelToLoad);
    }
}
