using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWalkingEnemy : WalkingEnemy {
    [Tooltip("Distância do raio que verifica se há chão na frente do inimigo")]
    [SerializeField] private float diagonalRayDistance;

    /// <summary>
    /// Atira um raio em 45 graus para baixo na direção em que o inimigo está andando,
    /// e se esse raio não atingir o chão (indicando um buraco), ele muda sua direção.
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
