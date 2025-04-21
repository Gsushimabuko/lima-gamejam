using UnityEngine;

public class ExplosiveProjectile : BaseProjectile
{
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] private string animName = "explosion";
    private bool animationPlayed = false;

    //----------------------------------------------------------------------------

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //----------------------------------------------------------------------------

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            // Si la animaci�n se est� reproduciendo
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animationPlayed)
            {
                // Cuando la animaci�n termina
                animationPlayed = true;

                Invoke(nameof(DisableObject), 0.1f);
            }
        }
        else
        {
            animationPlayed = false; // Reseteamos para permitir futuras ejecuciones
        }
    }

    //----------------------------------------------------------------------------

    protected override void HandleEnemyCollision(GameObject enemy)
    {
        AudioManager.instance.PlaySfx("MyBombExplosion");

        animator.SetTrigger("explosion");
        rb.linearVelocity = Vector3.zero;
        enemy.SetActive(false);
    }

    //----------------------------------------------------------------------------

    private void DisableObject()
    {
        animator.Play("Idle");
        this.gameObject.SetActive(false);
    }
}
