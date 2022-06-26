using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowersManager : MonoBehaviour
{
    [SerializeField] private int _currentHead;
    [SerializeField] private Sprite[] _playerHeadSprites;
    [SerializeField] private UIManager _uIManager;

    private PowerUp[] _powerUps;

    private OnPlatform[] _onPlatforms;

    private Character _character;

    private UpgradePod[] _upgradePods;

    public bool _holdingAPowerUp;

    private void Awake()
    {
        _character = FindObjectOfType<Character>(true); 
        _onPlatforms = FindObjectsOfType<OnPlatform>();
        _powerUps = FindObjectsOfType<PowerUp>();
        _upgradePods = FindObjectsOfType<UpgradePod>();
        SetPowerUpListeners();
    }
    private void SetPowerUpListeners()
    {
        foreach (var pw in _powerUps)
        {
            pw.OnPowerUpColled += HandlePowerUpCollected;
        }
    }

    private void HandlePowerUpCollected(PowerUp pw)
    {
        if (_holdingAPowerUp)
        {
            return;
        }

        _holdingAPowerUp = true;
        pw.gameObject.SetActive(false);
        _uIManager.ShowItemIcon(pw.Sprite.sprite);

        foreach (var pod in _upgradePods)
        {
            pod.TurnOn();
        }

        switch (pw.BodyPart)
        {
            case BodyPartEnum.Head:
                break;
            case BodyPartEnum.Body:
                break;
            case BodyPartEnum.Feet:
                break;
        }
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