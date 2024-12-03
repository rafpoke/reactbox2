using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Função para carregar uma cena
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Função para alternar entre duas cenas
    public void SwitchScenes()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
}