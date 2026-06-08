using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [Header("Race State")]
    public bool raceStarted;
    public bool raceFinished;

    [Header("Time")]
    public float currentTime;
    public float penaltyTime;

    [Header("Best Time")]
    public float bestTime;
    private const string BestTimeKey = "BestTime_Level01";

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text finalTimeText;
    public TMP_Text bestTimeText;
    public GameObject raceFinishedPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = 0f;
        penaltyTime = 0f;
        raceStarted = false;
        raceFinished = false;

        LoadBestTime();

        if (raceFinishedPanel != null)
        {
            raceFinishedPanel.SetActive(false);
        }

        UpdateTimerUI();
        UpdateBestTimeUI();
    }

    void Update()
    {
        if (raceStarted && !raceFinished)
        {
            currentTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartRace()
    {
        if (raceStarted)
        {
            return;
        }

        raceStarted = true;
        raceFinished = false;
        currentTime = 0f;
        penaltyTime = 0f;

        Debug.Log("Race started");
        UpdateTimerUI();
    }

    public void AddPenalty(float amount)
    {
        if (!raceStarted || raceFinished)
        {
            return;
        }

        penaltyTime += amount;
        Debug.Log("Penalty added: +" + amount + " second");
        UpdateTimerUI();
    }

    public void FinishRace()
    {
        if (!raceStarted || raceFinished)
        {
            return;
        }

        raceFinished = true;

        float finalTime = GetFinalTime();

        Debug.Log("Race finished. Final time: " + finalTime.ToString("F2"));

        SaveBestTime(finalTime);
        
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.AddScore(finalTime);
        }

        if (finalTimeText != null)
        {
            finalTimeText.text = "Final Time: " + finalTime.ToString("F2") + "s";
        }

        if (raceFinishedPanel != null)
        {
            raceFinishedPanel.SetActive(true);
        }

        UpdateBestTimeUI();
    }

    public float GetFinalTime()
    {
        return currentTime + penaltyTime;
    }

    void LoadBestTime()
    {
        if (PlayerPrefs.HasKey(BestTimeKey))
        {
            bestTime = PlayerPrefs.GetFloat(BestTimeKey);
        }
        else
        {
            bestTime = 0f;
        }
    }

    void SaveBestTime(float finalTime)
    {
        if (bestTime == 0f || finalTime < bestTime)
        {
            bestTime = finalTime;
            PlayerPrefs.SetFloat(BestTimeKey, bestTime);
            PlayerPrefs.Save();

            Debug.Log("New best time saved: " + bestTime.ToString("F2"));
        }
        else
        {
            Debug.Log("Best time not beaten");
        }
    }

    void UpdateTimerUI()
    {
        float shownTime = currentTime + penaltyTime;

        if (timerText != null)
        {
            timerText.text = shownTime.ToString("F2") + "s";
        }
    }

    void UpdateBestTimeUI()
    {
        if (bestTimeText == null)
        {
            return;
        }

        if (bestTime == 0f)
        {
            bestTimeText.text = "Best Time: --";
        }
        else
        {
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2") + "s";
        }
    }

    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey(BestTimeKey);
        bestTime = 0f;
        UpdateBestTimeUI();

        Debug.Log("Best time reset");
    }
}