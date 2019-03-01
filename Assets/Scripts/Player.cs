using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	private CapsuleCollider2D groundCheck;
	private bool lookingRight = true;
	private bool canAttack = true;
	public bool grounded;
	[SerializeField] private GameObject[] projectiles; 
	[SerializeField] [Range(0f, 10f)] private float speed;
	[SerializeField] [Range(0f, 10f)] private float jumpForce;
	[SerializeField] [Range(0f, 5f)] private float attackCooldown;
	[SerializeField] [Range(0f, 1f)] private float groundCheckDistance;


	// Use this for initialization
	void Start () {
		// Gets references to componets
		rb = GetComponent<Rigidbody2D>();
		// groundCheck = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Horizontal movement
		rb.velocity	= new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);

		// Ground Check
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));
		Debug.DrawRay(this.transform.position, new Vector2(0f, -groundCheckDistance), Color.green);
		if (hit) {
			grounded = true;
		} else {
			grounded = false;
		}

		// Jump
		if (grounded && ( (Input.GetKeyDown(KeyCode.W)) || Input.GetKeyDown(KeyCode.UpArrow)) ) {
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}

		// Check looking side
		if (rb.velocity.x > 0f) {
			lookingRight = true;
		} else if (rb.velocity.x < 0f) {
			lookingRight = false;
		}

		// Attack
		if (canAttack) {
			if (Input.GetKeyDown(KeyCode.J)) {
				InstantiateProjectile(0);
			} else if (Input.GetKeyDown(KeyCode.K)) {
				InstantiateProjectile(1);
			} else if (Input.GetKeyDown(KeyCode.L)) {
				InstantiateProjectile(2);
			}
		}


	}

	IEnumerator AttackCooldown() {
		canAttack = false;
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}

	GameObject InstantiateProjectile(int projectile) {
		StartCoroutine("AttackCooldown");

		float direction = lookingRight ? 1 : -1;
		GameObject instance =  Instantiate(projectiles[projectile], new Vector2(this.transform.position.x + direction, this.transform.position.y), Quaternion.identity);
		Rigidbody instanceRb = instance.GetComponent<Rigidbody>();
		instanceRb.velocity = new Vector2((speed * 2) * direction, 0f);
		return instance;
	}
}