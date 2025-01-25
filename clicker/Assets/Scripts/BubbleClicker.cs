using UnityEngine;
using TMPro; // Usamos TextMeshPro para mostrar el dinero

public class BubbleClicker : MonoBehaviour
{
    public TextMeshProUGUI dineroTexto; // Referencia al texto en pantalla

    // Este método se llama cuando el jugador hace clic en el objeto
    public void OnMouseDown()
    {
        // Aumentar el dinero al hacer clic a través del GameManager
        GameManager.Instance.AgregarDinero(1); // Aumentar 1 al dinero global

        // Actualiza el texto en pantalla (puedes obtener la variable directamente del GameManager)
        dineroTexto.text = "Dinero: " + GameManager.Instance.dinero;

        // Cambia el color de la burbuja al azar
        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        Debug.Log("Click ");
    }
}
