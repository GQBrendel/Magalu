using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _characterOriginPosition;
    [SerializeField] private ThreatsManager _threatsManager;
    [SerializeField] private UpgradePod _upgradePod;

    [SerializeField] private GameObject _closeUpCamera;

    private Character _character;


    private void Awake()
    {
        _character = FindObjectOfType<Character>(true);
        _threatsManager.OnChacterDeath += ReturnCharacterToOrigin;

        _upgradePod.OnUpgradeStarted += HandleUpgradeStart;
    }

    private void HandleUpgradeStart()
    {
        _closeUpCamera.SetActive(true);
    }

    private void ReturnCharacterToOrigin()
    {
        StartCoroutine(WaitDelay());
        IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(GameConfig.Instance.DeathTriggerDelay);
            _character.transform.position = _characterOriginPosition.position;
        }
    }

    private void OnDestroy()
    {
        _threatsManager.OnChacterDeath -= ReturnCharacterToOrigin;
    }
}
