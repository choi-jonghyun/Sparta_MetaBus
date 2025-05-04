using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMeta : MonoBehaviour
{
   public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MetaBusScene");
    }
}
