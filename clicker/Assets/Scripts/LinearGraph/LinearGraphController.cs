using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearGraphController : MonoBehaviour
{
    private Vector3 initialPosition = new Vector3(-63.6599998f, 0, 0);

    private Vector3 repeatPosition = new Vector3(-15.5100002f, 0, 0);

    private float speed = 5;

    //--------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.localPosition + (Vector3.right * speed * Time.deltaTime); 
        
        if (transform.localPosition.x >= repeatPosition.x)
        {
            transform.localPosition = initialPosition;
        }
        
    }
}
