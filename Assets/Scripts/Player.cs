using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	private SpriteRenderer sRenderer;
	private CapsuleCollider2D groundCheck;
	private bool lookingRight = true;
	private bool canAttack = true;
	private bool grounded;
    [Header("Referências a objetos ou componentes")]
	[Tooltip("Prefab do projétil atirado pelo jogador")]
	[SerializeField] private GameObject projectile; 
	[Space(5)]
    [Header("Variáveis de Game Design")]
    [Tooltip("Velocidade do jogador")]
	[SerializeField] [Range(0f, 10f)] private float speed;
    [Tooltip("Força do pulo do jogador")]
	[SerializeField] [Range(0f, 10f)] private float jumpForce;
    [Tooltip("Tempo de espera para o jogador poder atirar novamente")]
	[SerializeField] [Range(0f, 5f)] private float attackCooldown;
    [Tooltip("Distância do jogador na qual o projeto é instanciado")]
	[SerializeField] [Range(0f, 1f)] private float attackOffset;
    [Tooltip("Distância mínima até o solo para que o jogador possa pular")]
	[SerializeField] [Range(0f, 1f)] private float groundCheckDistance;

	/// <summary>
	/// Pega referências dos componentes.
	/// </summary>
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sRenderer = GetComponent<SpriteRenderer>();
	}
	

	/// <summary>
	/// Lida principalmente com a movimentação e ataque do jogador. 
	/// Também verifica qual a posição correta da Sprite do jogador.
	/// </summary>
	void Update () {
		// Movimento horizontal 
		rb.velocity	= new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);

		// Checa se o jogador está no chão atirando o raio para baixo
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));
		Debug.DrawRay(this.transform.position, new Vector2(0f, -groundCheckDistance), Color.green);
		if (hit) {
			grounded = true;
		} else {
			grounded = false;
		}

		// Pulo
		if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}

		// Ataque
		if (canAttack && Input.GetKeyDown(KeyCode.J)) {
			StartCoroutine("AttackCooldown");

			// A direção do projétil varia de acordo com o lado para qual o jogador está virado 
			int direction = lookingRight ? 1 : -1;
			GameObject instance = Instantiate(projectile, new Vector2(this.transform.position.x + direction * attackOffset, this.transform.position.y), Quaternion.identity);
			instance.GetComponent<Projectile>().speed *= direction; 
		}

		// Checa para qual lado o jogador está olhando, e inverte a sprite
		if (rb.velocity.x > 0f) {
			lookingRight = true;
			sRenderer.flipX = false;
		} else if (rb.velocity.x < 0f) {
			lookingRight = false;
			sRenderer.flipX = true;
		}
	}
	

	/// <summary>
	/// Impede que o jogador atire por um tempo.
	/// </summary>
	/// <returns> Espera pelo tempo de cooldown. </returns>
	private IEnumerator AttackCooldown() {
		canAttack = false;
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}
}