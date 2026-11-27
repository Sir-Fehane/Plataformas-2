using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverClass : MonoBehaviour
{
    public void BotonContinue()
    {
        PlayerPrefs.SetInt("player_lives", 5);
        SceneManager.LoadScene(1);
    }
    public void BotonTitulo()
    {
        PlayerPrefs.SetInt("player_lives", 5);
        SceneManager.LoadScene(0);
    }
}
