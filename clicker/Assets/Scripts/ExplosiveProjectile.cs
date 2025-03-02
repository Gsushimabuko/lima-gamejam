using UnityEngine;

public class ExplosiveProjectile : BaseProjectile
{
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] private string animName = "explosion";
    private bool animationPlayed = false;

    [Header("Audio")]
    [SerializeField] private AudioClip explosionSoundClip;
    private AudioSource mAudioSource;

    //----------------------------------------------------------------------------

    void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    //----------------------------------------------------------------------------

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //----------------------------------------------------------------------------
    private void PlayExplotionSound()
    {
        //Reproducimos sonido de Disparo
        mAudioSource.PlayOneShot(explosionSoundClip, 0.20f);
    }

    //----------------------------------------------------------------------------

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            // Si la animación se está reproduciendo
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animationPlayed)
            {
                // Cuando la animación termina
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
        //Reproducimos sonido de Explosion
        PlayExplotionSound();

        animator.SetTrigger("explosion");
        rb.velocity = Vector3.zero;
        enemy.SetActive(false);
    }

    //----------------------------------------------------------------------------

    private void DisableObject()
    {
        animator.Play("Idle");
        this.gameObject.SetActive(false);
    }
}
