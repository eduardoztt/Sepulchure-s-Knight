using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spr;

    public void Start() {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        Debug.Log($"Input jogaodor x: {v.x}, y: {v.y}");

        if (v.x < 0) {
            spr.flipX = true;
        } 
        if (v.x > 0){
            spr.flipX = false;
        }


        anim.SetFloat("VelH", Mathf.Abs(v.x));


    }

    public void OnFire()
    {
        Debug.Log("ATIROU");
    }
}