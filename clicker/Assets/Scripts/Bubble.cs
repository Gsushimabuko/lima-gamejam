using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    // Singleton de Burbuja
    public static Bubble Instance;

    [Range(0.001f, 0.1f)][SerializeField] private float escalaCrecimiento;

    public static Action<float> OnActionTriggeredWithFloat;

    [SerializeField] private int vida = 100;

    [SerializeField] private Animator animator;

    private int count = 0; // Contador principal
    private int internalClickCounter = 0; // Contador interno para controlar los 5 clics

    private AudioSource mAudioSource;
    [SerializeField] private AudioClip clipDamage;
    

    //----------------------------------------------------------------

    private void Awake()
    {
        Instance = this;
        vida = 100;

        mAudioSource = GetComponent<AudioSource>();
    }

    //----------------------------------------------------------------

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Grow()
    {
        animator.SetTrigger("click");

        // Capturamos la escala inicial
        Vector3 currentScale = transform.localScale;

        // Calculamos la nueva escala
        Vector3 newScale = currentScale + new Vector3(escalaCrecimiento, escalaCrecimiento, 1);

        // Incrementar el contador interno
        internalClickCounter++;

        // Incrementar el contador principal solo cada 5 clics
        if (internalClickCounter >= 10)
        {
            count++;
            internalClickCounter = 0; // Reiniciar el contador interno
        }

        if (count >= 1500)
        {
            return;
        }

        // Calculamos el factor de cambio de la escala
        float scaleChange = currentScale.x / newScale.x; // Calculamos el factor de cambio

        // Disparamos la acci�n con el factor de cambio
        //OnActionTriggeredWithFloat?.Invoke(scaleChange);
    }

    //----------------------------------------------------------------

    public void PlayDamageSound()
    {
        // Reproduce el sonido de daño
        mAudioSource.PlayOneShot(clipDamage, 0.5f);

        // Cambia el color del sprite a rojo por medio segundo
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Guarda el color original del sprite
            Color originalColor = spriteRenderer.color;

            // Cambia el color del sprite a rojo
            spriteRenderer.color = Color.red;

            // Espera medio segundo
            yield return new WaitForSeconds(0.15f);

            // Restaura el color original del sprite
            spriteRenderer.color = originalColor;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on the object.");
        }
    }

    //----------------------------------------------------------------
}
