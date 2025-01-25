using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    public GameObject orbPrefab; // Prefab del orbe
    public int initialOrbCount = 5; // Cantidad inicial de orbes
    public float radiusOffset = 1.0f; // Distancia inicial de los orbes desde el objeto
    public float orbitalSpeed = 10f; // Velocidad de rotación de los orbes
    private List<GameObject> orbs = new List<GameObject>(); // Lista de orbes instanciados
    private float currentAngle = 0f; // Ángulo de rotación global

    private void Start()
    {
        // Instanciar los orbes alrededor del objeto
        InstantiateOrbs(initialOrbCount);
        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
    }

    private void Update()
    {
        // Revisa si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Agregar un nuevo orbe y recalcular las distancias
            AddOrb();
        }

        // Rotar los orbes alrededor del objeto
        currentAngle += orbitalSpeed * Time.deltaTime; // Incrementar el ángulo con el tiempo

        // Asegurarse de que el ángulo no pase de 360 grados
        if (currentAngle >= 360f)
        {
            currentAngle -= 360f;
        }

        RotateOrbs();
    }

    void InstantiateOrbs(int count)
    {
        // Crear los orbes alrededor del objeto, distribuidos equidistantemente
        float angleStep = 360f / count;
        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            Vector3 orbPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0) * (radiusOffset + transform.localScale.x);
            GameObject orb = Instantiate(orbPrefab, transform.position + orbPosition, Quaternion.identity);
            orbs.Add(orb);
            Vector3 currentScale = orb.transform.localScale;
            orb.transform.localScale = currentScale * GameManager.Instance.globalSize;
        }
    }

    void AddOrb()
    {
        // Añadir un nuevo orbe
        int newCount = orbs.Count + 1;
        DestroyAllOrbs();
        InstantiateOrbs(newCount);
    }

    void DestroyAllOrbs()
    {
        // Destruir todos los orbes actuales
        foreach (GameObject orb in orbs)
        {
            Destroy(orb);
        }
        orbs.Clear();
    }

    void RotateOrbs()
    {
        // Rotar los orbes alrededor del objeto utilizando el ángulo global
        for (int i = 0; i < orbs.Count; i++)
        {
            // Calcular la nueva posición del orbe en función del ángulo
            float angle = (i * (360f / orbs.Count)) + currentAngle; // Agregar el ángulo global
            Vector3 newPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0) * (transform.lossyScale.x + radiusOffset);
            orbs[i].transform.position = transform.position + newPosition; // Actualizar la posición del orbe
        }
    }

    private void HandleActionWithFloat(float value)
    {
        foreach (GameObject orb in orbs)
        {
            // Obtenemos la escala actual del orb
            Vector3 currentScale = orb.transform.localScale;

            // Multiplicamos la escala actual por el valor recibido
            orb.transform.localScale = currentScale * value;

            // Si quieres que la escala no sea menor que un valor específico, puedes usar algo como esto:
            // orb.transform.localScale = Vector3.Max(currentScale * value, new Vector3(minScale, minScale, minScale));
        }
    }
}
