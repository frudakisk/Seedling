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
        int highscore = DataManager.Instance.highscoreTime;
        if(highscore == 1000)
        {
            return "Highscore: None";
        }
        string minutes = Mathf.Floor(highscore / 60).ToString("00");
        string seconds = (highscore % 60).ToString("00");
        return $"Highscore: {minutes}:{seconds}";
    }
}
