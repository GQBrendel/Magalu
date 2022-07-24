using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _characterOriginPosition;
    [SerializeField] private ThreatsManager _threatsManager;

    [SerializeField] private UIManager _uIManager;
    [SerializeField] private GameObject _closeUpCamera;
    [SerializeField] private PlayerPowersManager _playerPowerManager;


    private UpgradePod[] _upgradePods;
    private Character _character;


    private void Awake()
    {
        _character = FindObjectOfType<Character>(true);
        _threatsManager.OnChacterDeath += ReturnCharacterToOrigin;
        _upgradePods = FindObjectsOfType<UpgradePod>(true);

        _uIManager.OnUpgradeFinished += HandleUpgradeFinished;

        foreach (var pod in _upgradePods)
        {
            pod.OnUpgradeStarted += HandleUpgradeStart;
        }
    }

    private void DIsablePodLight()
    {
        foreach (var pod in _upgradePods)
        {
            pod.TurnOff();
        }

    }

    private void HandleUpgradeFinished()
    {
        _playerPowerManager.EquipPowerByMachine();
        _playerPowerManager.IsOnUpgradeProcess = false;
        _character.EnableControl(); 
        DIsablePodLight();
        _closeUpCamera.SetActive(false);
    }

    private void HandleUpgradeStart()
    {
        _playerPowerManager.IsOnUpgradeProcess = true;
        _closeUpCamera.SetActive(true);
        StartCoroutine(WaitForPodZoomRoutine());

        IEnumerator WaitForPodZoomRoutine()
        {
            yield return new WaitForSeconds(GameConfig.Instance.PodDelay);
            _uIManager.MoveUpgradeIcon();
        }
    }

    private void ReturnCharacterToOrigin(Transform t)
    {
        StartCoroutine(WaitDelay());
        IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(GameConfig.Instance.DeathTriggerDelay);
            _character.transform.position = t.position;
        }
    }

    private void OnDestroy()
    {
        _threatsManager.OnChacterDeath -= ReturnCharacterToOrigin;
    }
}
