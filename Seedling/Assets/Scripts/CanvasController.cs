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
            panelActive = true;
            StartCoroutine(ShowGameOverPanel());
        }
        
    }

    private IEnumerator Timer()
    {
        while(!GameManager.gameOver)
        {
            UpdateTimer(timer);
            yield return new WaitForSeconds(1.0f);
            timer++;
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

    private void UpdateTimer(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        timerText.text = $"Time: {minutes}:{seconds}";
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
