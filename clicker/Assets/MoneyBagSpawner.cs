using UnityEngine;

public class MoneyBagSpawner : MonoBehaviour
{
    public GameObject moneyBagPrefab; // Prefab de la bolsa de dinero
    public float spawnInterval = 10f; // Intervalo entre spawns

    void Start()
    {
        // Comienza a generar bolsas de dinero
        InvokeRepeating(nameof(SpawnMoneyBag), spawnInterval, spawnInterval);
    }

    void SpawnMoneyBag()
    {
        // Determina una posición aleatoria desde los bordes
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instancia la bolsa de dinero en la posición calculada
        Instantiate(moneyBagPrefab, spawnPosition, Quaternion.identity);
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

        spawnPos.z = 0; // Asegúrate de que las bolsas estén en el plano 2D
        return spawnPos;
    }
}
