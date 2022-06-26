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

    internal void SetHeadSprite(Sprite sprite)
    {
        _headSprite.sprite = sprite;
    }

    internal void SetDorsoSprite(Sprite sprite)
    {
        _dorsoSprite.sprite = sprite;
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