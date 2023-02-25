using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject DifficultyMenu;
    public void StartBtn()
    {
        MainMenu.SetActive(false);
        DifficultyMenu.SetActive(true);
    }

    public void EndBtn()
    {
        Application.Quit();
    }

    public void Easy()
    {
        GameManager.Instance.difficulty = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void Medium()
    {
        GameManager.Instance.difficulty = 2;
        SceneManager.LoadScene("SampleScene");
    }

    public void Hard()
    {
        GameManager.Instance.difficulty = 3;
        SceneManager.LoadScene("SampleScene");
    }

    public void INSANE()
    {
        GameManager.Instance.difficulty = 4;
        SceneManager.LoadScene("SampleScene");
    }
}
