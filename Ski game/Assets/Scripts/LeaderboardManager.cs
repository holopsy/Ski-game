using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    [Header("Leaderboard")]
    public int maxScores = 5;
    public string leaderboardKey = "Leaderboard_Level01";

    [Header("UI")]
    public TMP_Text leaderboardText;

    private List<float> scores = new List<float>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadScores();
        UpdateLeaderboardUI();
    }

    public void AddScore(float newScore)
    {
        LoadScores();

        scores.Add(newScore);
        scores.Sort();

        if (scores.Count > maxScores)
        {
            scores.RemoveRange(maxScores, scores.Count - maxScores);
        }

        SaveScores();
        UpdateLeaderboardUI();

        Debug.Log("Leaderboard updated");
    }

    void LoadScores()
    {
        scores.Clear();

        for (int i = 0; i < maxScores; i++)
        {
            string key = leaderboardKey + "_" + i;

            if (PlayerPrefs.HasKey(key))
            {
                scores.Add(PlayerPrefs.GetFloat(key));
            }
        }

        scores.Sort();
    }

    void SaveScores()
    {
        for (int i = 0; i < maxScores; i++)
        {
            string key = leaderboardKey + "_" + i;

            if (i < scores.Count)
            {
                PlayerPrefs.SetFloat(key, scores[i]);
            }
            else
            {
                PlayerPrefs.DeleteKey(key);
            }
        }

        PlayerPrefs.Save();
    }

    void UpdateLeaderboardUI()
    {
        if (leaderboardText == null)
        {
            return;
        }

        string text = "Leaderboard\n";

        for (int i = 0; i < maxScores; i++)
        {
            if (i < scores.Count)
            {
                text += (i + 1) + ". " + scores[i].ToString("F2") + "s\n";
            }
            else
            {
                text += (i + 1) + ". --\n";
            }
        }

        leaderboardText.text = text;
    }

    public void ResetLeaderboard()
    {
        for (int i = 0; i < maxScores; i++)
        {
            PlayerPrefs.DeleteKey(leaderboardKey + "_" + i);
        }

        scores.Clear();
        UpdateLeaderboardUI();

        Debug.Log("Leaderboard reset");
    }
}