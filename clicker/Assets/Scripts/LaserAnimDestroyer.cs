using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimDestroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
