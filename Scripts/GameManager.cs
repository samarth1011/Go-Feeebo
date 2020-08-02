using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    public static GameManager Instance;
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    
    public Text scoreText;

    public Text scoreText1;


    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }
    int score = 0;
    int score1 = 0;
    
    bool gameOver = true;
    public bool GameOver
    {
        get { return gameOver; }
    }
    public int Score { get { return Score; } }
    
    public int Score1 { get { return Score1; } }
    void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }
    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
    }
    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }
    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            score = 0;
        }
        
        if(SceneManager.GetActiveScene().buildIndex == 2)        {
            score1 = 0;
        }
        gameOver = false;

    }
    void OnPlayerDied()
    {
       
        int savedScore = PlayerPrefs.GetInt("HighScore");
        int savedScore1 = PlayerPrefs.GetInt("HighScore1");

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameOver = true;
            if (score > savedScore)
            {    gameOver = true;
                PlayerPrefs.SetInt("HighScore", score);
            }
            SetPageState(PageState.GameOver);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameOver = true;
            if (score1 > savedScore1)
            {
                PlayerPrefs.SetInt("HighScore1", score1);
            }
            SetPageState(PageState.GameOver);
        }
        
        
    }
    void OnPlayerScored()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            score++;
            scoreText.text = score.ToString();
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            score1++;
            scoreText1.text = score1.ToString();
        }
    }
    
    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }
    public void ConfirmGameOver()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            OnGameOverConfirmed();

            OnGameOverConfirmed();
            
            SetPageState(PageState.Start);
            scoreText.text = "0";
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            OnGameOverConfirmed();

            OnGameOverConfirmed();
            //scoreText1.text = "0";
            SetPageState(PageState.Start);
            scoreText1.text = "0";
        }
        
       
        
        
    }
    public void StartGame()
    {
        SetPageState(PageState.Countdown);
    }

    // Start is called before the first frame update

}
