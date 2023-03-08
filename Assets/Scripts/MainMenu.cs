using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void SettingsGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void BackGame()
    {
        SceneManager.LoadScene(0);
    }
}
