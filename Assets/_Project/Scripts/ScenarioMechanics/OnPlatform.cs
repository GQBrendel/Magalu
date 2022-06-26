using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OnPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private GameObject _sound;
    private bool _shouldBeOn;

    public void TurnOnPlatform()
    {
        _shouldBeOn = true;
        LeanTween.alpha(this.gameObject, 1, GameConfig.Instance.TimeToTurnOn).setOnComplete(CompleteOn);
        GetComponent<ShadowCaster2D>().enabled = _shouldBeOn;
        _sound.SetActive(true);
    }
    public void TurnOffPlatform()
    {
        _shouldBeOn = false;
        _boxCollider.enabled = false;
        LeanTween.alpha(this.gameObject, 0, GameConfig.Instance.TimeToTurnOff);
        GetComponent<ShadowCaster2D>().enabled = _shouldBeOn; 
        _sound.SetActive(false);
    }

    private void CompleteOn()
    {
        _boxCollider.enabled = _shouldBeOn;
    }
}