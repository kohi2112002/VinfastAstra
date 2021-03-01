using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum GameState
{
    Start = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Crash = 4
}
public class GameController : MonoBehaviour
{
    private GameState state = GameState.Start;
    private float startTime;
    private bool isReady = false;
    [SerializeField] private Transform player;
    private int score, totalTime;
    public UIController _UIController;
    public Text scoreText;
    public Slider energySlider;
    [SerializeField] private Text countDownText;
    [SerializeField] private AudioSource countDownAudio, outOfEnergyAudio, winAudio;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }
    private float energy = 80;
    public float Energy
    {
        get { return energy; }
        set { energy = Mathf.Min(80, value); }
    }
    public bool HasReachOffice
    {
        get { return state != GameState.Start && Time.time - startTime > 180; }
    }
    void Start()
    {
        StartCoroutine(GameLoop());
    }
    public void ReadyButtonClick()
    {
        _UIController.TurnOffTutorial();
        StartCoroutine(CountDown());
    }
    private IEnumerator CountDown()
    {
        int count = 3;
        countDownText.gameObject.SetActive(true);
        countDownAudio.Play();
        while (count > 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countDownAudio.Stop();
        isReady = true;
        countDownText.text = "GO";
        yield return new WaitForSeconds(0.5f);
        countDownText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isReady && state == GameState.Start)
            state = GameState.Playing;
    }
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStart());
        yield return StartCoroutine(GamePlay());
        yield return StartCoroutine(GameEnd());
    }
    private IEnumerator GameStart()
    {
        while (state == GameState.Start)
            yield return null;
        startTime = Time.time;
        energy = 80;
        FindObjectOfType<PlayerController>().StartRunning();
    }
    private IEnumerator GamePlay()
    {
        while (state == GameState.Playing)
        {
            energy -= Time.deltaTime * 4;
            energySlider.value = energy / 10;
            if (energy <= 0)
                state = GameState.Lose;
            yield return null;
            if (!HasReachOffice)
                FindObjectOfType<PropManager>().SpawnProp((int)(player.position.z));
        }
    }
    private IEnumerator GameEnd()
    {
        if (state == GameState.Lose)
            outOfEnergyAudio.Play();
        else
            if (state == GameState.Win)
            winAudio.Play();
        FindObjectOfType<PlayerController>().StopRunning();
        totalTime = (int)(Time.time - startTime);
        _UIController.GameOver((int)state - 2, score, totalTime);
        yield return null;
    }
    public void OnReachOffice()
    {
        state = GameState.Win;
    }
    public void OnCrash()
    {
        state = GameState.Crash;
    }
}
