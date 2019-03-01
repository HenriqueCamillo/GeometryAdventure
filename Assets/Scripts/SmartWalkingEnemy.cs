using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWalkingEnemy : WalkingEnemy {
    [Tooltip("Distance of the ray to check if there is ground")]
    [SerializeField] private float diagonalRayDistance;

    /// <summary>
    /// Sends a raycast in a 45 degrees aiming to the ground, 
    /// and if it doesn't see any ground, it changes its movement direction.
    /// </summary>
    private new void Update() {
        base.Update();
        
        Vector2 direction = walkingToTheLeft ? new Vector2(-1, -1) : new Vector2(1, -1);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, diagonalRayDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay((Vector2)this.transform.position, direction * diagonalRayDistance, Color.green, 0.1f);

        if (!hit) {
            ChangeDirection();
        }
    }
}
