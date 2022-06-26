using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowersManager : MonoBehaviour
{
    [SerializeField] private int _currentHead;

    private OnPlatform[] _onPlatforms;

    private Character _character;

    private void Awake()
    {
        _character = FindObjectOfType<Character>(true); 
        _onPlatforms = FindObjectsOfType<OnPlatform>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            TurnOnPlatforms();
        }
        else if (Input.GetKey(KeyCode.Y))
        {
            TurnOffPlatforms();
        }
    }

    private void TurnOnPlatforms()
    {
        foreach(var plat in _onPlatforms)
        {
            plat.TurnOnPlatform();
        }
    }
    private void TurnOffPlatforms()
    {
        foreach (var plat in _onPlatforms)
        {
            plat.TurnOffPlatform();
        }
    }
}