using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /* Static instance */
    public static AudioManager Instance;      //AudioManager in scene
    //Public referance to the AudioManager for other scripts to access
    /*public static AudioManager Instance{
        get{
            //Check if there is no instance of the audio manager in the scene
            if(instance == null){
                //Look for an audio manager in scene
                instance = FindObjectOfType<AudioManager>();
                //If we still didn't find an audio manager
                if(instance == null){
                    //Create a new game object with an audio manager and set that as the instance
                    instance = new GameObject("NewAudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        //Ensures that set functionality can't be set be any other script
        private set{
            //Set the instance to a value
            instance = value;
        }
    }*/

    /* Other Variables */
    [SerializeField] private float managerMusicVolume;     //Music Volume of the audiomanager
    [SerializeField] private float managerSFXVolume;     //SFX Volume of the audiomanager

    [Header("Snapshots and output groups")]
    [SerializeField] private AudioMixerSnapshot dungeonAudioSnapshot;   //Dungeon audio snapshot
    [SerializeField] private AudioMixerSnapshot battleAudioSnapshot;    //Battle audio snapshot

    [Space]
    [SerializeField] private AudioMixerGroup dungeonOutputGroup;        //Dungeon audio output
    [SerializeField] private AudioMixerGroup battleOutputGroup;         //Battle audio output

    private AudioSource musicSource;       //Audio source for playing music
    private AudioSource dungeonMusicSource;       //Audio source for cross fading music
    private AudioSource battleMusicSource;
    private AudioSource sfxSource;          //Audio source for playing sound effects
    private bool musicSourceAPlaying;       //Check if music source a is playing

    private void Awake() {
        //Check if there is no instance in the scene
        if (Instance == null)
        {
            //Make this Manager the instance
            Instance = this;
        }
        //If there's already an instance
        else if (Instance != null)
        {
            //Destroy this game object
            Destroy(this.gameObject);
        }

        //Add the Audiosources as components to the AudioManager
        musicSource = this.gameObject.AddComponent<AudioSource>();
        dungeonMusicSource = this.gameObject.AddComponent<AudioSource>();
        battleMusicSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //Set the output group for the dungeon and battle audio sources
        dungeonMusicSource.outputAudioMixerGroup = dungeonOutputGroup;
        battleMusicSource.outputAudioMixerGroup = battleOutputGroup;

        //Loop the music tracks
        musicSource.loop = true;
        dungeonMusicSource.loop = true;
        battleMusicSource.loop = true;
    }

    /// <summary>
    /// Plays a music track
    /// </summary>
    /// <param name="musicClip">Music audio to play</param>
    public void PlayMusic(AudioClip musicClip){
        //Check which audio source is playing, if musicSourceAPlaying is true use musicSourceA, otherwise use musicSourceB
        AudioSource activeSource = (musicSourceAPlaying) ? musicSource : dungeonMusicSource;

        //Set the audio clip on the music source and play it
        activeSource.clip = musicClip;
        //Set the volume of the clip
        activeSource.volume = managerMusicVolume;
        activeSource.Play();
    }

    public void PlayDungeonMusic(AudioClip dungeonTheme, AudioClip battleTheme)
    {
        //Set the music for both sources
        dungeonMusicSource.clip = dungeonTheme;
        battleMusicSource.clip = battleTheme;

        //Set the volume of both music sources
        dungeonMusicSource.volume = managerMusicVolume;
        battleMusicSource.volume = managerMusicVolume;

        //Play both
        dungeonMusicSource.Play();
        battleMusicSource.Play();
    }

    public void StopDungeonMusic(float transitionTime = 1.0f)
    {
        //Fade out float
        float t = 0.0f;

        //Fade out over time
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            //Reduce the volume of the sources
            dungeonMusicSource.volume = (managerMusicVolume - (t / transitionTime));
            battleMusicSource.volume = (managerMusicVolume - (t / transitionTime));
        }

        //Stops the music
        dungeonMusicSource.Stop();
        battleMusicSource.Stop();
    }

    //Transitions to the battle music snapshot
    public void FadeToBattleMusic(float transitionTime = 1.0f)
    {
        //Fade to the battle theme
        battleAudioSnapshot.TransitionTo(transitionTime);
    }

    //Transition to dungeon music snapshot
    public void FadeToDungeonMusic(float transitionTime = 1.0f)
    {
        //Fade to the dungeon theme
        dungeonAudioSnapshot.TransitionTo(transitionTime);
    }

    /// <summary>
    /// Stops the music that's currently playing
    /// </summary>
    public void StopMusic(float transitionTime = 1.0f)
    {
        //Check which audio source is playing, if musicSourceAPlaying is true use musicSourceA, otherwise use musicSourceB
        AudioSource activeSource = (musicSourceAPlaying) ? musicSource : dungeonMusicSource;

        //Fade out float
        float t = 0.0f;

        //Fade out over time
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            //Reduce the volume of the active source
            activeSource.volume = (managerMusicVolume - (t / transitionTime));
        }

        //Stops the music
        activeSource.Stop();
    }

    /// <summary>
    /// Play music sound and fade out the previous music sound
    /// </summary>
    /// <param name="newClip">New music to play</param>
    /// <param name="transitionTime">Transition time between music sounds</param>
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f){
        //Check which audio source is playing, if musicSourceAPlaying is true use musicSourceA, otherwise use musicSourceB
        AudioSource activeSource = (musicSourceAPlaying) ? musicSource : dungeonMusicSource;

        //Start UpdateMusic coroutine
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }

    /// <summary>
    /// Plays new music sound while cross fading from one music sound to the other
    /// </summary>
    /// <param name="newClip">New music track to play</param>
    /// <param name="transitionTime">Time of transition between music</param>
    public void PlayMusicWithCrossFade(AudioClip newClip, float transitionTime = 1.0f){
        //Determine which source is active
        AudioSource activeSource = (musicSourceAPlaying) ? musicSource : dungeonMusicSource;
        //Set new source
        AudioSource newSource = (musicSourceAPlaying) ? dungeonMusicSource : musicSource;

        //Swap the source
        musicSourceAPlaying = !musicSourceAPlaying;

        //Set the fields of the new Audio source
        newSource.clip = newClip;
        newSource.Play();
        //Start coroutine to cross fade audio
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    /// <summary>
    /// Changes the music sound to a new one
    /// </summary>
    /// <param name="activeSource">Current audioSource playing</param>
    /// <param name="newClip">New music clip</param>
    /// <param name="transitionTime">Transition time between music clips</param>
    /// <returns></returns>
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime){
        //Check that the source is active and playing
        if(!activeSource.isPlaying){
            activeSource.Play();
        }

        //Fade out float
        float t = 0.0f;

        //Fade out over time
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            //Reduce the volume of the active source
            activeSource.volume = (managerMusicVolume - (t/transitionTime));

            yield return null;
        }

        //Stop the active source
        activeSource.Stop();

        //Set the new clip
        activeSource.clip = newClip;
        //Play the new clip on the source
        activeSource.Play();

        //Fade in over time
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            //Increase the volume of the active source
            activeSource.volume = (t/transitionTime) * managerMusicVolume;

            yield return null;
        }
    }

    /// <summary>
    /// Changes the volume of the old music source and the new source over time
    /// </summary>
    /// <param name="original">Old audio source</param>
    /// <param name="newSource">New audio source</param>
    /// <param name="transitionTime">Transition time of fade</param>
    /// <returns></returns>
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime){
        //Float variable to track transition
        float t = 0.0f;

        //Loop until t has reached transition time
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            //lower volume of original source
            original.volume = (managerMusicVolume - (t/transitionTime));
            //Increase volume of new source
            newSource.volume = (t/transitionTime) * managerMusicVolume;

            yield return null;
        }

        //stop the original source
        original.Stop();
    }

    /// <summary>
    /// Plays a sound effect
    /// </summary>
    /// <param name="soundClip">Sound to play</param>
    public void PlaySFX(AudioClip soundClip){
        //Plays the sound effect once
        sfxSource.PlayOneShot(soundClip);
    }

    /// <summary>
    /// Plays a sound effect
    /// </summary>
    /// <param name="soundClip">Sound to play</param>
    /// <param name="volume">Volume of sound</param>
    public void PlaySFX(AudioClip soundClip, float volume){
        //Plays the sound effect once with the volume setting
        sfxSource.PlayOneShot(soundClip,volume);
    }

    /// <summary>
    /// Sets the volume of the music sources
    /// </summary>
    /// <param name="volume">Volume level</param>
    public void SetMusicVolume(float volume){
        //Set the volume level of both music sources
        musicSource.volume = volume;
        dungeonMusicSource.volume = volume;

        //Change the manager music level to volume
        managerMusicVolume = volume;
    }

    /// <summary>
    /// Sets the volume of the sfx source
    /// </summary>
    /// <param name="volume">Volume level</param>
    public void SetSFXVolume(float volume){
        //Set the volume of the sfx source
        sfxSource.volume = volume;

        //Change the manager sfx level to volume
        managerSFXVolume = volume;
    }
}
