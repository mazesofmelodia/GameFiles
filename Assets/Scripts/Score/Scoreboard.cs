using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxEntries = 10;                   //Max number of entries
    [SerializeField] private Transform scoreHolder = null;          //Holder of the score entries
    [SerializeField] private GameObject scoreEntryObject = null;    //Entry Object

    [Header("Test")]
    [SerializeField] private string testEntryName = "New Name";
    [SerializeField] private int testEntryScore = 0;

    //Reference to the savepath of the savedata
    private string savePath;

    private void Start()
    {
        //Set the savepath for the score data
        savePath = $"{Application.persistentDataPath}/highscores.dat";

        //Get the saved scores
        ScoreboardSaveData savedScores = GetSavedScores();

        //Save the scores immediately
        SaveScores(savedScores);
    }

    [ContextMenu("Add Test Entry")]
    public void AddTestEntry()
    {
        //Add the test data to the scores
        AddEntry(new ScoreEntryData() {
            entryName = testEntryName,
            entryScore = testEntryScore
        });
    }

    [ContextMenu("Delete HighScore File")]
    public void DeleteFile()
    {
        if (File.Exists(savePath))
        {
            //Delete the save path
            File.Delete(savePath);

            //Get the saved scores
            ScoreboardSaveData savedScores = GetSavedScores();

            //Save the scores immediately
            SaveScores(savedScores);

            //Update the score UI
            UpdateUI(savedScores);
        }
    }

    public void ListScores()
    {
        //Get the saved scores
        ScoreboardSaveData savedScores = GetSavedScores();

        //Display the UI
        UpdateUI(savedScores);
    }

    public void AddEntry(ScoreEntryData entryData)
    {
        //Get the saved scores
        ScoreboardSaveData savedScores = GetSavedScores();

        bool scoreAdded = false;

        //Loop through all saved scores
        for (int i = 0; i < savedScores.highScores.Count; i++)
        {
            //Check if the entry score is greater than this score
            if(entryData.entryScore > savedScores.highScores[i].entryScore)
            {
                //Add the score to that point in the list
                savedScores.highScores.Insert(i, entryData);

                //Score has now been added
                scoreAdded = true;

                //Break out of the loop
                break;
            }
        }

        //Check if the score has not been added and the saved scores is lower than the max scores
        if (!scoreAdded && savedScores.highScores.Count < maxEntries)
        {
            //Add the entry data as a new item on the list
            savedScores.highScores.Add(entryData);
        }

        //If the number of saved scores is higher than the max entries
        if(savedScores.highScores.Count > maxEntries)
        {
            //Remove a range of entries until the number is equal to the max entries
            savedScores.highScores.RemoveRange(maxEntries, savedScores.highScores.Count - maxEntries);
        }

        //Save the scores
        SaveScores(savedScores);

        //Update the UI
        UpdateUI(savedScores,entryData);
    }

    private ScoreboardSaveData GetSavedScores()
    {
        //Set the savepath for the score data
        savePath = $"{Application.persistentDataPath}/highscores.dat";
        //If the file exists
        if (!File.Exists(savePath))
        {
            //Create the savepath file
            File.Create(savePath).Dispose();

            //return new instance
            return new ScoreboardSaveData();
        }

        //Otherwise read the stream data
        /*using (StreamReader stream = new StreamReader(savePath))
        {
            //Read the whole data
            string json = stream.ReadToEnd();

            //Return the converted JSON data
            return JsonUtility.FromJson<ScoreboardSaveData>(json);
        }*/

        FileStream file;

        file = File.OpenRead(savePath);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        ScoreboardSaveData savedScores = (ScoreboardSaveData)binaryFormatter.Deserialize(file);

        file.Close();

        return savedScores;
    }

    private void SaveScores(ScoreboardSaveData scoreboardSaveData)
    {
        //Set the savepath for the score data
        savePath = $"{Application.persistentDataPath}/highscores.dat";
        //Write the stream data to the stream path
        /*using (StreamWriter stream = new StreamWriter(savePath))
        {
            //Write the save data to json
            string json = JsonUtility.ToJson(scoreboardSaveData, true);

            //Write the data
            stream.Write(json);
        }*/
        FileStream file;

        if (File.Exists(savePath))
        {
            file = File.OpenWrite(savePath);
        }
        else
        {
            file = File.Create(savePath);
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        binaryFormatter.Serialize(file, scoreboardSaveData);

        file.Close();
    }

    private void UpdateUI(ScoreboardSaveData savedScores)
    {
        if(savedScores.highScores.Count == 0)
        {
            //Disable the highscores holder
            scoreHolder.gameObject.SetActive(false);

            return;
        }

        //If the holder game object is disabled
        if(scoreHolder.gameObject.activeSelf == false)
        {
            //Activate the game object
            scoreHolder.gameObject.SetActive(true);
        }
        //Loop through all child objects in scoreHoler
        foreach (Transform child in scoreHolder)
        {
            //Destroy the child object
            Destroy(child.gameObject);
        }

        //Loop through all the entries in savedScores
        foreach (ScoreEntryData highScore in savedScores.highScores)
        {
            //Spawn the entry UI and initalize the entry
            Instantiate(scoreEntryObject, scoreHolder).GetComponent<ScoreEntryUI>().Initialize(highScore);
        }
    }

    private void UpdateUI(ScoreboardSaveData savedScores, ScoreEntryData newData)
    {
        if (savedScores.highScores.Count == 0)
        {
            //Disable the highscores holder
            scoreHolder.gameObject.SetActive(false);

            return;
        }

        //If the holder game object is disabled
        if (scoreHolder.gameObject.activeSelf == false)
        {
            //Activate the game object
            scoreHolder.gameObject.SetActive(true);
        }
        //Loop through all child objects in scoreHoler
        foreach (Transform child in scoreHolder)
        {
            //Destroy the child object
            Destroy(child.gameObject);
        }

        //Loop through all the entries in savedScores
        foreach (ScoreEntryData highScore in savedScores.highScores)
        {
            //Spawn the entry UI
            ScoreEntryUI scoreListing = Instantiate(scoreEntryObject, scoreHolder).GetComponent<ScoreEntryUI>();

            //Initalise the UI
            scoreListing.Initialize(highScore);

            //check if the new data matches the current highscore
            if (newData.Equals(highScore))
            {
                scoreListing.HighlightUI();
            }
        }
    }
}
