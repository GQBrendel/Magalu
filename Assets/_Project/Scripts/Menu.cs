using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject creditos;

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void Creditos()
    {
        menu.SetActive(false);
        creditos.SetActive(true);
    }

    public void VoltarMenu()
    {
        menu.SetActive(true);
        creditos.SetActive(false);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
