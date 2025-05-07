using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed ;
    [SerializeField] private float distance ;
    [SerializeField] private float detectionRadius ;
    [SerializeField] private float attackRange ;
    [SerializeField] private float chaseSpeedMultiplier;
    [SerializeField] private float patrolTime = 3f; 

    private float patrolTimer;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    public Animator animator;
    private bool isChasing = false;
    private bool isAttacking = false;
    private string currentAnimation; 

    public VidaControler coracao;
    public PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        currentAnimation = "idle"; 

        int randomDirection = Random.Range(0, 2); // para deixar random a direção da patrulha do enemy
        if (randomDirection == 0)
        {
            movingRight = false;
            Flip(); 
        }
    }

    void Update()
    {
        if (enemyHealth != null && !enemyHealth.die && playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);


            if (!isAttacking && distanceToPlayer <= attackRange && coracao.vida > 0  )
            {
                //Debug.Log("enemy atacou");
                isAttacking = true;
                isChasing = false;
                animator.Play("attack");
                currentAnimation = "attack";

            }

            else if (isAttacking && distanceToPlayer > attackRange)
            {
                //Debug.Log("Parando ataque");
                isAttacking = false;
                animator.Play("idle"); 
                currentAnimation = "idle";
                isChasing = true; 
            }

            // Se o jogador estiver dentro do raio de detecção, inicia a perseguição
            else if (!isChasing && distanceToPlayer <= detectionRadius)
            {
                isChasing = true;
            }

            // Lógica de perseguição
            if (isChasing && !isAttacking)
            {
                ChasePlayer();
                if (distanceToPlayer > detectionRadius * 1.2f)
                {
                    isChasing = false;
                }
            }
            // Patrulha
            else if (!isChasing && !isAttacking)
            {
                Patrol();
                currentAnimation = "walking"; 
                if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    animator.Play("walking");
                }
            }
            else if (!isChasing && isAttacking && distanceToPlayer > attackRange && currentAnimation == "attack" )
            {
                 animator.Play("idle");
                 currentAnimation = "idle";
                 isAttacking = false;
                 isChasing = true;
            }
        }

    }

    public void ApplyDamageToPlayer(){ // é chamda automaticamente ao adicionar um evento de animação no animation de ataque do enemy


        if (isAttacking && playerController != null)
        {
            Debug.Log("Vida do jogador: " + coracao.vida);
            Debug.Log("Player tomo dano ");
            float direction = (playerTransform.position.x <= transform.position.x) ? 1 : -1;
            playerController.KBCount = playerController.KBTime;
            playerController.isKnock = (direction == 1);
            coracao.vida--;
            playerController.animator.SetTrigger("HitDamage");
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < playerTransform.position.x)
        {
            transform.Translate(Vector2.right * speed * chaseSpeedMultiplier * Time.deltaTime);
            if (!movingRight) Flip();
            movingRight = true;
        }
        else if (transform.position.x > playerTransform.position.x)
        {
            transform.Translate(Vector2.left * speed * chaseSpeedMultiplier * Time.deltaTime);
            if (movingRight) Flip();
            movingRight = false;
        }
        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            animator.Play("walking"); 
            currentAnimation = "walking";
        }
    }

void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolTime)
        {
            movingRight = !movingRight;
            Flip();
            patrolTimer = 0f; // Reseta o timer
        }

        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            animator.Play("walking");
            currentAnimation = "walking";
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected() // mostra os raios visualmente no unity
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}