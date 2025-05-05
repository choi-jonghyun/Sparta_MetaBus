using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameEndHandler : MonoBehaviour
{
    ScoreManager scoreManager;
    public int finalscore;
    public TextMeshProUGUI scoreText; //�ؽ�Ʈ ������Ʈ ����


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
        //������������
        PlayerPrefs.SetInt("LastScore", finalScore);
        //�ְ���������
        PlayerPrefs.Save();
        

        UnityEngine.SceneManagement.SceneManager.LoadScene("MetaBusScene");
    }

}
