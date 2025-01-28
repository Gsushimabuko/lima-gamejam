using TMPro;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // El GameObject (proyectil) que será disparado
    public Transform spawnPoint;    // Punto de origen de los disparos
    public bool canShoot = false;   // Control para habilitar o deshabilitar los disparos
    public float shootInterval = 2f; // Tiempo entre cada lote de disparos (en segundos)
    public int shotsPerBatch = 5;   // Cantidad de disparos por lote
    public float intervalBetweenShots = 0.25f; // Intervalo entre disparos en un lote

    private float nextBatchTime = 0f; // Tiempo para el próximo lote
    private int shotsFiredInBatch = 0; // Contador de disparos realizados en el lote
    private float nextShotTime = 0f;  // Tiempo para el próximo disparo en el lote

    public int cost;
    public int count = 0;
    public float costMultiplier = 2;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI countText;

    private void Start()
    {
        costText.text = cost.ToString();
        countText.text = count.ToString();
    }

    private void Update()
    {
        if (canShoot)
        {
            // Comprobar si es hora de un nuevo lote de disparos
            if (Time.time >= nextBatchTime)
            {
                nextBatchTime = Time.time + shootInterval;
                shotsFiredInBatch = 0;
                nextShotTime = Time.time; // Comenzar el lote inmediatamente
            }

            // Realizar disparos en el lote actual
            if (shotsFiredInBatch < shotsPerBatch && Time.time >= nextShotTime)
            {
                GameObject nearestEnemy = FindNearestEnemy();
                if (nearestEnemy != null)
                {
                    // Crear el proyectil
                    GameObject projectile = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

                    // Obtener la posición del enemigo ajustada para mantener la rotación en Z
                    Vector3 direction = nearestEnemy.transform.position - spawnPoint.position;
                    direction.z = 0; // Asegurarse de que solo se oriente en el eje Z

                    // Rotar el proyectil hacia el enemigo más cercano en el plano 2D (X y Y)
                    projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                }

                shotsFiredInBatch++;
                nextShotTime = Time.time + intervalBetweenShots;
            }
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(spawnPoint.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    public void AddLaser()
    {
        if (GameManager.Instance.dinero < cost)
        {
            return;
        }
        if (canShoot == false)
            canShoot = true;
        else
            shotsPerBatch++;

        GameManager.Instance.RestarDinero(cost);

        cost = (int)(cost * costMultiplier);
        costText.text = cost.ToString();
        count++;
        countText.text = count.ToString();
    }
}
