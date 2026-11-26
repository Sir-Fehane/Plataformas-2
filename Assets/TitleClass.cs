using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleClass : MonoBehaviour
{
    public void botonNewGame()
    {
        PlayerPrefs.SetInt("current_progress", 0);
        PlayerPrefs.SetInt("player_lives", 5);
        SceneManager.LoadScene(1);
    }

    public void botonContinue()
    {
        if (PlayerPrefs.HasKey("current_progress"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayerPrefs.SetInt("current_progress", 0);
            SceneManager.LoadScene(1);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
