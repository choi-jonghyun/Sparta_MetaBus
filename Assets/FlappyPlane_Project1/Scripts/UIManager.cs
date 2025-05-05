using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI bestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");

        

        restartText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive (true);
        scoreText.gameObject.SetActive (true);
        bestScoreText.gameObject.SetActive (true);
    }

   
  
}
