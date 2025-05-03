using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MiniGameUIController : MonoBehaviour
{

   
    public void RestartMiniGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //현재 씬 다시 로드
    }

    public void ExitToMainMap()
    {
        SceneManager.LoadScene("MetaBusScene"); 
    }
    public void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
    }
}
