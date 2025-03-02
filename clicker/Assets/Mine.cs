using UnityEngine;
using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Vector3 direction; // Direcci�n del movimiento
    private float lifeTime = 5f; // Tiempo que la mina permanece activa antes de desaparecer

    [Header("Audio")]
    [SerializeField] private AudioClip explosionClip;
    private AudioSource mAudioSource;

    void Awake()
    {
        //Obtenemos referencia a componentes
        mAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Configura la direcci�n inicial del movimiento hacia un extremo opuesto
        SetInitialDirection();

        // Destruye la mina despu�s de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mueve la mina en la direcci�n configurada
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnMouseDown()
    {
        // Desactiva la mina tras clic y provoca la explosi�n
        Explode();
        Destroy(gameObject);
    }

    //------------------------------------------------------------------------

    private void PlayExplotionSound()
    {
        //Reproducimos sonido de Disparo
        mAudioSource.PlayOneShot(explosionClip, 0.1f);
    }

    private void SetInitialDirection()
    {
        // Define una direcci�n aleatoria desde un borde hacia el borde opuesto
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 startPos = Vector3.zero;

        // Elegir un borde aleatorio para la posici�n inicial
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

        startPos.z = 0; // Aseg�rate de que est� en el plano 2D
        transform.position = startPos; // Asigna la posici�n inicial
    }

    private void Explode()
    {
        // Eliminar enemigos con tag "Enemy" y ponerles vida 1
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.RecibirDano(1); // La mina les hace 1 de da�o
            }
        }

        //Reproducimos sonido de Explosion
        PlayExplotionSound();

    }
}
