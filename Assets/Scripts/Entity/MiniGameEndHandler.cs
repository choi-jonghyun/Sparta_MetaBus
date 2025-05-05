using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameEndHandler : MonoBehaviour
{
    ScoreManager scoreManager;
    public int finalscore;
    public TextMeshProUGUI scoreText; //텍스트 오브젝트 연결


    private void Start()
    {
        scoreManager = ScoreManager.instance;

       
       
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
       
        
        scoreText.text = $"{lastScore }";
    }

   
    public void EndMiniGame()
    {
        int finalScore = ScoreManager.instance.currentScore;
        
        ScoreManager.instance.SaveHighScore();
        //현재점수저장
        PlayerPrefs.SetInt("LastScore", finalScore);
        //최고점수저장
        PlayerPrefs.Save();
        

        UnityEngine.SceneManagement.SceneManager.LoadScene("MetaBusScene");
    }

}
