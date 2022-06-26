using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerEscuro : MonoBehaviour
{
    [SerializeField] Light2D objActive;
    [SerializeField] Light2D objDeactive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            objActive.enabled = true;
            objDeactive.enabled = false;
        }
    }

}
