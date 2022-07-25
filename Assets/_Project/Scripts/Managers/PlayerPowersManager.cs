using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowersManager : MonoBehaviour
{
    [SerializeField] private PowerType _currentHeadPower;
    [SerializeField] private PowerType _currentDorsoPower;

    [SerializeField] private List<PowerUp> _playerDorso;
    [SerializeField] private List<PowerUp> _playerHeads;

    [SerializeField] private UIManager _uIManager;

    [Header("Player References")]
    [SerializeField] private GameObject _tunderaLight;
    [SerializeField] private GameObject _dorsoLight;

    public bool IsOnUpgradeProcess { get; set; }
    public bool InfinyEnergyHack { get; set; }
    public bool canRead;

    private PowerUp[] _powerUps;
    private OnPlatform[] _onPlatforms;
    private Character _character;
    private UpgradePod[] _upgradePods;
    private PowerUp _currentPowerUp;
    private EnergyBar _energyBar;

    public bool _holdingAPowerUp;

    private int _headCount = 0;
    private int _dorsoCount = 0;

    private void Awake()
    {
        _character = FindObjectOfType<Character>(true); 
        _onPlatforms = FindObjectsOfType<OnPlatform>(true);
        _powerUps = FindObjectsOfType<PowerUp>(true);
        _upgradePods = FindObjectsOfType<UpgradePod>(true);
        _energyBar = FindObjectOfType<EnergyBar>(true);

        SetPowerUpListeners();
      //  CanSwap = true;
    }
    private void Update()
    {
        if (IsOnUpgradeProcess)
        {
            return;
        }

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
        if (!HasEnergy())
        {
            StopPowerHead();
            return;
        }

        switch (_currentHeadPower)
        {
            case PowerType.ReadSignsHead:
                canRead = true;
                TurnOffPlatforms();
                break;
            case PowerType.LightPlatformsHead:
                _energyBar.WasteEnergy(GameConfig.Instance.HeadCost);
                TurnOnPlatforms();
                break;      
        }
    }

    private void KeepPowerHead()
    {        
        if (!HasEnergy())
        {
            StopPowerHead();
            return;
        }
        if (_currentHeadPower == PowerType.None)
        {
            return;
        }
        _energyBar.WasteEnergy(GameConfig.Instance.HeadCost);
    }
    private void KeepPowerDorso()
    {
        if (!HasEnergy())
        {
            StopPowerDorso();
            return;
        }
        if (_currentDorsoPower == PowerType.None)
        {
            return;
        }
        _energyBar.WasteEnergy(GameConfig.Instance.DorsoCost);
    }
    private void StopPowerHead()
    {
        TurnOffPlatforms();

        if(_currentHeadPower == PowerType.ReadSignsHead)
        {
            canRead = false;
        }
    }
    private void StartPowerWheel()
    {

    }
    private void StopPowerWheel()
    {

    }

    private bool HasEnergy()
    {
        if (InfinyEnergyHack)
        {
            return true;
        }

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

        switch (_currentDorsoPower)
        {
            case PowerType.LightDorso:
                _dorsoLight.SetActive(true);
                break;
        }

        _energyBar.WasteEnergy(GameConfig.Instance.DorsoCost);
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

    public void EquipPowerByMachine()
    {
        if (!_currentPowerUp)
        {
            Debug.LogError($"Trying to equip a power but the player does not have a upgrade");
        }

        _holdingAPowerUp = false;
        switch (_currentPowerUp.BodyPart)
        {
            case BodyPartEnum.Head:
                EquipHead(_currentPowerUp);
                break;
            case BodyPartEnum.Body:
                EquipDorso(_currentPowerUp);
                break;
            case BodyPartEnum.Feet:
                break;
        }
    }

    public void EquipDorso(PowerUp power)
    {
        _currentDorsoPower = power.PowerType;

        if (!_playerDorso.Contains(power))
        {
            if (power.BodyPart == BodyPartEnum.Head)
                _playerDorso.Add(power);
        }

        _character.SetDorsoSprite(power);

        StopPowerDorso();
    }

    public void EquipHead(PowerUp power)
    {
        _currentHeadPower = power.PowerType;

        if (!_playerHeads.Contains(power))
        {
            if(power.BodyPart == BodyPartEnum.Head)
                _playerHeads.Add(power);
        }

        _character.SetHeadSprite(power);

        StopPowerHead();
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
        if (_playerHeads.Count <= 1)
        {
            return;
        }

        _headCount++;

        if(_headCount >= _playerHeads.Count)
        {
            _headCount = 0;
        }

        EquipHead(_playerHeads[_headCount]);
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
            float distance = Vector2.Distance(_character.transform.position, plat.transform.position);

            if(distance < 50f)
            {
                plat.TurnOnPlatform();
            }
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