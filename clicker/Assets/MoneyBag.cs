using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Vector3 direction; // Dirección del movimiento
    private float lifeTime = 5f; // Tiempo que la bolsa permanece activa antes de desaparecer

    void Start()
    {
        // Configura la dirección inicial del movimiento hacia un extremo opuesto
        SetInitialDirection();

        // Destruye la bolsa después de un tiempo si no se hace clic en ella
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mueve la bolsa en la dirección configurada
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnMouseDown()
    {
        // Duplica el dinero del jugador
        GameManager.Instance.dinero *= 2;
        GameManager.Instance.ActualizarInterfaz();
        Debug.Log("¡Dinero duplicado!");

        // Desactiva la bolsa tras clic
        Destroy(gameObject);
    }
   

    private void SetInitialDirection()
    {
        // Define una dirección aleatoria desde un borde hacia el borde opuesto
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 startPos = Vector3.zero;

        // Elegir un borde aleatorio para la posición inicial
        int spawnSide = Random.Range(0, 4);

        switch (spawnSide)
        {
            case 0: // Arriba
                startPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, screenWidth), screenHeight, Camera.main.nearClipPlane));
                direction = Vector3.down;
                break;
            case 1: // Abajo
                startPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, screenWidth), 0, Camera.main.nearClipPlane));
                direction = Vector3.up;
                break;
            case 2: // Izquierda
                startPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Random.Range(0, screenHeight), Camera.main.nearClipPlane));
                direction = Vector3.right;
                break;
            case 3: // Derecha
                startPos = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, Random.Range(0, screenHeight), Camera.main.nearClipPlane));
                direction = Vector3.left;
                break;
        }

        startPos.z = 0; // Asegúrate de que esté en el plano 2D
        transform.position = startPos; // Asigna la posición inicial
    }
}
