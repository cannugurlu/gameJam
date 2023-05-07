using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void PlayBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
}
