using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class SaveLoadTimes : MonoBehaviour
{
    private string filePath;
    const string saveFolder = "/SavedTimes/";


    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        string folder = scene.name;


        filePath = Application.persistentDataPath + saveFolder + folder + "/";
        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        //bool exists = Directory.Exists(Application.persistentDataPath);
        //if (!exists) Directory.CreateDirectory(filePath);
        Debug.Log(filePath);
    }
    public void SaveData(float value, string checkpointIndex, out bool isNewRecord, out float timeDifference)
    {
        isNewRecord = false;
        timeDifference = 0;

        
        // Check if the file exists
        if (File.Exists(filePath + checkpointIndex + ".json"))
        {
            // Read the existing saved time
            float recordedTime = LoadData(checkpointIndex + ".json");
            timeDifference = value - recordedTime;
            // Only save if the new time is less than the existing time
            if (timeDifference < 0)
            {
                isNewRecord = true;
                Save(value, checkpointIndex);
            }
            else if(timeDifference >= 0)
            {
                isNewRecord = false;
            }
        }
        else
        {
            isNewRecord = false;
            Save(value, checkpointIndex);
        }
    }
    private void Save(float value, string checkpointIndex)
    {

        // Create a data container
        TimeData data = new TimeData();
        data.totalTimeValue = value;

        // Convert the data to JSON format
        string json = JsonUtility.ToJson(data);

        // Write the JSON data to a file
        File.WriteAllText(filePath + checkpointIndex + ".json", json);
    }

    public float LoadData(string fileName)
    {
        // Check if the file exists
        if (File.Exists(filePath + fileName))
        {
            // Read the JSON data from the file
            string json = File.ReadAllText(filePath + fileName);

            // Convert JSON data back to TimeData object
            TimeData data = JsonUtility.FromJson<TimeData>(json);

            // Return the float value
            return data.totalTimeValue;
        }
        else
        {
            return 0f; // Return default value if file doesn't exist
        }
    }

    public void ClearSavedData()
    {
        System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + saveFolder);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }


    [System.Serializable]
    private class TimeData
    {
        public float totalTimeValue;
    }
}
