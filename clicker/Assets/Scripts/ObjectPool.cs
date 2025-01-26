using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Lista de Prefabs de Proyectiles")]
    [SerializeField] private List<GameObject> prefabObjects; // Lista de diferentes tipos de proyectiles

    [Header("Tamaño inicial del Pool")]
    [SerializeField] private int poolSize = 100;

    [Header("Lista de Proyectiles")]
    private Dictionary<GameObject, List<GameObject>> objectsPool = new Dictionary<GameObject, List<GameObject>>();

    //----------------------------------------------------------------

    void Start()
    {
        // Crear un pool inicial para cada tipo de prefab
        foreach (GameObject prefab in prefabObjects)
        {
            if (!objectsPool.ContainsKey(prefab))
            {
                objectsPool[prefab] = new List<GameObject>();
                AddProjectileToPool(prefab, poolSize);
            }
        }
    }

    //----------------------------------------------------------------

    private void AddProjectileToPool(GameObject prefab, int cantidad)
    {
        // Crear la cantidad especificada de proyectiles para el prefab dado
        for (int i = 0; i < cantidad; i++)
        {
            GameObject newObject = Instantiate(prefab, this.transform);

            // Desactivar el objeto y agregarlo a la lista correspondiente
            newObject.SetActive(false);
            objectsPool[prefab].Add(newObject);

            // Hacer que el objeto sea hijo del Pool para mantenerlo organizado
            newObject.transform.parent = transform;
        }
    }

    //----------------------------------------------------------------

    public GameObject AskForProjectile(string prefabName, Vector3 spawnPosition)
    {
        // Buscar el prefab por nombre en la lista
        GameObject requestedPrefab = prefabObjects.Find(prefab => prefab.name == prefabName);

        if (requestedPrefab == null)
        {
            Debug.LogError($"El prefab solicitado con el nombre '{prefabName}' no existe en la lista de prefabs.");
            return null;
        }

        // Verificar si el prefab solicitado está en el pool
        if (!objectsPool.ContainsKey(requestedPrefab))
        {
            Debug.LogError($"El prefab solicitado ({prefabName}) no tiene un pool asignado.");
            return null;
        }

        // Buscar un proyectil desactivado dentro del pool de ese prefab
        List<GameObject> poolForPrefab = objectsPool[requestedPrefab];
        foreach (GameObject obj in poolForPrefab)
        {
            if (!obj.activeSelf)
            {
                // Configurar posición y activar el objeto
                obj.transform.position = spawnPosition;
                obj.SetActive(true);
                return obj;
            }
        }

        // Si no hay proyectiles disponibles, crear uno nuevo
        AddProjectileToPool(requestedPrefab, 1);
        GameObject newObject = objectsPool[requestedPrefab][objectsPool[requestedPrefab].Count - 1];
        newObject.transform.position = spawnPosition;
        newObject.SetActive(true);
        return newObject;
    }

    //----------------------------------------------------------------

    public bool VerifyAllDisabled()
    {
        // Verificar si todos los proyectiles están desactivados
        foreach (var pool in objectsPool.Values)
        {
            foreach (GameObject obj in pool)
            {
                if (obj.activeSelf)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
