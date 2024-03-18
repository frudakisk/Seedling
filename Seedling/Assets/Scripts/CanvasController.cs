using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private RootController player;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public Button resetButton;
    public GameObject gameOverPanel;

    private float timer;
    private bool routineOn;

    private bool panelActive;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("root").GetComponent<RootController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOn)
        {
            startText.gameObject.SetActive(false);
            resetButton.gameObject.SetActive(false);
            if(!routineOn)
            {
                routineOn = true;
                StartCoroutine(Timer());
            }
        }

        if(GameManager.gameOver && !panelActive)
        {
            CompareHighscore();
            panelActive = true;
            StartCoroutine(ShowGameOverPanel());
        }
        
    }

    private void CompareHighscore()
    {
        if(timer < DataManager.Instance.highscoreTime && player.playerWon)
        {
            DataManager.Instance.highscoreTime = timer;
        }
    }

    private IEnumerator ShowGameOverPanel()
    {
        if(player.playerWon)
        {
            gameOverText.text = $"You Won!\n{timerText.text}";
        }
        else
        {
            gameOverText.text = "Game Over";
        }
        yield return new WaitForSeconds(3.0f);
        gameOverPanel.SetActive(!gameOverPanel.activeSelf);
        
    }

    private IEnumerator Timer()
    {
        while (!GameManager.gameOver)
        {
            UpdateTimer(timer);
            yield return null;
            timer += Time.deltaTime;
            
        }

    }

    private void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        string timerString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        timerText.text = "Time: " + timerString;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.gameOver = false;
        GameManager.gameOn = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.gameOver = false;
        GameManager.gameOn = false;
    }

}
