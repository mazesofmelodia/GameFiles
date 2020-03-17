using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSyncColor : AudioSyncer
{
    public Color[] beatColors;          //Array of possible colours
    public Color restColor;             //Resting colour

    private int m_randomIndx;           //Random colour selection
    private Image m_img;                //Image component

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the image component
        m_img = GetComponent<Image>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Ignore this function if a beat is currently happening
        if (m_isBeat) return;

        //Gradually change the colour to the rest colour
        m_img.color = Color.Lerp(m_img.color, restColor, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        //Select a random colour
        Color _c = RandomColor();

        //Stop the coroutine
        StopCoroutine("MoveToColor");

        //Start the corountine changing to the random colour
        StartCoroutine("MoveToColor", _c);
    }

    private Color RandomColor()
    {
        //If there are no beat colours to choose from return White
        if (beatColors == null || beatColors.Length == 0) return Color.white;

        //Select a random colour
        m_randomIndx = Random.Range(0, beatColors.Length);

        //Return that colour
        return beatColors[m_randomIndx];
    }

    private IEnumerator MoveToColor(Color _target)
    {
        //Get the current image colour
        Color _curr = m_img.color;

        //Set that as the initial image colour
        Color _initial = _curr;

        //Define a timer variable
        float _timer = 0;

        //While the colour hasn't reached the target colour
        while (_curr != _target)
        {
            //Gradually change the colour over time based on the time to beat
            _curr = Color.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            m_img.color = _curr;

            yield return null;
        }

        //No longer causing a beat
        m_isBeat = false;
    }
}
