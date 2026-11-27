using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaClass : MonoBehaviour
{
    public void meta()
    {
        int current_progress = PlayerPrefs.GetInt("current_progress");
        int level_select = PlayerPrefs.GetInt("level_select");

        if (level_select == 1 && current_progress < 1)
        {
            PlayerPrefs.SetInt("current_progress", 1);
        }
        else if (level_select == 2 && current_progress < 2)
        {
            PlayerPrefs.SetInt("current_progress", 2);
        }
        else if (level_select == 3 && current_progress < 3)
        {
            PlayerPrefs.SetInt("current_progress", 3);
        }
        else if (level_select == 4 && current_progress < 4)
        {
            PlayerPrefs.SetInt("current_progress", 4);
        }

        if (level_select == 1)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
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
