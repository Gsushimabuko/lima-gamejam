using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    // Singleton de Burbuja
    public static Bubble Instance;

    [Header("Escala de crecimiento")]
    [Range(0.001f, 0.1f)][SerializeField] private float escalaCrecimiento;

    public static Action<float> OnActionTriggeredWithFloat;

    [SerializeField] private int vida = 100;

    [SerializeField] private Animator animator;

    private int count = 0; // Contador principal
    private int internalClickCounter = 0; // Contador interno para controlar los 5 clics

    private AudioSource mAudioSource;

    [Header("Sonidos de la Burbuja")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private List<AudioClip> bubbleClickClips;

    //Indice de sonido de Burbuja
    private int clipSoundIndex;

    //Indicador de direccion de frecuencia
    private int clipFrecuencyIndicator;

    public UnityAction OnBubblePressed;
    public UnityAction OnBubbleTakeDamage;


    //----------------------------------------------------------------

    private void Awake()
    {
        Instance = this;
        vida = 100;

        //Referencia a componentes
        mAudioSource = GetComponent<AudioSource>();

        //Iniciamos el indice de sonido en 0
        clipSoundIndex = 0;

        //Iniciamos el indicador de frecuencia en positivo.
        clipFrecuencyIndicator = 1;
    }

    //----------------------------------------------------------------

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Grow()
    {
        //Invocamos al evento de Burbuja presionada para alertar a los delegados.
        OnBubblePressed?.Invoke();

        animator.SetTrigger("click");

        //Reproducimos sonido de Click
        PlayClickSound();

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

        if (count >= 2000)
        {
            return;
        }

        // Calculamos el factor de cambio de la escala
        float scaleChange = currentScale.x / newScale.x; // Calculamos el factor de cambio

        // Disparamos la acci�n con el factor de cambio
        OnActionTriggeredWithFloat?.Invoke(scaleChange);
    }

    //------------------------------------------------------------------------------------------------------

    public void PlayDamageEffect()
    {
        // Reproduce el sonido de daño
        mAudioSource.PlayOneShot(damageClip, 0.5f);

        //Invocmaos al Evento de "Burbuja Recibe Daño
        OnBubbleTakeDamage?.Invoke();

        // Cambia el color del sprite a rojo por medio segundo
        StartCoroutine(FlashRed());
    }

    public void PlayClickSound()
    {
        // Reproduce el sonido de daño
        mAudioSource.PlayOneShot(bubbleClickClips[clipSoundIndex], 1.00f);

        // Seteamos el indice para el siguiente sonido de click correspondiente
        SetNextClickSound();
    }

    private void SetNextClickSound()
    {
        //Si el indice se encuentra en el tope de la Lista
        if (clipSoundIndex == bubbleClickClips.Count - 1)
        {
            //Cambiamos el indicador de frecuencia a negativo (descenso)
            clipFrecuencyIndicator = -1;
        }

        //Si el indice se encuentra en el inicio de la Lista
        else if (clipSoundIndex == 0)
        {
            //Cambiamos el indicador de frecuencia a Positivo (Ascenso)
            clipFrecuencyIndicator = 1;
        }

        //Modificamos el Indice llevandolo en aumento o descenso segun el indicador.
        clipSoundIndex += (1 * clipFrecuencyIndicator);
        
    }

    //------------------------------------------------------------------------------------------------------------

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
