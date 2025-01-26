using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float radius = 3f; // Rango m�ximo para moverse desde la posici�n inicial
    public float speed = 2f;  // Velocidad del movimiento
    private Vector2 originalPosition; // Posici�n inicial del objeto
    private Vector2 targetPosition;   // Posici�n objetivo

    void Start()
    {
        // Guardamos la posici�n inicial
        originalPosition = transform.position;
        // Calculamos una posici�n aleatoria inicial
        SetNewTargetPosition();
    }

    void Update()
    {
        // Movemos el objeto hacia la posici�n objetivo
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el objeto llega al objetivo, calculamos una nueva posici�n aleatoria
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        // Generamos una posici�n aleatoria dentro del c�rculo de radio "radius"
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        targetPosition = originalPosition + randomOffset;
    }

    private void OnDrawGizmos()
    {
        // Dibujamos el radio en el editor para visualizar el rango del movimiento
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(originalPosition, radius);
    }
}
