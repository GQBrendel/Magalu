using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEscuro : MonoBehaviour
{
    [SerializeField] GameObject objActive;
    [SerializeField] GameObject objDeactive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            objActive.SetActive(true);
            objDeactive.SetActive(false);
        }
    }

}
