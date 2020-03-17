using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    //Public accessable spectrum value
    public static float SpectrumValue { get; private set; }

    private float[] m_audioSpectrum;        //Array to serve music beats

    // Start is called before the first frame update
    void Start()
    {
        //Define the audio spectrum array
        m_audioSpectrum = new float[128];
    }

    // Update is called once per frame
    void Update()
    {
        //Get the spectrum data from the audio listener
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if(m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            SpectrumValue = m_audioSpectrum[0] * 100;
        }
    }
}
