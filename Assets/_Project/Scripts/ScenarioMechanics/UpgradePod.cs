using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePod : MonoBehaviour
{
    public delegate void UpgradeHandler();
    public UpgradeHandler OnUpgradeStarted;

    [SerializeField] private GameObject[] _lights;
    [SerializeField] private Transform _playerMoveToPosition;

    public bool _isOn;

    private void Awake()
    {
        foreach(var light in _lights)
        {
            LeanTween.alpha(light, 0, 0);
        }
    }

    public void TurnOn()
    {
        foreach (var light in _lights)
        {
            LeanTween.alpha(light, 1, 1).setOnComplete(PodIsOn);
        }
    }

    public void TurnOff()
    {

    }

    private void PodIsOn()
    {
        _isOn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOn)
        {
            return;
        }

        Character character = collision.GetComponent<Character>();
        if (character != null)
        {
            OnUpgradeStarted?.Invoke();
            character.DisableControl();

            LeanTween.move(character.gameObject, _playerMoveToPosition.position, 1f);
        }
    }
}