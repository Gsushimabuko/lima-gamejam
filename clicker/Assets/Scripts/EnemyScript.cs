using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 3; // Vida configurable del enemigo
    public int dineroValor = 10; // Dinero que da el enemigo al ser destruido
    public float velocidad = 2f; // Velocidad de movimiento del enemigo

    private SpriteRenderer spriteRenderer; // Para cambiar el color del enemigo

    //----------------------------------------------------------------

    void Awake()
    {
        // Obtener el SpriteRenderer para cambiar color
        spriteRenderer = GetComponent<SpriteRenderer>();
        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
    }
    
    //----------------------------------------------------------------

    void Update()
    {
        //Si hay una instancia de Bubble asignada
        if (Bubble.Instance != null)
        {
            // Movemos al enemigo hacia la burbuja
            MoverHaciaBurbuja();
        }
    }

    //----------------------------------------------------------------

    public void Hide()
    {
        //Desactivamos el GameObject
        gameObject.SetActive(false);

        vida = 3;

        spriteRenderer.color = Color.green;
    }

    //----------------------------------------------------------------

    void MoverHaciaBurbuja()
    {
        // Mover al enemigo hacia la burbuja
        Vector3 direccion = Bubble.Instance.transform.position - transform.position;
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    //----------------------------------------------------------------

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bubble"))
        {
            Debug.Log("¡Enemigo tocó la burbuja!");

            // El enemigo toca la burbuja, reduce la vida de la burbuja
            GameManager.Instance.RecibirDano(10); // Llama al GameManager para reducir vida de la burbuja

            // Desactivamos al enemigo
            Hide();

            // Agregar dinero al jugador
            GameManager.Instance.AgregarDinero(dineroValor); // Agregar dinero al jugador
        }
    }

    //----------------------------------------------------------------

    // Método para recibir daño
    public void RecibirDano(int dano)
    {
        // Reduce la vida del enemigo
        vida -= dano; 

        // Cambiar color según la vida restante
        if (vida <= 0)
        {
            // Cuando la vida llegue a 0 o menos, el enemigo muere
            spriteRenderer.color = Color.red; // Cambia a rojo al morir
            Hide();

            GameManager.Instance.AgregarDinero(dineroValor); // Agregar dinero al jugador
        }
        else if (vida == 2)
        {
            spriteRenderer.color = Color.yellow; // Cambia a amarillo cuando queda 2 de vida
        }
        else if (vida == 1)
        {
            spriteRenderer.color = Color.red; // Cambia a verde cuando queda 1 de vida
        }
    }

    //----------------------------------------------------------------

    // Método para detectar clics sobre el enemigo
    void OnMouseDown()
    {
        // Solo recibe daño si el enemigo aún está vivo
        if (vida > 0)
        {
            RecibirDano(1); // Al hacer clic, el enemigo recibe 1 de daño
        }
    }

    private void HandleActionWithFloat(float value)
    {
        Vector3 currentScale = transform.localScale;

        transform.localScale = currentScale * value;
    }
}
