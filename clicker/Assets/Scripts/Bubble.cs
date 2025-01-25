using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //Singleton de Burbuja
    public static Bubble Instance;

    [Range(0.005f, 0.1f)] [SerializeField] float escalaCrecimiento;

    public static Action<float> OnActionTriggeredWithFloat;

    [SerializeField] private int vida = 100; 

    //----------------------------------------------------------------

    void Awake()
    {
        Instance = this;

        vida = 100;
    }

    //----------------------------------------------------------------

    public void Grow()
    {
        // Capturamos la escala inicial
        Vector3 currentScale = transform.localScale;

        // Calculamos la nueva escala
        Vector3 newScale = currentScale + new Vector3(escalaCrecimiento, escalaCrecimiento, 1);

        // Calculamos el factor de cambio de la escala
        float scaleChange = currentScale.x / newScale.x;  // Calculamos el factor de cambio

        // Actualizamos la escala del objeto
        // transform.localScale = newScale;

        // Disparamos la acción con el factor de cambio
        Debug.Log(scaleChange);  // Esto debería imprimir > 1 si crece y < 1 si decrece
        OnActionTriggeredWithFloat?.Invoke(scaleChange);
    }





    //----------------------------------------------------------------




}
