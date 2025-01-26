using System.Collections;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{

    public int cryptoMinerPrice; // Precio del Crypto Miner
    public int botSpawnerPrice; // Precio del Bot Spawner
    public int networkMarketingPrice; // Precio del Bot Spawner
    public int networkMarketingMultiply; // Precio del Bot Spawner
    public float botInterval = 1f; // Tiempo entre clicks automáticos del bot

    private int activeBotsCount = 0; // Contador de bots activos
    private float minSpacing = 1f; // Espacio mínimo entre CryptoMiners

    // Compra de Crypto Miner
    public void BuyCryptoMiner()
    {
        if (GameManager.Instance.dinero >= cryptoMinerPrice)
        {
            GameManager.Instance.RestarDinero(cryptoMinerPrice);
  

            // Incrementamos el contador en el GameManager
            GameManager.Instance.cryptoMinerCount++;
            Debug.Log($"Cantidad actual de Crypto Miners: {GameManager.Instance.cryptoMinerCount}");
        }
        else
        {
            GameManager.Instance.MostrarErrorDinero();
        }
    }


    // Compra de Bot Spawner
    public void BuyBotSpawner()
    {
        if (GameManager.Instance.dinero >= botSpawnerPrice)
        {
            GameManager.Instance.RestarDinero(botSpawnerPrice);

            // Inicia la corutina del bot
            StartCoroutine(BotSpawnerRoutine());

            // Incrementa el contador de bots
            activeBotsCount++;
            Debug.Log($"Cantidad actual de Bots Spawners: {activeBotsCount}");
        }
        else
        {
            GameManager.Instance.MostrarErrorDinero();
        }
    }

    // Rutina del Bot Spawner
    private IEnumerator BotSpawnerRoutine()
    {
        while (true)
        {
            // Simula un click en la burbuja
            GameManager.Instance.AgregarDinero(1 + GameManager.Instance.cryptoMinerCount);
            Debug.Log("Bot clickeó la burbuja");

            // Espera el tiempo definido antes de volver a clickear
            yield return new WaitForSeconds(botInterval);
        }
    }

    // Compra de Network Marketing
    public void BuyNetworkMarketing()
    {


        if (GameManager.Instance.dinero >= networkMarketingPrice)
        {
            GameManager.Instance.RestarDinero(networkMarketingPrice);

            // Duplica la cantidad de bots activos
            int newBotsToAdd = activeBotsCount; // Duplicamos los bots actuales
            for (int i = 0; i < newBotsToAdd; i++)
            {
                StartCoroutine(BotSpawnerRoutine());
            }
            activeBotsCount += newBotsToAdd;

            // Multiplica por 5 el CryptoMiner Count
            GameManager.Instance.cryptoMinerCount *= networkMarketingMultiply;

            Debug.Log($"Network Marketing aplicado: {activeBotsCount} Bots Spawners activos, CryptoMinerCount = {GameManager.Instance.cryptoMinerCount}");
        }
        else
        {
            GameManager.Instance.MostrarErrorDinero();
        }
    }
}
