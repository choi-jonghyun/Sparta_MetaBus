using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameInput : MonoBehaviour
{
    public string miniGameSceneName = "FlappyPlaneScene";

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (CanEnterMiniGame())
        //    {
        //        EnterMiniGame();
        //    }
        //}
    }

    private bool CanEnterMiniGame()
    {
        return true;
    }

    private void EnterMiniGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(miniGameSceneName);
    }
}
