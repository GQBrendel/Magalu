using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _headSprite;
    [SerializeField] private SpriteRenderer _dorsoSprite;
    [SerializeField] private CharacterController2D _characterController;

    private void Awake()
    {
        TryGetComponent(out _characterController);
    }

    internal void SetHeadSprite(PowerUp power)
    {
        _headSprite.sprite = power.Sprite.sprite;
    }

    internal void SetDorsoSprite(PowerUp power)
    {
        _dorsoSprite.sprite = power.Sprite.sprite;
    }

    internal void DisableControl()
    {
        _characterController.CanMove = false;
    }
    internal void EnableControl()
    {
        _characterController.CanMove = true;
    }
}