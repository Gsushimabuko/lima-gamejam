using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 3; // Vida configurable del enemigo
    public int dineroValor = 10; // Dinero que da el enemigo al ser destruido
    public float velocidad = 2f; // Velocidad de movimiento del enemigo
    public Transform burbuja; // Referencia a la burbuja
    private SpriteRenderer spriteRenderer; // Para cambiar el color del enemigo

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer para cambiar color
        if (burbuja == null)
        {
            // Si la burbuja no está asignada, buscamos el objeto con la etiqueta "Bubble"
            burbuja = GameObject.FindGameObjectWithTag("Bubble").transform;
        }
    }

    void Update()
    {
        if (burbuja != null)
        {
            // Mover al enemigo hacia la burbuja
            MoverHaciaBurbuja();
        }
    }

    void MoverHaciaBurbuja()
    {
        // Mover al enemigo hacia la burbuja
        Vector3 direccion = burbuja.position - transform.position;
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bubble"))
        {
            Debug.Log("¡Enemigo tocó la burbuja!");

            // El enemigo toca la burbuja, reduce la vida de la burbuja
            GameManager.Instance.RecibirDano(10); // Llama al GameManager para reducir vida de la burbuja

            // Destruye al enemigo
            Destroy(gameObject);

            // Agregar dinero al jugador
            GameManager.Instance.AgregarDinero(dineroValor); // Agregar dinero al jugador
        }
    }

    // Método para recibir daño
    public void RecibirDano(int dano)
    {
        vida -= dano; // Reduce la vida del enemigo por el daño recibido

        // Cambiar color según la vida restante
        if (vida <= 0)
        {
            // Cuando la vida llegue a 0 o menos, el enemigo muere
            spriteRenderer.color = Color.red; // Cambia a rojo al morir
            Destroy(gameObject); // El enemigo muere
            GameManager.Instance.AgregarDinero(dineroValor); // Agregar dinero al jugador
        }
        else if (vida == 2)
        {
            spriteRenderer.color = Color.yellow; // Cambia a amarillo cuando queda 2 de vida
        }
        else if (vida == 1)
        {
            spriteRenderer.color = Color.green; // Cambia a verde cuando queda 1 de vida
        }
    }

    // Método para detectar clics sobre el enemigo
    void OnMouseDown()
    {
        // Solo recibe daño si el enemigo aún está vivo
        if (vida > 0)
        {
            RecibirDano(1); // Al hacer clic, el enemigo recibe 1 de daño
        }
    }
}
