using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public Transform Cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = Cam.position.x * parallaxEffect;
        float movement = Cam.position.x *(1-parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);


        if(movement > startPos + length){

            startPos += length;
            
        }else{
            if(movement < startPos - length){
                startPos -= length;
            }
        }
    }
}
