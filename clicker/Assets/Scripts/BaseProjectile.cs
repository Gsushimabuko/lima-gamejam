using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    protected virtual void Start()
    {
        SubscribeToEvents();
        AdjustScale(GameManager.Instance.globalSize);
    }

    protected virtual void OnEnable()
    {
        AdjustScale(GameManager.Instance.globalSize);
    }

    private void AdjustScale(float sizeMultiplier)
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale * sizeMultiplier;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            HandleEnemyCollision(collider.gameObject);
        }
    }

    protected virtual void HandleEnemyCollision(GameObject enemy)
    {
        
    }

    protected virtual void SubscribeToEvents()
    {
        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
    }

    protected virtual void UnsubscribeFromEvents()
    {
        Bubble.OnActionTriggeredWithFloat -= HandleActionWithFloat;
    }

    protected virtual void HandleActionWithFloat(float value)
    {
        AdjustScale(value);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
