using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool pool; // Object Pool de enemigos

    public float spawnInterval = 1.5f; // Intervalo inicial entre spawns
    public float spawnIntervalMultiplier = 0.5f; // Multiplicador para reducir el tiempo de spawn cada incremento

    public float speedMultiplier = 1.1f; // Multiplicador de velocidad de enemigos
    public float waveInterval = 10f; // Tiempo en segundos para aumentar la dificultad

    private float currentSpawnInterval;
    private float nextWaveTime;

    void Start()
    {
        currentSpawnInterval = spawnInterval;
        nextWaveTime = Time.time + waveInterval;

        InvokeRepeating("SpawnEnemy", 0f, currentSpawnInterval);
    }

    void Update()
    {
        // Comprueba si es hora de aumentar la dificultad
        if (Time.time >= nextWaveTime)
        {
            IncreaseDifficulty();
            nextWaveTime += waveInterval;
        }
    }

    void SpawnEnemy()
    {
        // Generar posición aleatoria
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Obtener un enemigo del pool
        GameObject enemy = pool.AskForProjectile(spawnPosition);

        // Aumentar la velocidad del enemigo según el multiplicador
        if (enemy != null)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.velocidad *= speedMultiplier;
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

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

        spawnPos.z = 0;
        return spawnPos;
    }

    void IncreaseDifficulty()
    {
        // Reducir el intervalo de spawn
        currentSpawnInterval *= spawnIntervalMultiplier;

        // Cancelar y volver a invocar el spawn con el nuevo intervalo
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 0f, currentSpawnInterval);

        // Log para depuración
        Debug.Log($"Dificultad aumentada: Nuevo intervalo de spawn = {currentSpawnInterval}, Multiplicador de velocidad = {speedMultiplier}");
    }
}
