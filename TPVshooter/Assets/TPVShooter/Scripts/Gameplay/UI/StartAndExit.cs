using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndExit : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("DefaultGameScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
