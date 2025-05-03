using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager instance {  get; private set; } //전역 접근용
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public bool isMiniGame = false;
    private const string BestScoreKey = "HighScore";
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
        score = 0;
    }

    public void SaveHighScore()
    {
        //int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", score);
            //PlayerPrefs.Save();
        }
    }

    void Start()
    {
        if(scoreText == null)
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt(BestScoreKey);
        UpdateScoreUI(score);
     }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI(score);
    }

    public void UpdateScoreUI(int score)
    {

        scoreText.text = score.ToString();
        
                          
    }

    public void ResetScore(int score)
    {
        score = 0 ;
        UpdateScoreUI(score) ;
    }
}
