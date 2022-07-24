using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDebug : MonoBehaviour
{
    [SerializeField] private GameObject _DebugUI;

    [SerializeField] private PowerUp _readSigns;
    [SerializeField] private PowerUp _lightPlatforms;
    [SerializeField] private PowerUp _dorsoLight;

    private PlayerPowersManager _playerPowersManager;

    private void Awake()
    {
        if (Application.isEditor)
        {
            _DebugUI.SetActive(Application.isEditor);
            _playerPowersManager = FindObjectOfType<PlayerPowersManager>();
            StartCoroutine(DebugRoutine());
        }
    }

    private IEnumerator DebugRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _playerPowersManager.EquipHead(_readSigns);
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                _playerPowersManager.EquipHead(_lightPlatforms);
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                _playerPowersManager.EquipDorso(_dorsoLight);
            }

            yield return null;
        }
    }
}