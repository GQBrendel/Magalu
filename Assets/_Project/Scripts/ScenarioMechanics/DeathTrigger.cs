using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathTrigger : MonoBehaviour
{
    public delegate void DeathHandler();
    public DeathHandler OnChacterDeath;

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        TryGetComponent(out _boxCollider2D);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character;
        bool isCharacer = collision.TryGetComponent(out character);
        if (isCharacer)
        {
            OnChacterDeath?.Invoke();
        }
    }
}
