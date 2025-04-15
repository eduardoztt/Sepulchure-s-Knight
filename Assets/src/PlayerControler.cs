using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int velocidade = 5;

    [SerializeField] private Transform charactereFeet;
    [SerializeField] private LayerMask groundLayer;

    private bool inTheGround;
    private Animator animator;
    private SpriteRenderer spriteRender;

    private int movingHash = Animator.StringToHash("moving");
    private int jumpingHash = Animator.StringToHash("jumping");
    private int attackHash = Animator.StringToHash("attack");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    } 

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && inTheGround)
        {
            rb.AddForce(Vector2.up * 600);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger(attackHash);
        }

        inTheGround = Physics2D.OverlapCircle(charactereFeet.position, 0.5f, groundLayer);

        animator.SetBool(movingHash, horizontalInput != 0);
        animator.SetBool(jumpingHash, !inTheGround);

        if (horizontalInput > 0)
        {
            spriteRender.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRender.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * velocidade, rb.linearVelocity.y);
    }
}
