using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartEnum
{
    Head,
    Body,
    Feet
}

public class PowerUp : MonoBehaviour
{
    public delegate void PowerUpCollecteHandler(PowerUp powerUp);
    public PowerUpCollecteHandler OnPowerUpColled;

    [SerializeField] private BodyPartEnum _bodyPart;
    [SerializeField] private int _partIndex;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public int PartIndex
    {
        get { return _partIndex; }
        set { _partIndex = value; }
    }
    public SpriteRenderer Sprite
    {
        get { return _spriteRenderer; }
        set { _spriteRenderer = value; }
    }

    public BodyPartEnum BodyPart
    {
        get { return _bodyPart; }
        set { _bodyPart = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPowerUpColled?.Invoke(this);
    }
}