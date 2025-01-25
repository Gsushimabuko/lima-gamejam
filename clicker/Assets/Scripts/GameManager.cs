<<<<<<< HEAD
using UnityEngine;
using TMPro; // Usamos TextMeshPro para mostrar el dinero

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia estática para acceder desde otros scripts

    public int vidaBurbuja = 100; // Vida de la burbuja
    public int dinero = 0; // Dinero del jugador
    public TextMeshProUGUI vidaTexto; // Para mostrar la vida de la burbuja en pantalla
    public TextMeshProUGUI dineroTexto; // Para mostrar el dinero del jugador en pantalla

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
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public bool paused;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
>>>>>>> 425e26579b47e40de5e95c8bbb9f95e32e920a0a
    }

    void Start()
    {
<<<<<<< HEAD
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
}
=======
        
    }

    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
>>>>>>> 425e26579b47e40de5e95c8bbb9f95e32e920a0a
