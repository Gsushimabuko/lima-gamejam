using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Bubble.OnActionTriggeredWithFloat += HandleActionWithFloat;
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale * GameManager.Instance.globalSize;
    }

    private void OnEnable()
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale * GameManager.Instance.globalSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleActionWithFloat(float value)
    {
        Vector3 currentScale = transform.localScale;

        transform.localScale = currentScale * value;
    }
}
