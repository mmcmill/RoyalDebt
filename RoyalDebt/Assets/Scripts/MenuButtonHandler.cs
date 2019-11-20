using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void NarrativeOverview()
    {
        SceneManager.LoadScene("NarrativeOverview");
    }


}
