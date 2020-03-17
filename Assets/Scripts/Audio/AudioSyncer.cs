using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public float bias;                      //What spectrum value will trigger a beat
    public float timeStep;                  //Minimum interval between each beat
    public float timeToBeat;                //Time to complete audio visualization
    public float restSmoothTime;            //How fast the object rests after a beat

    private float m_previousAudioValue;     //What the previous audio value was
    private float m_audioValue;             //The current audio value, checks if there's enough change to cause a beat
    private float m_timer;                  //Timestamp 

    protected bool m_isBeat;                //Is the object currently in a beat state

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    /// <summary>
	/// Inherit this to cause some behavior on each beat
	/// </summary>
	public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }

    /// <summary>
    /// Inherit this to do whatever you want in Unity's update function
    /// Typically, this is used to arrive at some rest state..
    /// ..defined by the child class
    /// </summary>
    public virtual void OnUpdate()
    {
        // update audio value
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.SpectrumValue;

        // if audio value went below the bias during this frame
        if (m_previousAudioValue > bias &&
            m_audioValue <= bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        // if audio value went above the bias during this frame
        if (m_previousAudioValue <= bias &&
            m_audioValue > bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        m_timer += Time.deltaTime;
    }
}
