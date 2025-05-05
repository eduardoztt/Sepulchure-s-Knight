using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Outras funcionalidades do seu GameManager podem estar aqui

    public void LoadSceneByName(string sceneName){
        SceneManager.LoadScene(sceneName);
        //Debug.Log("Carregando a cena: " + sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
        //Debug.Log("Carregando a cena com índice: " + sceneIndex);
    }

        public void QuitGame(){
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
        Debug.Log("Saindo do Jogo");
    }

}