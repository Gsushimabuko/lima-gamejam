using UnityEngine;

public class LaserAnimDestroyer : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.instance.PlaySfx("LaserShot");
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
