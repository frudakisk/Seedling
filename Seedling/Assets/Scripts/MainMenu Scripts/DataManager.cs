using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int highscoreTime = 1000;

    private AudioSource audioSource;
    private bool songPlaying;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.highscoreTime = highscoreTime;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highscoreTime = data.highscoreTime;
        }
    }

    public void Reset()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    //primarily for music
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay" && !songPlaying)
        {
            Debug.Log("Playing gameplay song");
            songPlaying = true;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name != "Gameplay" && songPlaying)
        {
            Debug.Log("Stopping song");
            songPlaying = false;
            audioSource.Stop();
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int highscoreTime;
    }

}
