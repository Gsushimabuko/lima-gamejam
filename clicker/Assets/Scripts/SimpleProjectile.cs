using UnityEngine;

public class SimpleProjectile : BaseProjectile
{
    // Puedes agregar funcionalidades espec�ficas para este tipo de proyectil aqu�.

    protected override void HandleEnemyCollision(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}
