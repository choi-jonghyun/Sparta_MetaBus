using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;   //상 하로 이동할 때 얼마나 이동 할 것인가.
    public float lowPosY = -1f; 

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f; //탑과 바텀 사이 공간

    public Transform topObject;
    public Transform bottomObject;  

    public float widthPadding = 4f; // 오브젝트 사이 폭
    ScoreManager scoreManager;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize=Random.Range(holeSizeMin,holeSizeMax);
        float halfHoleSize= holeSize/2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player=collision.GetComponent<Player>();
        if (player != null)
            ScoreManager.instance.AddScore(1);
    }

}
