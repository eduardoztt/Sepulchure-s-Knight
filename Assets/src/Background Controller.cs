using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    public Transform Cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
    }

    void Update()
    {
        float distance = Cam.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
