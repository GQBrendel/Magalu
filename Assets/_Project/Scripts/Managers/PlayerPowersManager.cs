using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowersManager : MonoBehaviour
{
    [SerializeField] private int _currentHead;
    [SerializeField] private Sprite[] _playerHeadSprites;

    private OnPlatform[] _onPlatforms;

    private Character _character;

    private void Awake()
    {
        _character = FindObjectOfType<Character>(true); 
        _onPlatforms = FindObjectsOfType<OnPlatform>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("SwapHead"))
        {
            SwapHead();
        }
    }

    private void SwapHead()
    {
        _currentHead++;

        if(_currentHead >= _playerHeadSprites.Length)
        {
            _currentHead = 0;
        }
        
        if(_currentHead == 0)
        {
            TurnOffPlatforms();
        }
        else if (_currentHead == 1) //Ligadora de plataformas
        {
            TurnOnPlatforms();
        }
        else if(_currentHead == 2)
        {
            TurnOffPlatforms();
        }

        _character.SetHeadSprite(_playerHeadSprites[_currentHead]);
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