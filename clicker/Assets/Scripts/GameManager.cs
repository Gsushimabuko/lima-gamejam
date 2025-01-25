using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Usamos TextMeshPro para mostrar el dinero

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia estática para acceder desde otros scripts

    public int vidaBurbuja = 100; // Vida de la burbuja
    public int dinero = 0; // Dinero del jugador
    public TextMeshProUGUI vidaTexto; // Para mostrar la vida de la burbuja en pantalla
    public TextMeshProUGUI dineroTexto; // Para mostrar el dinero del jugador en pantalla
    public float globalSize = 1f;

    public bool paused;

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
            Debug.Log("La burbuja ha sido destruida!");
        }
        ActualizarInterfaz();
    }

    // Método para agregar dinero
    public void AgregarDinero(int cantidad)
    {
        dinero += cantidad;
        ActualizarInterfaz();
    }

    // Método para actualizar la interfaz de usuario
    void ActualizarInterfaz()
    {
        vidaTexto.text = "Vida Burbuja: " + vidaBurbuja;
        dineroTexto.text = "Dinero: " + dinero;
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
