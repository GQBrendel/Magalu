using System.Collections;
using UnityEngine;

public class OnPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;

    private bool _shouldBeOn;

    public void TurnOnPlatform()
    {
        _shouldBeOn = true;
        LeanTween.alpha(this.gameObject, 1, GameConfig.Instance.TimeToTurnOn).setOnComplete(CompleteOn);
    }
    public void TurnOffPlatform()
    {
        _shouldBeOn = false;
        _boxCollider.enabled = false;
        LeanTween.alpha(this.gameObject, 0, GameConfig.Instance.TimeToTurnOff);
    }

    private void CompleteOn()
    {
        _boxCollider.enabled = _shouldBeOn;
    }
}