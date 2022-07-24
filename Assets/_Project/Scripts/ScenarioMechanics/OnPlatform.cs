using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OnPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private GameObject _sound;
    private SpriteRenderer _spriteRenderer;
    private bool _shouldBeOn;


    private void Awake()
    {
        TryGetComponent(out _spriteRenderer);
    }

    public void TurnOnPlatform()
    {
        gameObject.SetActive(true);
        _shouldBeOn = true;
        LeanTween.alpha(this.gameObject, 1, GameConfig.Instance.TimeToTurnOn).setOnComplete(CompleteOn);
        GetComponent<ShadowCaster2D>().enabled = _shouldBeOn;
        _sound.SetActive(true);
    }
    public void TurnOffPlatform()
    {
        _shouldBeOn = false;
        _boxCollider.enabled = false;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);
//        LeanTween.alpha(this.gameObject, 0, GameConfig.Instance.TimeToTurnOff);
        GetComponent<ShadowCaster2D>().enabled = _shouldBeOn;
        gameObject.SetActive(false);
        _sound.SetActive(false);
    }

    private void CompleteOn()
    {
        _boxCollider.enabled = _shouldBeOn;
    }
}