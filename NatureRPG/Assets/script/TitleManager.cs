using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("Project");
    }

    public void GoToGuide()
    {
        SceneManager.LoadScene("Guide");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
