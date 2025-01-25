using UnityEngine;

public class Bubble : MonoBehaviour
{
    //Singleton de Burbuja
    public static Bubble Instance;

    [Range(0.005f, 0.1f)] [SerializeField] float escalaCrecimiento;

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
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale + new Vector3 (escalaCrecimiento, escalaCrecimiento, 1);
    }



    //----------------------------------------------------------------

    

  
}
