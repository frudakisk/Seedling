using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    public GameObject howToPlayPanel;
    // Start is called before the first frame update

    public void ToggleHowToPlayPanel()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
