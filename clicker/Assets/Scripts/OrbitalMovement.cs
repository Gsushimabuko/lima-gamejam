using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    public enum OrbType
    {
        Tower1,
        Tower2,
        Tower3
    }

    [System.Serializable]
    public class OrbitalRing
    {
        public OrbType orbType; // Tipo de orbe
        public GameObject prefab; // Prefab del orbe
        public float radiusOffset = 1.0f; // Distancia del anillo al centro
        public float orbitalSpeed = 10f; // Velocidad de rotación del anillo
        public int cost;
        public int count = 0;
        public float costMultiplier;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI countText;
    }

    public List<OrbitalRing> orbitalRings = new List<OrbitalRing>(); // Lista de anillos orbitales
    private Dictionary<OrbitalRing, List<GameObject>> orbsByRing = new Dictionary<OrbitalRing, List<GameObject>>(); // Diccionario para rastrear los orbes por anillo
    private Dictionary<OrbitalRing, float> currentAngles = new Dictionary<OrbitalRing, float>(); // Ángulos actuales de los anillos

    private void Start()
    {
        // Instanciar los orbes de cada anillo
        foreach (OrbitalRing ring in orbitalRings)
        {
            InstantiateOrbs(ring);
            currentAngles[ring] = 0f; // Inicializar ángulo para cada anillo
        }

        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
    }

    private void Update()
    {
        // Actualizar la rotación de cada anillo
        foreach (OrbitalRing ring in orbitalRings)
        {
            currentAngles[ring] += ring.orbitalSpeed * Time.deltaTime;
            if (currentAngles[ring] >= 360f)
            {
                currentAngles[ring] -= 360f;
            }

            RotateOrbs(ring);
        }

        // Presionar espacio para agregar un nuevo orbe al primer anillo como ejemplo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTower(OrbType.Tower1);
        }
    }

    void InstantiateOrbs(OrbitalRing ring)
    {
        // Crear los orbes para un anillo específico
        float angleStep = 360f / ring.count;
        List<GameObject> ringOrbs = new List<GameObject>();

        for (int i = 0; i < ring.count; i++)
        {
            float angle = i * angleStep;
            Vector3 orbPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0) * (ring.radiusOffset + transform.localScale.x);
            GameObject orb = Instantiate(ring.prefab, transform.position + orbPosition, Quaternion.identity);
            ringOrbs.Add(orb);

            // Ajustar la escala del orbe si es necesario
            Vector3 currentScale = orb.transform.localScale;
            orb.transform.localScale = currentScale * GameManager.Instance.globalSize;
        }

        orbsByRing[ring] = ringOrbs;

        ring.costText.text = ring.cost.ToString();
        ring.countText.text = ring.count.ToString();
    }

    public void AddTower(OrbType orbType)
    {
        // Encontrar el anillo correspondiente al tipo de orbe
        OrbitalRing ring = orbitalRings.Find(r => r.orbType == orbType);
        if (ring != null)
        {
            if (GameManager.Instance.dinero < ring.cost)
            {
                return;
            }
            GameManager.Instance.RestarDinero(ring.cost);
            int newCount = orbsByRing[ring].Count + 1;
            DestroyAllOrbs(ring);
            ring.count = newCount;
            ring.cost = (int)(ring.cost * ring.costMultiplier);
            InstantiateOrbs(ring);
        }
    }

    public void AddTower1()
    {
        AddTower(OrbType.Tower1);
    }

    public void AddTower2()
    {
        AddTower(OrbType.Tower2);
    }

    public void AddTower3()
    {
        AddTower(OrbType.Tower3);
    }

    void DestroyAllOrbs(OrbitalRing ring)
    {
        // Destruir todos los orbes de un anillo específico
        foreach (GameObject orb in orbsByRing[ring])
        {
            Destroy(orb);
        }
        orbsByRing[ring].Clear();
    }

    void RotateOrbs(OrbitalRing ring)
    {
        // Rotar los orbes de un anillo específico
        List<GameObject> ringOrbs = orbsByRing[ring];
        for (int i = 0; i < ringOrbs.Count; i++)
        {
            float angle = (i * (360f / ringOrbs.Count)) + currentAngles[ring];
            Vector3 newPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0) * (ring.radiusOffset + transform.lossyScale.x);
            ringOrbs[i].transform.position = transform.position + newPosition;
        }
    }

    private void HandleActionWithFloat(float value)
    {
        foreach (var ring in orbsByRing)
        {
            foreach (GameObject orb in ring.Value)
            {
                // Obtenemos la escala actual del orbe
                Vector3 currentScale = orb.transform.localScale;

                // Multiplicamos la escala actual por el valor recibido
                orb.transform.localScale = currentScale * value;
            }
        }
    }
}
