using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelLoader : MonoBehaviour
{
    public Animator fadeoutAnim;
    public float transitionTime;

    //Loads the next level
    public void LoadLevel(string levelName)
    {
        //Start the load level routine
        StartCoroutine(LoadLevelRoutine(levelName));
    }

    //Coroutine to fade out and load the level
    IEnumerator LoadLevelRoutine(string levelName)
    {
        //Starts the Start animation
        fadeoutAnim.SetTrigger("Start");

        //Pauses the coroutine
        yield return new WaitForSeconds(transitionTime);

        //Load the scene
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        //Start the quit game coroutine
        StartCoroutine(QuitGameRoutine());
    }

    //Coroutine to fade out and quit the game
    IEnumerator QuitGameRoutine()
    {
        //Starts the Start animation
        fadeoutAnim.SetTrigger("Start");

        //Pauses the coroutine
        yield return new WaitForSeconds(transitionTime);

        //If we are using the Unity editor, then the scene will stop playing.
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;

        //If this was a build of the game it would quit to the desktop.
        #else
        Application.Quit();
        #endif
    }
}
