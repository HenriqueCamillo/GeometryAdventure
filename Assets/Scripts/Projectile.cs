using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    [Header("Variáveis de Game Design")]
    [Tooltip("Velocidade com que o projétil é disparado")]
    [Range(0f, 10f)] public float speed;
    [Tooltip("Velocidade da rotação do projétil")]
    [SerializeField] float rotationSpeed;
    [Tooltip("Dano causado pelo projétil ao atingir algum inimigo")]
    [SerializeField] float damage;

    /// <summary>
    /// Pega a refência do componente do RigidBody2D e atribui uma velocidade horizontal ao projétil
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0f);
    }

    /// <summary>
    /// Faz o projétil permanecer rodando
    /// </summary>
    void FixedUpdate() {
        this.transform.Rotate(0f, 0f, rotationSpeed);
    }

    /// <summary>
    /// Checa colisões com outros objetos.
    /// O projétil sempre se destroi ao colidir com algo.
    /// Ao colidir com inimigos, causa dano neles.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.GetComponent<Life>().TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
