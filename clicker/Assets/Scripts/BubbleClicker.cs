using UnityEngine;
using TMPro; // Usamos TextMeshPro para mostrar el dinero

public class BubbleClicker : MonoBehaviour
{
    public TextMeshProUGUI dineroTexto; // Referencia al texto en pantalla

    [SerializeField] private Bubble bubble;

    //--------------------------------------------------------------

    // Este método se llama cuando el jugador hace clic en el objeto
    public void OnMouseDown()
    {
        // Hacemos que la burbuja crezca
        bubble.Grow();

        // Obtener el número de CryptoMiners del GameManager
        int cryptoMinerCount = GameManager.Instance.cryptoMinerCount;

        // Aumentar el dinero al hacer clic en la burbuja
        // Añadimos 1 de dinero base + el valor de CryptoMiners
        GameManager.Instance.AgregarDinero(1 + cryptoMinerCount);

        // Actualiza el texto en pantalla con el nuevo valor de dinero
        dineroTexto.text = "Dinero: " + GameManager.Instance.dinero;

        // Cambia el color de la burbuja al azar
        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
    }
}