using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour {
    [Header("Variáveis de Game Desing")]
    [Tooltip("Vida do personagem")]
    [SerializeField] float hp;

    /// <summary>
    /// Ao atribuir hp, verifica se ele é menor ou igual a 0, o que indica que o personagem morreu.
    /// Se não for o player, o personagem se destroi. Se for, carrega cena de Game Over.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            if (this.gameObject.CompareTag("Player")) {
                SceneManager.LoadScene("GameOver");
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}