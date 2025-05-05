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

    public void SaveHighScore()
    {
        //int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
            //PlayerPrefs.Save();
        }
    }

    void Start()
    {
        if(scoreText == null)
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreUI();
     }

    public void AddScore(int amount)
    {
        currentScore += amount;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {

        scoreText.text = $"{currentScore}";
        bestScoreText.text = $"{bestScore}";
        
                          
    }

    public void ResetScore(int score)
    {
        currentScore = 0 ;
        UpdateScoreUI() ;
    }
}
