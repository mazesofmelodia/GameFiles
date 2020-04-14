using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pages = null;      //List of all the pages in the instructions

    private int trackerNumber = 0;                               //Number reference to current page

    public void ShowNextPage()
    {
        //Deactivate the current page
        pages[trackerNumber].SetActive(false);

        //Increase the tracker number by 1
        trackerNumber++;

        //Check if the number has exceeded the array range
        if(trackerNumber > pages.Length - 1)
        {
            //Set the value to the end of the array
            trackerNumber = pages.Length - 1;
        }

        //Activate the page of the tracker number
        pages[trackerNumber].SetActive(true);
    }

    public void ShowPreviousPage()
    {
        //Deactivate the current page
        pages[trackerNumber].SetActive(false);

        //Decrease the tracker number by 1
        trackerNumber--;

        //Check if the number has gone below 0
        if (trackerNumber < 0)
        {
            //Set the value to 0
            trackerNumber = 0;
        }

        //Activate the page of the tracker number
        pages[trackerNumber].SetActive(true);
    }
}
