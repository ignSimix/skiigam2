using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRace : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameEvents.CallRaceFinish();
    }
}
