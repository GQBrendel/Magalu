using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatsManager : MonoBehaviour
{
    public delegate void DeathHandler(Transform t);
    public DeathHandler OnChacterDeath;

    private DeathTrigger[] _deathTriggers;
    private Drone[] _drones;

    private void Awake()
    {
        _deathTriggers = FindObjectsOfType<DeathTrigger>(true);
        _drones = FindObjectsOfType<Drone>(true);
        SetUpEventListeners();
    }

    private void SetUpEventListeners()
    {
        foreach (var dt in _deathTriggers)
        {
            dt.OnChacterDeath += HandleCharacterDeath;
        }

        foreach (var dt in _drones)
        {
            dt.OnChacterDeath += HandleCharacterDeath;
        }
    }

    private void HandleCharacterDeath(Transform t)
    {
        OnChacterDeath?.Invoke(t);
    }

    private void OnDestroy()
    {
        RemoveEventListeners();
    }

    private void RemoveEventListeners()
    {
        foreach (var dt in _deathTriggers)
        {
            dt.OnChacterDeath -= HandleCharacterDeath;
        }

        foreach (var dt in _drones)
        {
            dt.OnChacterDeath -= HandleCharacterDeath;
        }
    }
}
