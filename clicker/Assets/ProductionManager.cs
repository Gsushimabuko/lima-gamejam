using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    public int cryptoMinerPrice; // Precio del Crypto Miner
    public int cryptoMineCount = 0;
    public TextMeshProUGUI cryptoTextPrice;
    public TextMeshProUGUI cryptoTextCount;
    public float cryptoMultiplier = 1.5f;

    public int botSpawnerPrice; // Precio del Bot Spawner
    public int botSpawnerCount = 0;
    public TextMeshProUGUI botTextPrice;
    public TextMeshProUGUI botTextCount;
    public float botMultiplier = 1.5f;

    public int networkMarketingPrice; // Precio del Network Marketing
    public int networkMarketingPriceCount = 0;
    public TextMeshProUGUI networkMarketingTextPrice;
    public TextMeshProUGUI networkMarketingTextCount;
    public float networkMarketingMultiplier = 1.5f;

    public float networkMarketingMultiply; // Multiplicador del Network Marketing
    public float botInterval = 1f; // Tiempo entre clicks automáticos del bot

    private int activeBotsCount = 0; // Contador de bots activos
    private float minSpacing = 1f; // Espacio mínimo entre CryptoMiners

    private void Start()
    {
        // Inicializa los textos con los valores iniciales
        UpdateTexts();
    }

    // Actualiza los textos
    private void UpdateTexts()
    {
        cryptoTextPrice.text = $"{cryptoMinerPrice}";
        cryptoTextCount.text = $"{cryptoMineCount}";

        botTextPrice.text = $"{botSpawnerPrice}";
        botTextCount.text = $"{botSpawnerCount}";

        networkMarketingTextPrice.text = $"{networkMarketingPrice}";
        networkMarketingTextCount.text = $"{networkMarketingPriceCount}";
    }

    // Compra de Crypto Miner
    public void BuyCryptoMiner()
    {
        if (GameManager.Instance.dinero >= cryptoMinerPrice)
        {
            GameManager.Instance.RestarDinero(cryptoMinerPrice);

            cryptoMineCount++;

            // Incrementamos el contador en el GameManager
            GameManager.Instance.cryptoMinerCount++;
            Debug.Log($"Cantidad actual de Crypto Miners: {GameManager.Instance.cryptoMinerCount}");

            // Actualiza los textos
            UpdateTexts();

            cryptoMinerPrice = (int)(cryptoMinerPrice * cryptoMultiplier);
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
            botSpawnerCount++;
            Debug.Log($"Cantidad actual de Bots Spawners: {activeBotsCount}");

            // Actualiza los textos
            UpdateTexts();

            botSpawnerPrice = (int)(botSpawnerPrice * botMultiplier);
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

            // Multiplica por el multiplicador el CryptoMiner Count
            GameManager.Instance.cryptoMinerCount = (int)Math.Round(GameManager.Instance.cryptoMinerCount * networkMarketingMultiply);

            networkMarketingPriceCount++;
            Debug.Log($"Network Marketing aplicado: {activeBotsCount} Bots Spawners activos, CryptoMinerCount = {GameManager.Instance.cryptoMinerCount}");

            // Actualiza los textos
            UpdateTexts();

            networkMarketingPrice  = (int)(networkMarketingPrice * networkMarketingMultiplier);
        }
        else
        {
            GameManager.Instance.MostrarErrorDinero();
        }
    }
}
