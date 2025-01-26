using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 3; // Vida configurable
    public int dineroValor = 10; // Dinero que otorga al ser destruido
    public float velocidad = 2f; // Velocidad base de movimiento

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
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
        vida = 3;
        spriteRenderer.color = Color.green;
    }

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
        else if (vida == 2)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (vida == 1)
        {
            spriteRenderer.color = Color.red;
        }
    }

    void OnMouseDown()
    {
        if (vida > 0)
        {
            RecibirDano(1);
        }
    }

    private void HandleActionWithFloat(float value)
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale * value;
    }
}
