using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private string highScorePlayerName;
    private int highScorePlayerScore;
    // Start is called before the first frame update
    void Start()
    {
        highScorePlayerName=GameManager.Instance.highScorePlayerName;
        highScorePlayerScore = GameManager.Instance.highScorePlayerScore;

        gameObject.GetComponent<Text>().text = "Best Score : " + highScorePlayerName + " : "+ highScorePlayerScore;
    }

}
