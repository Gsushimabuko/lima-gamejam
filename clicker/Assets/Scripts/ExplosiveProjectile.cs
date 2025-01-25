using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : BaseProjectile
{
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] private string animName = "explosion";
    private bool animationPlayed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            print("1");
            // Si la animaci�n se est� reproduciendo
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animationPlayed)
            {
                print("2");
                // Cuando la animaci�n termina
                animationPlayed = true;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            animationPlayed = false; // Reseteamos para permitir futuras ejecuciones
        }
    }

    protected override void HandleEnemyCollision(GameObject enemy)
    {
        animator.SetTrigger("explosion");
        rb.velocity = Vector3.zero;
        enemy.SetActive(false);
    }
}
