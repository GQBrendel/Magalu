using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Slider _slider;

    private const float _divisionConstant = 1000f;

    public float CurrentEnergy { get; set; }

    public bool _onOverheat;

    private void Awake()
    {
        TryGetComponent(out _slider);
    }

    public void WasteEnergy(float energyCost)
    {
        _slider.value -= energyCost / _divisionConstant;
    }

    private void FixedUpdate()
    {
        RefillEnergy();
        CurrentEnergy = _slider.value * 100f;
    }

    public void RefillEnergy()
    {
        if (_onOverheat)
        {
            return;
        }
        _slider.value += GameConfig.Instance.RefillRate / _divisionConstant;
    }

    internal void Overheat()
    {
        if (_onOverheat)
        {
            return;
        }

        _slider.value = 0;
        _onOverheat = true;
        StartCoroutine(ReturnFromOverheat());
    }

    private IEnumerator ReturnFromOverheat()
    {
        yield return new WaitForSeconds(GameConfig.Instance.OverHeatDelay);
        _onOverheat = false;
    }
}
