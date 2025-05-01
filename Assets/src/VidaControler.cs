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

    void Start()
    {
        
    }

    void Update()
    {
        VidaLogic();
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
    vida++;
}
if (Input.GetKeyDown(KeyCode.DownArrow)) {
    vida--;
}

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


}
