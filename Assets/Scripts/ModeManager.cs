using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    public static int mode; 
    public Text eT, mT, hT;
    public GameObject controlsPanal;
    AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        controlsPanal.SetActive(false);
        mode = -1;
        float time = PlayerPrefs.GetFloat("Easy", 0);
        if(time > 0)
        {
            eT.text += "\n"  + (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
        }
        else
        {
            eT.text += "\n-----";
        }

         time = PlayerPrefs.GetFloat("Medium", 0);
        if(time > 0)
        {
            mT.text += "\n"  + (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
        }
        else
        {
            mT.text += "\n-----";
        }

         time = PlayerPrefs.GetFloat("Hard", 0);
        if(time > 0)
        {
            hT.text += "\n"  + (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
        }
        else
        {
            hT.text += "\n-----";
        }
    }

    public void Easy()
    {
        mode = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Medium()
    {
        mode = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Hard()
    {
        mode = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowPanal()
    {
        controlsPanal.SetActive(true);
    }

    public void LeavePanal()
    {
        controlsPanal.SetActive(false);
    }

    public void ButtonSound()
    {
        audioManager.button.Play();
    }
}
