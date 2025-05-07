using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 3f;


    private Vector3 startPos;
    private bool movingRight = true;
    public VidaControler coracao;
    public PlayerController player;
    public Animator animator;
    private EnemyHealth enemyHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {

        if (enemyHealth != null && !enemyHealth.die){
            if (movingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (transform.position.x >= startPos.x + distance)
                {
                    movingRight = false;
                    Flip();
                }
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (transform.position.x <= startPos.x - distance)
                {
                    movingRight = true;
                    Flip();
                }
            }
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverte o sprite
        transform.localScale = scale;
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "Player")
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            float direction = (collision.transform.position.x <= transform.position.x) ? 1 : -1;
            playerController.KBCount = playerController.KBTime;
            playerController.isKnock = (direction == 1); 
           
        }

        coracao.vida--;
        player.animator.SetTrigger("HitDamage");
    }
}
}
