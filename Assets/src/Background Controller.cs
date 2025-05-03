using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public Transform Cam = null;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate(){

         if (Cam != null){
            float temp = Cam.transform.position.x *(1 - parallaxEffect);
            float distance = Cam.transform.position.x * parallaxEffect;

            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);


        if (temp > startPos + length)
            {
                startPos += length;
            }
            
            else if (temp < startPos - length)
            {
                startPos -= length;
            }
        }

    }
}
