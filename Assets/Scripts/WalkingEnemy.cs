using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    [Space(5)]
    [Header("Variáveis de Game Design")]
    [Tooltip("Indica a direção para qual o inimgo está andando")]
    [SerializeField] protected bool walkingToTheLeft;
    [Tooltip("Velocidade do inimigo")]
    [SerializeField] private float speed;
    [Tooltip("Dano causado pelo inimigo ao atingir o player")]
    [SerializeField] float damage;
    [Tooltip("Distância do raio que checa se há uma parede na frente do inimigo")]
    [SerializeField] private float rayDistance;

    /// <summary>
    /// Pega referencia do SpriteRenderer e define a direção de sua velocidade.
    /// </summary>
    private void Start() {
        sRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        speed = walkingToTheLeft ? -speed : speed;
        sRenderer.flipX = walkingToTheLeft;
    }

    /// <summary>
    /// Coloca o inimigo em movimento e checa se há uma parede em sua frente, mudando de direção se houver.
    /// </summary>
    protected void Update() {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        Vector2 direction = walkingToTheLeft ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, rayDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay((Vector2)this.transform.position, direction * rayDistance, Color.green, 0.1f);
        
        if (hit) {
            ChangeDirection();
        }
    }

    /// <summary>
    /// Muda a direção do movimento do inimigo, mudando sua velocida, sprite e flag.
    /// </summary>
    protected void ChangeDirection() {
        speed = -speed;
        sRenderer.flipX = !sRenderer.flipX;
        walkingToTheLeft = !walkingToTheLeft;
    }

    /// <summary>
    /// Caso colida com o jogador, dá dano nele.
    /// Ao colidir com algum inimigo ou com o jogador, muda de direção.
    /// </summary>
    /// <param name="other"> Objeto com que o inimigo colidiu. </param>
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) {
            ChangeDirection();
            if (other.gameObject.CompareTag("Player")) {
                other.gameObject.GetComponent<Life>().TakeDamage(damage);
            }
        }
    }
}