using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager instance {  get; private set; } //���� ���ٿ�
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
        //    DontDestroyOnLoad(gameObject); //�� ��ȯ �� �ı�x
        //}
        //else
        //{
        //    Destroy(gameObject); //�ߺ�����
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
