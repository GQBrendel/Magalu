using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSecret : MonoBehaviour
{
    [SerializeField] GameObject conjBlocks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            conjBlocks.SetActive(false);
        }
    }
}
