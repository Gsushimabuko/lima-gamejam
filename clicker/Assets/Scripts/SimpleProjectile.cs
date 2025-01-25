using UnityEngine;

public class SimpleProjectile : BaseProjectile
{
    // Puedes agregar funcionalidades específicas para este tipo de proyectil aquí.

    protected override void HandleEnemyCollision(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}
