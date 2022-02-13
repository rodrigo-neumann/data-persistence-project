using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string highScorePlayerName="Rando";
    public int highScorePlayerScore=0;

    public string playerName;

    public List<HighScore> top10Scores = new List<HighScore>();


    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadTopScores();
        if (top10Scores != null)
        {
            if(top10Scores.Count>0)
            {
                highScorePlayerName = top10Scores[0].name;
                highScorePlayerScore = top10Scores[0].score;
            }
        }
    }

    [System.Serializable]
    public class HighScore
    {
        public string name;
        public int score;
    }

    public void SaveHighScore()
    {
        HighScore dataToSave = new HighScore();
        dataToSave.name = highScorePlayerName;
        dataToSave.score = highScorePlayerScore;

        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if(File.Exists(path))
        {
            string text=File.ReadAllText(path);
            HighScore highScore = JsonUtility.FromJson<HighScore>(text);
            highScorePlayerScore = highScore.score;
            highScorePlayerName = highScore.name;
        }
    }

    static int SortByScore(HighScore p1, HighScore p2)
    {
        return -p1.score.CompareTo(-p2.score);
    }
    
    public void AddScoreToTopScores (string playerName, int playerScore)
    {
        HighScore newScore = new HighScore();
        newScore.name = playerName;
        newScore.score = playerScore;

        top10Scores.Add(newScore);
        top10Scores.Sort(SortByScore);
        if (top10Scores.Count>10)
        {
            top10Scores = top10Scores.GetRange(0, 10);
        }
        SaveTopScores();
    }
    [System.Serializable]
    class Top10
    {
        public List<HighScore> list;
    }

    void SaveTopScores()
    {
        Top10 top10 = new Top10();
        top10.list = top10Scores;
        string json = JsonUtility.ToJson(top10);
        File.WriteAllText(Application.persistentDataPath + "/Top10HighScores.json", json);
    }

    void LoadTopScores()
    {
        string path = Application.persistentDataPath + "/Top10HighScores.json";
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Top10 top10 = JsonUtility.FromJson<Top10>(text);
            top10Scores = top10.list;
        }
    }
}
