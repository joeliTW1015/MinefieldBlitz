using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MinesweeperManager mineManager;
    public GameObject player;
    public Text mineCountText, timeText;
    public GameObject bigHp, bigBlack;
    public Image hp1, hp2, hp3;

    public GameObject losePanal, winPanal;
    public Text screText, bestTimeText;
    AudioManager audioManager;
    float timeSecond, bestSecond;
    public static int mineCount;

    void Start()
    {
        timeSecond = 0;
        mineCount = mineManager.mineNum;
        losePanal.SetActive(false);
        winPanal.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
        switch (ModeManager.mode)
        {
            case 1:
                bestSecond = PlayerPrefs.GetFloat("Easy", 0);
                break;
            case 2:
                bestSecond = PlayerPrefs.GetFloat("Medium", 0);
                break;
            case 3: 
                bestSecond = PlayerPrefs.GetFloat("Hard", 0);
                break;
            default: return;
        }
    }

    private void Update() {
        timeSecond += Time.deltaTime;
        timeText.text ="Time: " + (timeSecond / 60).ToString("00") + ":" + (timeSecond % 60).ToString("00"); 
        mineCountText.text = "Mine: " + (mineCount).ToString("00");
    }

    public void UpdateHP()
    {
        if(PlayerDie.hP == 3)
        {
            hp1.enabled = true;
            hp2.enabled = true;
            hp3.enabled = true;
        }
        else if(PlayerDie.hP == 2)
        {
            hp1.enabled = false;
            hp2.enabled = true;
            hp3.enabled = true;
        }
        else if(PlayerDie.hP == 1)
        {
            hp1.enabled = false;
            hp2.enabled = false;
            hp3.enabled = true;
        }
        else if(PlayerDie.hP == 0)
        {
            hp1.enabled = false;
            hp2.enabled = false;
            hp3.enabled = false;
        }
    }

    public void LoseGame()
    {
        audioManager.lose.Play();
        Time.timeScale = 0;
        bigHp.SetActive(false);
        bigBlack.SetActive(false);
        losePanal.SetActive(true);
        Camera.main.GetComponent<CamFollow>().enabled = false;
        Camera.main.orthographicSize = 20;
        Camera.main.transform.position = new Vector3(mineManager.width/2, mineManager.height/2 + 2, -10f);
    }

    public void WinGame()
    {
        audioManager.win.Play();
        Time.timeScale = 0;
        bigHp.SetActive(false);
        bigBlack.SetActive(false);
        winPanal.SetActive(true);
        Camera.main.GetComponent<CamFollow>().enabled = false;
        Camera.main.orthographicSize = 20;
        Camera.main.transform.position = new Vector3(mineManager.width/2, mineManager.height/2 + 2, -10f);
        screText.text = timeText.text;

        if(bestSecond > timeSecond || bestSecond == 0f)
        {
            switch (ModeManager.mode)
            {
                case 1:
                    PlayerPrefs.SetFloat("Easy", timeSecond);
                    break;
                case 2:
                    PlayerPrefs.SetFloat("Medium", timeSecond);
                    break;
                case 3: 
                    PlayerPrefs.SetFloat("Hard", timeSecond);
                    break;
                default: return;
            }

            switch (ModeManager.mode)
            {
                case 1:
                    bestSecond = PlayerPrefs.GetFloat("Easy", 0);
                    break;
                case 2:
                    bestSecond = PlayerPrefs.GetFloat("Medium", 0);
                    break;
                case 3: 
                    bestSecond = PlayerPrefs.GetFloat("Hard", 0);
                    break;
                default: return;
            }
        }

        bestTimeText.text ="Best record: " + (bestSecond / 60).ToString("00") + ":" + (bestSecond % 60).ToString("00");
    }

    public void BackToMenu()
    {
        audioManager.button.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
