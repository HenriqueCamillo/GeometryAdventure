using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    /// <summary>
    /// Carrega a cena do primeiro nível do jogo.
    /// </summary>
    public void Play() {
        SceneManager.LoadScene("Level-1");
    }

    /// <summary>
    /// Carrega a cena de menu. 
    /// </summary>
    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }


    /// <summary>
    /// Sai do jogo.
    /// </summary>
    public void Quit() {
        Application.Quit();
    }
}