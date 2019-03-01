using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour {
    [Header("Game Design Variables")]
    [Tooltip("Indicates the enemy moving direction")]
    [SerializeField] protected bool walkingToTheLeft;
    [Tooltip("Speed in which the enemy moves")]
    [SerializeField] private float speed;
    [Tooltip("Distance of the ray that checks if there is a wall in front of the enemy")]
    [SerializeField] private float rayDistance;
    private SpriteRenderer sRenderer;

    /// <summary>
    /// Gets sprite renderer component and sets its moving direction
    /// </summary>
    private void Start() {
        sRenderer = GetComponent<SpriteRenderer>();

        speed = walkingToTheLeft ? -speed : speed;
        sRenderer.flipX = walkingToTheLeft;
    }

    /// <summary>
    /// Checks if there is a wall in front of the enemy. If so, changes its moving direction
    /// </summary>
    protected void Update() {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(100, 0), speed);

        Vector2 direction = walkingToTheLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, rayDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay((Vector2)this.transform.position, direction * rayDistance, Color.green, 0.1f);
        
        if (hit) {
            ChangeDirection();
        }
    }

    /// <summary>
    /// Changes the movement direction of the enemy
    /// </summary>
    protected void ChangeDirection() {
        speed = -speed;
        sRenderer.flipX = !sRenderer.flipX;
        walkingToTheLeft = !walkingToTheLeft;
    }

    /// <summary>
    /// Changes the movement direction of the enemy if it hits the player
    /// </summary>
    /// <param name="other">Object </param>
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            ChangeDirection();
        }
    }
}