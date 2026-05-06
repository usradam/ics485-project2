using UnityEngine;

public class EnemyBulletHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        // Invert the bullet holder's x scale relative to the enemy
        transform.localScale = new Vector3(-enemy.localScale.x, enemy.localScale.y, enemy.localScale.z);
    }
}
