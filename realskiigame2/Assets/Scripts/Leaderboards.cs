using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    [SerializeField] private List<float> bestTimes = new();
    [SerializeField] private List<TextMeshProUGUI> timeTexts = new();

    private void Awake()
    {
        LoadTimes();
        UpdateUI();
    }

    public void AddRaceTime(float time)
    {
        bestTimes.Add(time);
        bestTimes.Sort();
        SaveTimes();
        UpdateUI();
    }

    private void SaveTimes()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < bestTimes.Count)
                PlayerPrefs.SetFloat("time" + i, bestTimes[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadTimes()
    {
        bestTimes = new List<float>();
        for (int i = 0; i < 5; i++)
        {
            bestTimes.Add(PlayerPrefs.GetFloat("time" + i, 99999));
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < timeTexts.Count; i++)
        {
            if (i < bestTimes.Count && bestTimes[i] < 99999)
            {
                int minutes = Mathf.FloorToInt(bestTimes[i] / 60);
                int seconds = Mathf.FloorToInt(bestTimes[i] % 60);
                timeTexts[i].text = $"{i + 1}. {minutes:00}:{seconds:00}";
            }
            else
            {
                timeTexts[i].text = $"{i + 1}. --:--";
            }
        }
    }
}
