using UnityEngine;
using TMPro; // Usamos TextMeshPro para mostrar el dinero

public class BubbleClicker : MonoBehaviour
{
    public TextMeshProUGUI dineroTexto; // Referencia al texto en pantalla
    public GameObject instText; // Prefab del texto a instanciar

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
        GameManager.Instance.AgregarDinero(1 + cryptoMinerCount);

        // Actualiza el texto en pantalla con el nuevo valor de dinero
        dineroTexto.text = "Dinero: " + GameManager.Instance.dinero;

        // Cambia el color de la burbuja al azar
        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegurar que el texto esté en el plano 2D

        // Agregar un desplazamiento aleatorio en X e Y
        Vector3 randomOffset = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1.5f), 0);
        Vector3 spawnPosition = mousePosition + randomOffset;

        // Instanciar el texto en la posición calculada
        GameObject textInst = Instantiate(instText, spawnPosition, Quaternion.identity);
        textInst.GetComponent<TextMeshPro>().text = "+1";

        // Destruir el texto después de 2 segundos
        Destroy(textInst, 2f);
    }

}
