using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Object Pool de enemigos
    [SerializeField] private ObjectPool pool;

    //Tiempo intervalo de Spawn
    public float spawnInterval = 1.5f; // Intervalo de tiempo entre spawns

    //-----------------------------------------------------------

    void Start()
    {
        // Comienza el spawn de enemigos
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    //-----------------------------------------------------------


    void SpawnEnemy()
    {
        // Elegir un borde aleatorio para el spawn
        Vector3 spawnPosition = GetRandomSpawnPosition();

        //Hacemos que el Pool spawnee un enemigo
        pool.AskForProjectile(spawnPosition);
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Definir los bordes de la pantalla
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Elegir un borde aleatorio
        int spawnSide = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (spawnSide)
        {
            case 0: // Arriba
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, screenWidth), screenHeight, 10f));
                break;
            case 1: // Abajo
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, screenWidth), 0f, 10f));
                break;
            case 2: // Izquierda
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, Random.Range(0, screenHeight), 10f));
                break;
            case 3: // Derecha
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, Random.Range(0, screenHeight), 10f));
                break;
        }

        spawnPos.z = 0; // Asegúrate de que los enemigos estén en el plano 2D
        return spawnPos;
    }
}
