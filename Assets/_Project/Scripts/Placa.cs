using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placa : MonoBehaviour
{
    [SerializeField] PlayerPowersManager manager;
    [SerializeField] GameObject mensagem;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (manager.canRead)
            {
                mensagem.SetActive(true);
            }
            else
            {
                mensagem.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (manager.canRead)
            {
                mensagem.SetActive(false);
            }
        }
    }
}
