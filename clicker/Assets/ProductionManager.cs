using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    public GameObject cryptoMinerPrefab; // Prefab del Crypto Miner
    public Transform cryptoMinerContainer; // Contenedor donde se instanciar�n los Crypto Miners
    public int cryptoMinerPrice; // Precio del Crypto Miner
    private float spacing = 10f; // Espaciado entre CryptoMiners
    private float minSpacing = 1f; // Espacio m�nimo entre CryptoMiners (ajustable)

    public void BuyCryptoMiner()
    {
        if (GameManager.Instance.dinero >= cryptoMinerPrice)
        {
            GameManager.Instance.RestarDinero(cryptoMinerPrice);
            SpawnCryptoMiner();

            // Incrementamos el contador en el GameManager
            GameManager.Instance.cryptoMinerCount = GameManager.Instance.cryptoMinerCount + 1;
            Debug.Log($"Cantidad actual de Crypto Miners: {GameManager.Instance.cryptoMinerCount}");
        }
        else
        {
            GameManager.Instance.MostrarErrorDinero();
        }
    }

    public void SpawnCryptoMiner()
    {
        // Instancia el CryptoMiner
        GameObject newCryptoMiner = Instantiate(cryptoMinerPrefab);

        // Asignamos al contenedor (esto hace que se mueva dentro del Canvas)
        newCryptoMiner.transform.SetParent(cryptoMinerContainer);

        // Aseguramos que la posici�n inicial sea correcta
        RectTransform rectTransform = newCryptoMiner.GetComponent<RectTransform>();

        // Obtenemos los l�mites del contenedor
        RectTransform containerRect = cryptoMinerContainer.GetComponent<RectTransform>();
        float containerWidth = containerRect.rect.width;
        float containerHeight = containerRect.rect.height;

        // Generamos una posici�n aleatoria dentro de los l�mites del contenedor, con espacio m�nimo
        float randomPosX = Random.Range(-containerWidth / 2 + minSpacing, containerWidth / 2 - minSpacing);
        float randomPosY = Random.Range(-containerHeight / 2 + minSpacing, containerHeight / 2 - minSpacing);

        // Establecemos la nueva posici�n del CryptoMiner
        rectTransform.localPosition = new Vector3(randomPosX, randomPosY, 0);
    }
}
