using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatsManager : MonoBehaviour
{
    public delegate void DeathHandler();
    public DeathHandler OnChacterDeath;

    private DeathTrigger[] _deathTriggers;

    private void Awake()
    {
        _deathTriggers = FindObjectsOfType<DeathTrigger>(true);
        SetUpEventListeners();
    }

    private void SetUpEventListeners()
    {
        foreach (var dt in _deathTriggers)
        {
            dt.OnChacterDeath += HandleCharacterDeath;
        }
    }

    private void HandleCharacterDeath()
    {
        OnChacterDeath?.Invoke();
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
    }
}
