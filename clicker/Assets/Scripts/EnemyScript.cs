using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 1; // Vida configurada desde el Inspector a 1
    public int dineroValor = 10; // Dinero que otorga al ser destruido
    public float velocidad = 2f; // Velocidad base de movimiento
    public float originalSpeed;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
        originalSpeed = velocidad;
    }

    private void OnDestroy()
    {
        Bubble.OnActionTriggeredWithFloat -= HandleActionWithFloat;
    }

    void Update()
    {
        if (Bubble.Instance != null)
        {
            MoverHaciaBurbuja();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        vida = 1; // Restablecer vida en caso de que sea necesario
        spriteRenderer.color = Color.green;
    }

    //----------------------------------------------------------------

    public void OnEnable()
    {
        vida = 1;

        spriteRenderer.color = Color.white;
    }

    //----------------------------------------------------------------

    void MoverHaciaBurbuja()
    {
        Vector3 direccion = Bubble.Instance.transform.position - transform.position;
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bubble"))
        {
            GameManager.Instance.RecibirDano(10);

            //Reproducimos sonido de Daño
            col.gameObject.GetComponent<Bubble>().PlayDamageEffect();

            Hide();
            GameManager.Instance.AgregarDinero(dineroValor);
        }
    }

    public void RecibirDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            spriteRenderer.color = Color.red;
            Hide();
            GameManager.Instance.AgregarDinero(dineroValor);
        }
        else if (vida == 1)
        {
            spriteRenderer.color = Color.red; // Al ser de vida 1, ya se pone de color rojo
        }
    }

    void OnMouseDown()
    {
        if (vida > 0)
        {
            RecibirDano(1); // Reduces vida a 1 cuando se hace clic en el enemigo
        }
    }

    private void HandleActionWithFloat(float value)
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale * value;
    }
}
