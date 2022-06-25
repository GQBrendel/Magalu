using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfigScriptableObject : ScriptableObject
{
    public float DeathTriggerDelay;
}