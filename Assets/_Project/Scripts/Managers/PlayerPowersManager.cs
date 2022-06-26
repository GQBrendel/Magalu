using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowersManager : MonoBehaviour
{
    [SerializeField] private int _currentHead;
    [SerializeField] private Sprite[] _playerHeadSprites;
    [SerializeField] private int _currentDorso;
    [SerializeField] private Sprite[] _playerDorsoSprites;
    [SerializeField] private UIManager _uIManager;

    [Header("Player References")]
    [SerializeField] private GameObject _tunderaLight;
    [SerializeField] private GameObject _dorsoLight;

    public bool CanSwap { get; set; }

    private PowerUp[] _powerUps;

    private OnPlatform[] _onPlatforms;

    private Character _character;

    private UpgradePod[] _upgradePods;

    private PowerUp _currentPowerUp;

    private EnergyBar _energyBar;


    public bool _holdingAPowerUp;

    private bool _headAxisInUse = false;
    private bool _dorsoAxisInUse = false;

    private void Awake()
    {
        _character = FindObjectOfType<Character>(true); 
        _onPlatforms = FindObjectsOfType<OnPlatform>(true);
        _powerUps = FindObjectsOfType<PowerUp>(true);
        _upgradePods = FindObjectsOfType<UpgradePod>(true);
        _energyBar = FindObjectOfType<EnergyBar>(true);

        SetPowerUpListeners();
        CanSwap = true;
    }
    private void Update()
    {
        if (!CanSwap)
        {
            return;
        }

        //float swapHead = Input.GetAxisRaw("SwapHead");
        //float swapWheel = Input.GetAxisRaw("SwapWheel");


        /*if (swapHead != 0)
        {
            if (_headAxisInUse == false)
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
                _headAxisInUse = true;
            }
        }
        else if (swapWheel != 0)
        {
            if (_dorsoAxisInUse == false)
            {
                if (swapWheel > 0)
                {
                    SwapWheels();
                }
                // Call your event function here.
                _dorsoAxisInUse = true;
            }

        }*/
        /*if (swapHead == 0)
        {
            _headAxisInUse = false;
        }*/
        /*if (swapWheel == 0)
        {
            _dorsoAxisInUse = false;
        }*/

        if (Input.GetButtonDown("SwapHead"))
        {
            SwapHead();
        }
        else if (Input.GetButtonDown("SwapWheel"))
        {
            SwapWheels();
        }
        
        if (Input.GetButtonDown("UsePowerChest"))
        {
            StartPowerDorso();
        }
        else if (Input.GetButton("UsePowerChest"))
        {
            KeepPowerDorso();
        }
        else if (Input.GetButtonUp("UsePowerChest"))
        {
            StopPowerDorso();
        }
        if (Input.GetButtonDown("UsePowerHead"))
        {
            StartPowerHead();
        }
        else if (Input.GetButton("UsePowerHead"))
        {
            KeepPowerHead();
        }
        else if (Input.GetButtonUp("UsePowerHead"))
        {
            StopPowerHead();
        }
        if (Input.GetButton("UsePowerWheel"))
        {
            StartPowerWheel();
        }
        else if (Input.GetButtonUp("UsePowerWheel"))
        {
            StopPowerWheel();
        }
    }

    private void StartPowerHead()
    {
        if (_currentHead == 0)
        {
            return;
        }
        if (!HasEnergy())
        {
            StopPowerHead();
            return;
        }
        else if (_currentHead == 1) //Ligadora de plataformas
        {
            _energyBar.WasteEnergy(GameConfig.Instance.HeadCost);
            TurnOnPlatforms(); 
        }
        else if (_currentHead == 2)
        {
            TurnOffPlatforms();
        }
    }

    private void KeepPowerHead()
    {        
        if (!HasEnergy())
        {
            return;
        }
        if (_currentHead == 0)
        {
            return;
        }
        _energyBar.WasteEnergy(GameConfig.Instance.HeadCost);
    }
    private void KeepPowerDorso()
    {
        if (!HasEnergy())
        {
            return;
        }
        if (_currentDorso == 0)
        {
            return;
        }
        _energyBar.WasteEnergy(GameConfig.Instance.DorsoCost);
    }
    private void StopPowerHead()
    {

        TurnOffPlatforms();
    }
    private void StartPowerWheel()
    {

    }
    private void StopPowerWheel()
    {

    }

    private bool HasEnergy()
    {
        if (_energyBar.CurrentEnergy <= 0.2f)
        {
            _energyBar.Overheat();
            return false;
        }
        return true;

    }

    private void StartPowerDorso()
    {
        if (!HasEnergy())
        {
            StopPowerDorso();
            return;
        }
        EquipDorso(1);
        _energyBar.WasteEnergy(GameConfig.Instance.DorsoCost);
        _dorsoLight.SetActive(true);
    }
    private void StopPowerDorso()
    {
        _dorsoLight.SetActive(false);
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
        StopPowerDorso();
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
        StopPowerHead();

        /*if (_currentHead == 0)
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
        }*/
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
    private void EquipWheels()
    {
        StopPowerWheel();
    }

    private void TurnOnPlatforms()
    {
        _tunderaLight.gameObject.SetActive(true);
        foreach (var plat in _onPlatforms)
        {
            plat.TurnOnPlatform();
        }
    }
    private void TurnOffPlatforms()
    {
        _tunderaLight.gameObject.SetActive(false);
        foreach (var plat in _onPlatforms)
        {
            plat.TurnOffPlatform();
        }
    }
}