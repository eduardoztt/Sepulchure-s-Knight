using UnityEngine;

public class TokenVida : MonoBehaviour
{
    public float distanciaInteracao = 2f; // Distância para o jogador interagir
    public GameObject teclaInteracaoSprite;
    public Transform pontoTecla;
    public GameObject particulasCuraPrefab; // Prefab do sistema de partículas de cura
    private Transform jogadorTransform;
    private VidaControler coracao;
    private bool podeInteragir = false;
    private bool jaUsado = false;
    private ParticleSystem particulasCuraInstancia;

    void Start()
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null)
        {
            jogadorTransform = jogador.transform;
            coracao = jogador.GetComponent<VidaControler>();
        }

        if (teclaInteracaoSprite != null)
        {
            teclaInteracaoSprite.SetActive(false); // Desativa o sprite da tecla no início
        }
        else
        {
            Debug.LogError("Sprite da tecla de interação não atribuído ao objeto " + gameObject.name);
        }
    }

    void Update()
    {
        if (jaUsado)
        {
            if (teclaInteracaoSprite != null)
            {
                teclaInteracaoSprite.SetActive(false); // Garante que a tecla não reapareça
            }
            return; // Se já foi usado, não precisa verificar a interação novamente
        }

        if (jogadorTransform != null && coracao != null && coracao.vida < coracao.vidaMax)
        {
            float distancia = Vector2.Distance(transform.position, jogadorTransform.position);

            if (distancia <= distanciaInteracao)
            {
                podeInteragir = true;
                if (teclaInteracaoSprite != null)
                {
                    teclaInteracaoSprite.SetActive(true);
                    // Atualiza a posição da tecla para o ponto de ancoragem
                    if (pontoTecla != null)
                    {
                        teclaInteracaoSprite.transform.position = pontoTecla.position;
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    coracao.vida++;
                    jaUsado = true;
                    if (teclaInteracaoSprite != null)
                    {
                        teclaInteracaoSprite.SetActive(false);
                    }
                    // Instancia o sistema de partículas e obtém a referência
                    if (particulasCuraPrefab != null)
                    {
                        GameObject tempParticulas = Instantiate(particulasCuraPrefab, transform.position, Quaternion.identity);
                        particulasCuraInstancia = tempParticulas.GetComponent<ParticleSystem>();
                        // Inicia a emissão das partículas
                        if (particulasCuraInstancia != null)
                        {
                            particulasCuraInstancia.Play();
                        }
                    }
                }
            }
            else
            {
                podeInteragir = false;
                if (teclaInteracaoSprite != null)
                {
                    teclaInteracaoSprite.SetActive(false);
                }
            }
        }
        else
        {
            podeInteragir = false;
            if (teclaInteracaoSprite != null)
            {
                teclaInteracaoSprite.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaInteracao);
    }
}