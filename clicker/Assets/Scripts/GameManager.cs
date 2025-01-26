using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Usamos TextMeshPro para mostrar el dinero

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia estática para acceder desde otros scripts

    public int vidaBurbuja = 100; // Vida de la burbuja
    public int dinero; // Dinero del jugador
    public TextMeshProUGUI vidaTexto; // Para mostrar la vida de la burbuja en pantalla
    public TextMeshProUGUI dineroTexto; // Para mostrar el dinero del jugador en pantalla
    public float globalSize = 1f;


    public TextMeshProUGUI tiempoTexto; // Referencia al texto en pantalla para mostrar el tiempo
    private float tiempoTranscurrido = 0f; // Tiempo en segundos

    public bool paused;
    public int cryptoMinerCount = 0;

    void Awake()
    {
        // Asegurarnos de que haya solo una instancia del GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Si ya existe una instancia, destruimos el nuevo objeto
        }

        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
    }

    void Start()
    {
        StartCoroutine(TrackTimeRoutine());
        // Actualizar la interfaz al inicio
        ActualizarInterfaz();
    }

    // Método para reducir la vida de la burbuja
    public void RecibirDano(int dano)
    {
        vidaBurbuja -= dano;
        if (vidaBurbuja <= 0)
        {
            // La burbuja ha sido destruida (puedes añadir lógica para finalizar el juego)
            Debug.Log("GAME OVER");
        }
        ActualizarInterfaz();
    }

    // Método para agregar dinero
    public void AgregarDinero(int cantidad)
    {
        dinero += cantidad;
        ActualizarInterfaz();
    }


    // Método para restar dinero
    public void RestarDinero(int cantidad)
    {
        if (dinero >= cantidad)
        {
            dinero -= cantidad;
            ActualizarInterfaz();
        }
        else
        {
            MostrarErrorDinero();
        }
    }

  public void MostrarErrorDinero()
    {
        Debug.Log("No tienes suficiente dinero.");
        // Aquí puedes añadir lógica adicional como mostrar un mensaje en pantalla, efectos visuales, etc.
    }


    // Método para actualizar la interfaz de usuario
    void ActualizarInterfaz()
    {
        vidaTexto.text = "Vida Burbuja: " + vidaBurbuja;
        dineroTexto.text = "Dinero: " + dinero;
    }



    //TIMER
    private IEnumerator TrackTimeRoutine()
    {
        while (true)
        {
            tiempoTranscurrido += 1f; // Incrementa el tiempo en 1 segundo
            tiempoTexto.text = $"Time: {Mathf.FloorToInt(tiempoTranscurrido)}"; // Actualiza el texto en pantalla
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de actualizar
        }
    }

    // Método para obtener el tiempo actual
    public float GetTiempoTranscurrido()
    {
        return tiempoTranscurrido;
    }




    void Update()
    {
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void HandleActionWithFloat(float value)
    {
        globalSize = globalSize * value;
    }
}
