using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuCanvas : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public TextMeshProUGUI highscoreText;

    private void Update()
    {
        highscoreText.text = ConvertHighscoreToTime();
    }

    public void ToggleHowToPlayPanel()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        DataManager.Instance.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private string ConvertHighscoreToTime()
    {
        float highscore = DataManager.Instance.highscoreTime;
        if(highscore == 1000)
        {
            return "Highscore: None";
        }
        int minutes = Mathf.FloorToInt(highscore / 60);
        int seconds = Mathf.FloorToInt(highscore % 60);
        int milliseconds = Mathf.FloorToInt((highscore * 1000) % 1000);

        string timerString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return "Time: " + timerString;
    }
}
