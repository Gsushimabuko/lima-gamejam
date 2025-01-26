using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float radius = 3f; // Rango máximo para moverse desde la posición inicial
    public float speed = 2f;  // Velocidad del movimiento
    private Vector2 originalPosition; // Posición inicial del objeto
    private Vector2 targetPosition;   // Posición objetivo

    void Start()
    {
        // Guardamos la posición inicial
        originalPosition = transform.position;
        // Calculamos una posición aleatoria inicial
        SetNewTargetPosition();
    }

    void Update()
    {
        // Movemos el objeto hacia la posición objetivo
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el objeto llega al objetivo, calculamos una nueva posición aleatoria
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        // Generamos una posición aleatoria dentro del círculo de radio "radius"
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
