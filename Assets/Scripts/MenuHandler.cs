using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public GameObject inputFieldGO;
    private TMP_InputField inputField;
    public List<TextMeshProUGUI> highScoreTexts;
    public GameObject highScoreScreen;
    private void Start()
    {
        inputField=inputFieldGO.GetComponent<TMP_InputField>();
        FillHighScores();
    }

    public void StartGame()
    {
        GameManager.Instance.playerName = inputField.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    void FillHighScores()
    {
        List<GameManager.HighScore> top10Scores = GameManager.Instance.top10Scores;
        for(int i=0; i<top10Scores.Count && i<10 ;i++)
        {
             highScoreTexts[i].text = (i+1)+"- "+ top10Scores[i].name + " - " + top10Scores[i].score;
        }
    }

    public void ShowHighScores()
    {
        highScoreScreen.SetActive(true);
    }
    public void CloseHighScores()
    {
        highScoreScreen.SetActive(false);
    }
}
