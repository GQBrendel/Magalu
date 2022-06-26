using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfigScriptableObject : ScriptableObject
{
    public float DeathTriggerDelay;

    [Header("Light Up Platform")]
    public float TimeToTurnOn;
    public float TimeToTurnOff;

    [Header("Upgrade Pod")]
    public float PodDelay;
    public float IconMovementTime;
    public float IconFadeTime;

    [Header("Energy Power")]
    public float HeadCost;
    public float DorsoCost;
    public float WheelCost;

    public float RefillRate = 2f;
    public float OverHeatDelay;

}