using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private int characterSpeed = 5;
    [SerializeField] private float jumpForce = 600;

    [SerializeField] private Transform characterFoot;
    [SerializeField] private LayerMask footLayer;

    private bool floorState;

    private int movingHash = Animator.StringToHash("moving");
    private int jumpingHash = Animator.StringToHash("jumping");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && floorState)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        floorState = Physics2D.OverlapCircle(characterFoot.position, 0.2f, footLayer);

        animator.SetBool(movingHash, horizontalInput != 0);
        animator.SetBool(jumpingHash, !floorState);

        // Optional: Flip sprite
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * characterSpeed, rb.linearVelocity.y);
    }
}
