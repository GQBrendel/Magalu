using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowersManager : MonoBehaviour
{
    [SerializeField] private int _currentHead;
    [SerializeField] private Sprite[] _playerHeadSprites;
    [SerializeField] private int _currentDorso;
    [SerializeField] private Sprite[] _playerDorsoSprites;
    [SerializeField] private GameObject _dorsoLight;
    [SerializeField] private UIManager _uIManager;

    public bool CanSwap { get; set; }

    private PowerUp[] _powerUps;

    private OnPlatform[] _onPlatforms;

    private Character _character;

    private UpgradePod[] _upgradePods;

    private PowerUp _currentPowerUp;

    public bool _holdingAPowerUp;

    private bool m_isAxisInUse = false;

    private void Awake()
    {
        _character = FindObjectOfType<Character>(true); 
        _onPlatforms = FindObjectsOfType<OnPlatform>(true);
        _powerUps = FindObjectsOfType<PowerUp>(true);
        _upgradePods = FindObjectsOfType<UpgradePod>(true);
        SetPowerUpListeners();
        CanSwap = true;
    }
    private void Update()
    {
        if (!CanSwap)
        {
            return;
        }

        float swapHead = Input.GetAxisRaw("SwapHead");
        //Debug.Log("Swap Head " + swapHead);

        if (swapHead != 0)
        {
            if (m_isAxisInUse == false)
            {
                if(swapHead > 0)
                {
                    SwapHead();
                }
                else if (swapHead < 0)
                {
                    SwapWheels();
                }
                // Call your event function here.
                m_isAxisInUse = true;
            }
        }
        if (swapHead == 0)
        {
            m_isAxisInUse = false;
        }

        if (Input.GetButtonDown("SwapHead"))
        {
            SwapHead();
        }

        else if (Input.GetButtonDown("Fire3"))
        {
            SwapDorso();
        }
    }
    private void SetPowerUpListeners()
    {
        foreach (var pw in _powerUps)
        {
            pw.OnPowerUpColled += HandlePowerUpCollected;
        }
    }


    public void EquipPower()
    {
        CanSwap = true;
        _holdingAPowerUp = false;
        switch (_currentPowerUp.BodyPart)
        {
            case BodyPartEnum.Head:

                EquipHead(_currentPowerUp.PartIndex);

                break;
            case BodyPartEnum.Body:
                EquipDorso(_currentPowerUp.PartIndex);
                break;
            case BodyPartEnum.Feet:
                break;
        }
    }

    private void EquipDorso(int index)
    {
        _currentDorso = index;        
        _character.SetDorsoSprite(_playerDorsoSprites[_currentDorso]);

        if (_currentDorso == 0)
        {
            _dorsoLight.SetActive(false);
        }
        else if (_currentDorso == 1)
        {
            _dorsoLight.SetActive(true);
        }
        else if (_currentDorso == 2)
        {
            _dorsoLight.SetActive(false);
        }
    }

    private void EquipHead(int index)
    {
        _currentHead = index;
        _character.SetHeadSprite(_playerHeadSprites[_currentHead]);

        if (_currentHead == 0)
        {
            TurnOffPlatforms();
        }
        else if (_currentHead == 1) //Ligadora de plataformas
        {
            TurnOnPlatforms();
        }
        else if (_currentHead == 2)
        {
            TurnOffPlatforms();
        }
    }

    private void HandlePowerUpCollected(PowerUp pw)
    {
        if (_holdingAPowerUp)
        {
            return;
        }

        _currentPowerUp = pw;
        _holdingAPowerUp = true;
        pw.gameObject.SetActive(false);
        _uIManager.ShowItemIcon(pw.Sprite.sprite);

        foreach (var pod in _upgradePods)
        {
            pod.TurnOn();
        }

       
    }


    private void SwapDorso()
    {
        _currentDorso++;

        if (_currentDorso >= _playerDorsoSprites.Length)
        {
            _currentDorso = 0;
        }

        EquipDorso(_currentDorso);
    }

    private void SwapHead()
    {
        _currentHead++;

        if(_currentHead >= _playerHeadSprites.Length)
        {
            _currentHead = 0;
        }

        EquipHead(_currentHead);
    }

    private void SwapWheels()
    {

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