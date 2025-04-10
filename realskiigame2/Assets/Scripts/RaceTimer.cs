using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] private float penaltyTime = 1;
    [SerializeField]  private Leaderboard leaderboard;

    private bool timerRunning =false;
    private float raceTime = 0;

    private void Update()
    {
        if (timerRunning)
            raceTime += Time.deltaTime;
    }
    private void OnEnable()
    {
        GameEvents.raceStart += StartRace;
        GameEvents.raceEnd += FinishRace;
        GameEvents.racePenalty += Penalty;
    }
    private void OnDisable()
    {
        GameEvents.raceStart -= StartRace;
        GameEvents.raceEnd -= FinishRace;
        GameEvents.racePenalty -= Penalty;
    }

    private void Penalty()
    {
        raceTime += penaltyTime;
        Debug.Log("penalty recieved!");
    }
    private void StartRace()
    {
        raceTime = 0;
        timerRunning = true;
        Debug.Log("race started!");
    }
    private void FinishRace()
    {
        timerRunning = false;
        leaderboard.AddTime(raceTime);
        GameData.Instance.racesCompleted++;
        Debug.Log("race finished!" + GameData.Instance.racesCompleted);
        Debug.Log("race time: " + raceTime);
    }
}