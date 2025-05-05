using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int bestScore = 0;
    private int currentScore = 0;
    ScoreManager scoreManager;

    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    [SerializeField] private MiniGameEndHandler endHandler;

    private void Awake()
    {
        gameManager = this;
        uiManager=FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        ScoreManager.instance.UpdateScoreUI();
    }
    public void GameOver()
    {
        ScoreManager.instance.SaveHighScore();
      
        uiManager.SetRestart();
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    //public void AddScore(int currentScore)
    //{
    //    currentScore += currentScore;
    //    Debug.Log("Score:" + currentScore);
    //    ScoreManager.instance.UpdateScoreUI(currentScore);
       
    //}

}
