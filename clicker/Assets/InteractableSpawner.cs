using UnityEngine;

public class InteractableSpawner : MonoBehaviour
{
    public GameObject moneyBagPrefab;  // Prefab de la bolsa de dinero
    public GameObject minePrefab;      // Prefab de la mina

    public float moneyBagSpawnInterval = 15f;  // Intervalo de aparición de la bolsa de dinero
    public float mineSpawnInterval = 25f;      // Intervalo de aparición de la mina

    private float moneyBagTimer = 0f;  // Temporizador para la bolsa de dinero
    private float mineTimer = 0f;      // Temporizador para la mina




    void Update()
    {
        // Controlamos el intervalo de aparición de la bolsa de dinero
        moneyBagTimer += Time.deltaTime;
        if (moneyBagTimer >= moneyBagSpawnInterval)
        {
            moneyBagTimer = 0f;
            SpawnMoneyBag();
        }

        // Controlamos el intervalo de aparición de las minas
        mineTimer += Time.deltaTime;
        if (mineTimer >= mineSpawnInterval)
        {
            mineTimer = 0f;
            SpawnMine();
        }
    }
    void SpawnMoneyBag()
    {
        Instantiate(moneyBagPrefab, Vector3.zero, Quaternion.identity);
    }

    void SpawnMine()
    {
        Instantiate(minePrefab, Vector3.zero, Quaternion.identity);
    }



}
