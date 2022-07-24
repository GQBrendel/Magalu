using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public enum LightTriggerType
{
    TurnLightsOn,
    TurnLightsOff
}

public class TriggerEscuro : MonoBehaviour
{
    [SerializeField] Light2D _globalLight;

    [SerializeField] private LightTriggerType _triggerType;

    [SerializeField] private Color _onColor;
    [SerializeField] private Color _offColor;

    [SerializeField] private bool _blockFirstTime;

    private IEnumerator ChangeColorOn()
    {
        float tick = 0f;
        while (_globalLight.color != _onColor)
        {
            tick += Time.deltaTime * GameConfig.Instance.BlendingLightTime;
            _globalLight.color = Color.Lerp(_offColor, _onColor, tick);
            yield return null;
        }
    }
    private IEnumerator ChangeColorOff()
    {
        float tick = 0f;
        while (_globalLight.color != _offColor)
        {
            tick += Time.deltaTime * GameConfig.Instance.BlendingLightTime;
            _globalLight.color = Color.Lerp(_onColor, _offColor, tick);
            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        if (_blockFirstTime)
        {
            _blockFirstTime = false;
            return;
        }

        switch (_triggerType)
        {
            case LightTriggerType.TurnLightsOn:
                StartCoroutine(ChangeColorOn());
                break;
            case LightTriggerType.TurnLightsOff:
                StartCoroutine(ChangeColorOff());
                break;
        }
    }
}