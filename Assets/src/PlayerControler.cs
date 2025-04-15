using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int characterSpeed = 5;
    [SerializeField] private float jumpForce = 600;

    [SerializeField] private Transform characterFoot;
    [SerializeField] private LayerMask footLayer;

    private bool floorState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && floorState)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        floorState = Physics2D.OverlapCircle(characterFoot.position, 0.2f, footLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * characterSpeed, rb.velocity.y);
    }
}
