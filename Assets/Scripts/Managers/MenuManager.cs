using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;   //Music to play for menu scene
    // Start is called before the first frame update
    void Start()
    {
        //Play the menu music
        AudioManager.Instance.PlayMusic(menuMusic);
    }



    public void QuitGame(){
        //If we are using the Unity editor, then the scene will stop playing.
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;

		//If this was a build of the game it would quit to the desktop.
		#else 
		Application.Quit();
		#endif
    }
}
