using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;     //Reference to background music

    // Start is called before the first frame update
    void Start()
    {
        //Play the background music
        AudioManager.Instance.PlayMusic(bgm);
    }

}
