using System.Collections;
using UnityEngine;

public class OnPlatform : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    //private SpriteRenderer _spriteRenderer;

    private Coroutine _turnOnRoutine;
    private bool _shouldBeOn;

    private void Awake()
    {
        TryGetComponent(out _boxCollider);
        //TryGetComponent(out _spriteRenderer);
    }

    public void TurnOnPlatform()
    {
        _shouldBeOn = true;
        LeanTween.alpha(this.gameObject, 1, GameConfig.Instance.TimeToTurnOn).setOnComplete(CompleteOn);
        //_turnOnRoutine = StartCoroutine(TurnOnRoutine());
    }
    public void TurnOffPlatform()
    {
        _shouldBeOn = false;
       // StopCoroutine(_turnOnRoutine);
        _boxCollider.enabled = false;
        LeanTween.alpha(this.gameObject, 0, GameConfig.Instance.TimeToTurnOff);
    }

    private void CompleteOn()
    {
        _boxCollider.enabled = _shouldBeOn;
    }

    private IEnumerator TurnOnRoutine()
    {
        yield return new WaitForSeconds(GameConfig.Instance.TimeToTurnOn);
        _boxCollider.enabled = true;
    }
}