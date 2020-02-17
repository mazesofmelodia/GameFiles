using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;             //Image for fading in and out
    public AnimationCurve fadeCurve;    //Animation curve to control fading

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Fades out to a different scene
    /// </summary>
    /// <param name="scene"></param>
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void RestartScene(){
        //Get the current scene and load that scene again
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().name));
    }

    //Fades in to a scene after loading it
    IEnumerator FadeIn()
    {
        float t = 1f;

        while(t > 0)
        {
            t -= Time.deltaTime;
            //Set a float based on the curve value relative to t
            float a = fadeCurve.Evaluate(t);
            //Adjust the image alpha value based on the a value
            fadeImage.color = new Color(0f, 0f, 0f, a);
            //Wait a frame and then continue the loop
            yield return 0;
        }
    }

    //Fades out from the scene to move to a different scene
    IEnumerator FadeOut(string scene)
    {
        
        Time.timeScale = 1;
        
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime;
            //Set a float based on the curve value relative to t
            float a = fadeCurve.Evaluate(t);
            //Adjust the image alpha value based on the a value
            fadeImage.color = new Color(0f, 0f, 0f, a);
            //Wait a frame and then continue the loop
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
