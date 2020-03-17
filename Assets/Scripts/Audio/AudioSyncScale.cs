using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits from AudioSyncer
public class AudioSyncScale : AudioSyncer
{
    public Vector3 beatScale;       //Scale when a beat is detected
    public Vector3 restScale;       //Scale when no beat is detected

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();

        //Ignore if a beat is currently playing
        if (m_isBeat) return;

        //Scale the object back to it's rest cale over time
        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        //Stop the coroutine
        StopCoroutine("MoveToScale");

        //Start the coroutine again
        StartCoroutine("MoveToScale", beatScale);
    }

    private IEnumerator MoveToScale(Vector3 _target)
    {
        //Get the current local scale
        Vector3 _curr = transform.localScale;
        //Record it as the initial scale
        Vector3 _initial = _curr;

        //set the time to 0
        float _timer = 0;

        //While the current scale is not the target scale
        while (_curr != _target)
        {
            //Move to the scale over time
            _curr = Vector3.Lerp(_initial, _target, _timer / timeToBeat);

            //Increase the time
            _timer += Time.deltaTime;

            //Set the scale to the current scale
            transform.localScale = _curr;

            yield return null;
        }

        //Set the beat to be false
        m_isBeat = false;
    }
}
