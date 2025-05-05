using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager instance {  get; private set; } //전역 접근용
    public int currentScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public bool isMiniGame = false;
    private bool isGameOver = false;
    
    public int bestScore = 0;

    private void Awake()
    {
        //if(instance == null)
        //{
            instance = this;
        //    DontDestroyOnLoad(gameObject); //씬 전환 시 파괴x
        //}
        //else
        //{
        //    Destroy(gameObject); //중복방지
        //}
        currentScore = 0;
    }

    //public void SaveHighScore()
    //{
    //    //int bestscore = PlayerPrefs.GetInt("Highscore", 0);
    //    if (currentScore > bestScore)
    //    {
    //        bestScore = currentScore;
    //        PlayerPrefs.SetInt("BestScore", currentScore);
    //        PlayerPrefs.Save();
    //    }
    //}

    void Start()
    {
        if(scoreText == null)
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreUI();
     }
    public void SaveHighScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        currentScore += amount;

      
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {

        scoreText.text = $"Score : {currentScore}";
        bestScoreText.text = $"BestScore : {bestScore}";
        
                          
    }

    public void ResetScore(int score)
    {
        currentScore = 0 ;
        UpdateScoreUI() ;
    }
}
