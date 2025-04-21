using UnityEngine;

public class OrbShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public float fireRange = 5f; // Rango de disparo
    public float fireCooldown = 1f; // Tiempo entre disparos

    private float lastFireTime;
    [SerializeField] private Transform firePoint;

    private ObjectPool projectilesPool;

    //------------------------------------------------------------------

    void Start()
    {
        // Obtenemos referencia al Pool de Projectiles
        projectilesPool = GameObject.Find("ProjectilePool").GetComponent<ObjectPool>();

        // Crear un firePoint como hijo del orbe si no existe
        if (firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            fp.transform.SetParent(transform);
            fp.transform.localPosition = Vector3.up; // Ajusta la posici�n local seg�n necesites
            firePoint = fp.transform;
        }
    }

    //------------------------------------------------------------------

    void Update()
    {
        TryFire();
    }

    //------------------------------------------------------------------

    private void TryFire()
    {
        if (Time.time - lastFireTime < fireCooldown) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, fireRange);
        bool enemyFound = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                RotateFirePoint(hit.transform.position); // Rota el firePoint hacia el enemigo
                FireBullet(hit.transform.position);
                lastFireTime = Time.time;
                enemyFound = true;
                break;
            }
        }

        if (!enemyFound)
        {
            // Apunta al opuesto del centro del mapa (0,0,0)
            Vector3 oppositeDirection = (transform.position - Vector3.zero).normalized;
            RotateFirePoint(transform.position + oppositeDirection);
        }
    }

    //------------------------------------------------------------------

    private void RotateFirePoint(Vector3 target)
    {
        if (firePoint == null) return;

        // Calcula la direcci�n hacia el objetivo
        Vector3 direction = target - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Obt�n el �ngulo en grados

        // Aplica la rotaci�n al firePoint
        firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    //------------------------------------------------------------------

    private void FireBullet(Vector3 target)
    {
        if (bulletPrefab == null || firePoint == null) return;

        // PLAY SOUND FIRE
        GameObject bullet = projectilesPool.AskForProjectile(bulletPrefab.name, firePoint.position);
        Vector3 direction = (target - firePoint.position).normalized;

        AudioManager.instance.PlaySfx("MyLaserShot");

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 20f; // Cambia la velocidad si lo necesitas
    }

    //------------------------------------------------------------------

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de disparo en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}
