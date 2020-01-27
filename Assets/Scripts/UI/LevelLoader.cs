using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
