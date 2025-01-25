using UnityEngine;

public class OrbShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public float fireRange = 5f; // Rango de disparo
    public float fireCooldown = 1f; // Tiempo entre disparos

    private float lastFireTime;
    [SerializeField] private Transform firePoint;

    private ObjectPool projectilesPool;

    void Start()
    {
        //Obtenemos referencia al Pool de Projectiles
        projectilesPool = GameObject.Find("ProjectilePool").GetComponent<ObjectPool>();

        // Crear un firePoint como hijo del orbe si no existe
        if (firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            fp.transform.SetParent(transform);
            fp.transform.localPosition = Vector3.up; // Ajusta la posición local según necesites
            firePoint = fp.transform;
        }
    }

    void Update()
    {
        TryFire();
    }

    private void TryFire()
    {
        if (Time.time - lastFireTime < fireCooldown) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, fireRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                FireBullet(hit.transform.position);
                lastFireTime = Time.time;
                break;
            }
        }
    }

    private void FireBullet(Vector3 target)
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = projectilesPool.AskForProjectile(firePoint.position);
        Vector3 direction = (target - firePoint.position).normalized;

        bullet.GetComponent<Rigidbody2D>().velocity = direction * 20f; // Cambia la velocidad si lo necesitas
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de disparo en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}
