using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePod : MonoBehaviour
{
    public delegate void UpgradeHandler();
    public UpgradeHandler OnUpgradeStarted;

    [SerializeField] private GameObject[] _lights;
    [SerializeField] private Transform _playerMoveToPosition;
    [SerializeField] private GameObject _effectLight;
    private BoxCollider2D _boxCollider;

    public bool _isOn;

    private void Awake()
    {
        TryGetComponent(out _boxCollider);
        foreach(var light in _lights)
        {
            LeanTween.alpha(light, 0, 0);
        }
        _boxCollider.enabled = false;
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
        _isOn = false; 
        
        foreach (var light in _lights)
        {
            LeanTween.alpha(light, 0, 1);
        }
        _effectLight.SetActive(false); 
        _boxCollider.enabled = false;
    }

    private void PodIsOn()
    {
        _isOn = true;
        _boxCollider.enabled = true;
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
            TurnOnEffect();
        }
    }

    private void TurnOnEffect()
    {
        StartCoroutine(EffectWait());
        IEnumerator EffectWait()
        {
            yield return new WaitForSeconds(2f);
            _effectLight.SetActive(true);
        }
    }
}