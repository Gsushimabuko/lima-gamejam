using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class BubbleClicker : MonoBehaviour
{
    public TextMeshProUGUI dineroTexto; // Referencia al texto en pantalla
    private int dinero = 0; // Contador de dinero

    // Este método se llama cuando el jugador hace clic en el objeto



   public void OnMouseDown()
    {
        dinero += 1; // Aumentar el dinero al hacer clic
        dineroTexto.text = "Dinero: " + dinero; // Actualizar el texto en pantalla


        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        Debug.Log("Click ");
    }
}
