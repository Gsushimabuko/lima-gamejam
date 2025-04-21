using TMPro; // Usamos TextMeshPro para mostrar el dinero
using UnityEngine;

public class BubbleClicker : MonoBehaviour
{
    public TextMeshProUGUI dineroTexto; // Referencia al texto en pantalla
    public GameObject instText; // Prefab del texto a instanciar

    [SerializeField] private Bubble bubble;

    // Color al que parpadeará la burbuja
    [SerializeField] private Color flashColor = Color.green; // Color del parpadeo

    private Color originalColor; // Color original de la burbuja
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer



    //--------------------------------------------------------------

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Guardamos el color original de la burbuja
    }

    //--------------------------------------------------------------

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lanzamos un rayo desde el mouse
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    ProcesarClick();
                    break; // solo necesitas procesarlo una vez
                }
            }
        }
    }

    //--------------------------------------------------------------

    private void ProcesarClick()
    {
        if (Time.timeScale == 0)
            return;

        spriteRenderer.color = flashColor;
        bubble.Grow();
        Invoke(nameof(RevertColor), 0.1f);

        int cryptoMinerCount = GameManager.Instance.cryptoMinerCount;
        GameManager.Instance.AgregarDinero(1 + cryptoMinerCount);
        dineroTexto.text = GameManager.Instance.dinero.ToString();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 randomOffset = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1.5f), 0);
        Vector3 spawnPosition = mousePosition + randomOffset;

        GameObject textInst = Instantiate(instText, spawnPosition, Quaternion.identity);
        textInst.GetComponent<TextMeshPro>().text = "+" + (1 + cryptoMinerCount).ToString();
        Destroy(textInst, 2f);
    }

    //--------------------------------------------------------------

    private void RevertColor()
    {
        // Revertir el color al original
        spriteRenderer.color = originalColor;
    }
}
