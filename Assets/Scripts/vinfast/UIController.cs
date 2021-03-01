using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public GameObject TutorialScreen;
    public GameObject PlayScreen;
    public GameObject GameOverScreen;
    public GameObject InputNameScreen;
    public GameObject GuideScreen;
    public GameObject[] gameOverState;
    public Text scoreGameOver;
    public Text distance;
    private int currentScore;
    private string curentName;
    // Use this for initialization
    void Start()
    {

    }
    public void TurnOnGameObj(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
    }
    public void TurnOffTutorial()
    {
        TutorialScreen.SetActive(false);
        PlayScreen.SetActive(true);
    }
    public void GameOver(int index, int score, int totalTime)
    {
        GameOverScreen.SetActive(true);
        gameOverState[index].SetActive(true);
        scoreGameOver.text = score.ToString();
        distance.text = (totalTime * 1).ToString() + " km";
        currentScore = score;
    }
    public void StopButtonClick()
    {
        UserData[] datas = DataManager.Instance.dataCollection.datas;
        if (datas.Length < 10)
        {
            GameOverScreen.SetActive(false);
            InputNameScreen.SetActive(true);
        }
        else if (datas.Length == 10)
        {
            if (currentScore <= datas[9].score)
            {
                PlayerPrefs.SetString("Home", "Home");
                SceneManager.LoadScene("Menu");
            }
            else
            {
                GameOverScreen.SetActive(false);
                InputNameScreen.SetActive(true);
            }
        }

    }
    public void InputName(string nameInput)
    {
        curentName = nameInput;
    }
    public void SaveButtonClick()
    {
        DataManager.Instance.SaveScore(curentName, currentScore);
        PlayerPrefs.SetString("Home", "Home");
        SceneManager.LoadScene("Menu");
    }
    public void ReplayButtonClick()
    {
        PlayerPrefs.SetString("Home", "Gara");
        SceneManager.LoadScene("Menu");
    }

}
