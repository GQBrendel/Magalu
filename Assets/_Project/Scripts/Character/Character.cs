using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _headSprite;

    internal void SetHeadSprite(Sprite sprite)
    {
        _headSprite.sprite = sprite;
    }
}