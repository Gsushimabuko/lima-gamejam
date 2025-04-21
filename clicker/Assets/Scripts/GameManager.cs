using System.Collections;
using TMPro; // Usamos TextMeshPro para mostrar el dinero
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia estática para acceder desde otros scripts

    public int vidaBurbuja = 100; // Vida de la burbuja
    public int dinero; // Dinero del jugador
    public TextMeshProUGUI dineroTexto; // Para mostrar el dinero del jugador en pantalla
    public float globalSize = 1f;


    private float tiempoTranscurrido = 0f; // Tiempo en segundos

    public bool paused;
    public int cryptoMinerCount = 0;

    [SerializeField] private Slider HealthBarSlider;

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
        HealthBarSlider.value = vidaBurbuja;

        if (vidaBurbuja <= 0)
        {
            PauseMenu.instance.GameOver();
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
    public void ActualizarInterfaz()
    {
        dineroTexto.text = dinero.ToString();
    }



    //TIMER
    private IEnumerator TrackTimeRoutine()
    {
        while (true)
        {
            tiempoTranscurrido += 1f; // Incrementa el tiempo en 1 segundo
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
