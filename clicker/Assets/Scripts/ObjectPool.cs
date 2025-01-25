using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Prefab de Proyectil")]
    [SerializeField] private GameObject prefabObject;

    [Header("Tamaño del Pool")]
    [SerializeField] private int poolSize = 100;

    [Header("Lista de Proyectiles")]
    public List<GameObject> objectsList;

    //Instancia (Static) de la Clase
    public static ObjectPool instance;

    //----------------------------------------------------------------

    void Awake()
    {
        ControlarUnicaInstancia();
    }

    //----------------------------------------------------

    private void ControlarUnicaInstancia()
    {
        //Si aun no hay instancia de la Clase
        if (instance == null)
        {
            //Asignamos esta instancia
            instance = this;
        }
        //Si ya hay una instancia
        else
        {
            //Destruimos el GameObject 
            Destroy(gameObject);
        }
    }

    //----------------------------------------------------------------

    void Start()
    {
        //Creamos y agregamos la cantidad de proyectiles especificada a la Lista 
        AddProjectileToPool(poolSize);
    }

    //---------------------------------------------------

    private void AddProjectileToPool(int cantidad)
    {
        //Por cada Item que vaya a tener eN el Pool 
        for (int i = 0; i < cantidad; i++)
        {
            //Generamos el GameObject.
            GameObject @object = Instantiate(prefabObject, this.transform);

            //Lo Desactivamos
            @object.SetActive(false);

            //Añadimos el Prefab instanciado a la lista de Prefabs
            objectsList.Add(@object);

            //El padre de este Objeto será este Pool(Object)
            @object.transform.parent = transform;
        }
    }

    //-----------------------------------------------------

    public GameObject AskForProjectile(Vector3 coordenadasAparicion)
    {
        //Recorremos los Items dentro del Pool
        for (int i = 0; i < poolSize; i++)
        {
            //Verificamos si el Item se encuentra desactivado
            if (objectsList[i].activeSelf == false)
            {
                //Modificamos la Posicion del Objeto
                objectsList[i].transform.position = coordenadasAparicion;

                //Lo activamos
                objectsList[i].SetActive(true);

                //Lo "devolvemos" al Solicitante
                return objectsList[i];
            }
        }

        //Si no hay Objetos disponibles, Creamos 1 más y lo agregamos a la lista
        AddProjectileToPool(1);

        //Incrementamos el valor del PoolSize
        poolSize++;

        //Modificamos las coordenadas del ultimo elemento creado
        objectsList[poolSize - 1].transform.position = coordenadasAparicion;

        //Activamos el último elemento creado
        objectsList[poolSize - 1].SetActive(true);

        //Devolvemos el último Objeto creado
        return objectsList[poolSize - 1];
    }

    //---------------------------------------------------------------------------------------------------------
    public bool VerifyAllDisabled()
    {
        bool todoDesactivado = true;

        //Recorremos todos los Proyectiles del Pool
        for (int i = 0; i < poolSize; i++)
        {
            //Si hay 1 proyectil activado...
            if (objectsList[i].activeSelf == true)
            {
                todoDesactivado = false;
            }
        }

        //Retornamos el resultado de la comprobacion
        return todoDesactivado;
    }
}
