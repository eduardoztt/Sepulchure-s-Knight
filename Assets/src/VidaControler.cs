using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaControler : MonoBehaviour
{
    [Header("Vida")]
    public int vida;
    public int vidaMax;

    [Header("Corações")]
    public Image[] coracao;

    [Header("Sprites dos Corações")]
    public Sprite cheio;
    public Sprite vazio;

    [Header("Tela de Morte")]
    public GameObject TelaMorte; 
    public float fadeSpeed = 0.5f;
    public GameObject TextMorte; 
    public GameObject ButtonTentarDenovo; 
    public GameObject ButtonMenu;

    private Image ImageMorte;

    private float currentFadeAlpha = 0f;
    public bool death;
    PlayerController player;

void Start()
{
    player = GetComponent<PlayerController>();
    if (TelaMorte != null)
    {
        ImageMorte = TelaMorte.GetComponent<Image>(); 
        if (ImageMorte != null)
        {
            Color fadeColor = ImageMorte.color;
            fadeColor.a = 0f;
            ImageMorte.color = fadeColor;
            TelaMorte.SetActive(false); 
        }
        else
        {
            Debug.LogError("O GameObject TelaMorte não possui um componente Image!");
        }
    }
    if (TextMorte != null) TextMorte.SetActive(false);

}

    void Update()
    {
        VidaLogic();
        Morte();
        MostrarTelaDeMorte();
    
    }

    

    void VidaLogic()
    {


        if(vida > vidaMax){
            vida = vidaMax;
        }
        for (int i = 0; i < coracao.Length; i++)
        {
            if( i < vida){

                coracao[i].sprite = cheio;

            }else{

                coracao[i].sprite = vazio;

            }

            if (i < vidaMax){
                coracao[i].enabled = true;
            }else{
                coracao[i].enabled = false;
            }
        }
    }

    void Morte(){

        if(vida <= 0 && !death){
            Debug.Log("Vida chegou a zero, setando death = true");
            player.animator.SetBool("death", true);
            death = true;
            player.animator.SetBool("death", death);

            //PARA N TER KNOCKBACK NA MORTE
            player.KBCount = 0;
            player.isKnock = false;
            player.rb.linearVelocity = new Vector2(0f, player.rb.linearVelocity.y); 


            GetComponent<PlayerController>().enabled = false;

            if (TelaMorte != null) TelaMorte.SetActive(true);
            //Destroy(gameObject,4.5f);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy,2f); 
   
            }
        }

    }

    void MostrarTelaDeMorte(){
        if (death && TelaMorte != null && TelaMorte.activeSelf && ImageMorte != null)
        {
            currentFadeAlpha += fadeSpeed * Time.deltaTime;
            currentFadeAlpha = Mathf.Clamp01(currentFadeAlpha);
            Color fadeColor = ImageMorte.color;
            fadeColor.a = currentFadeAlpha * 0.9f; //Altero o quao preto quero deixar a tela de 0 para transparente ate 1 para full preto
            ImageMorte.color = fadeColor;

            if (currentFadeAlpha >= 1f) 
            {
                if (TextMorte != null) TextMorte.SetActive(true);
                if (ButtonTentarDenovo != null) ButtonTentarDenovo.SetActive(true);
                if (ButtonMenu != null) ButtonMenu.SetActive(true);
            }
        }
    }

    public void RestartGame(){
 
            //Debug.LogError("Chamou a função");
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }

    public void Menu(){
        SceneManager.LoadScene("HomeScene");
    }


}

