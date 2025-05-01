using UnityEngine;
using UnityEngine.UI;

public class VidaControler : MonoBehaviour
{
    [Header("Vida")]
    public int vida;
    public int vidaMax;

    [Header("Corações")]
    public Image[] coracao;

    [Header("Sprites")]
    public Sprite cheio;
    public Sprite vazio;

    public bool death;
    PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        VidaLogic();
        Morte();
    
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

        if(vida <= 0){
            Debug.Log("Vida chegou a zero, setando death = true");
player.animator.SetBool("death", true);
            death = true;
            player.animator.SetBool("death", death);
            GetComponent<PlayerController>().enabled = false;
            Destroy(gameObject,4.5f);
        }

    }


}

