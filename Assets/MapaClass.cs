using UnityEngine;
using UnityEngine.SceneManagement;

public class MapaClass : MonoBehaviour
{
    public GameObject flagLevel1, flagLevel2,
    flagLevel3, flagLevel4;
    int current_progress;
    int level_select;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        current_progress = PlayerPrefs.GetInt("current_progress");

        switch (current_progress)
        {
            case 0:
                flagLevel1.SetActive(false);
                flagLevel2.SetActive(false);
                flagLevel3.SetActive(false);
                flagLevel4.SetActive(false);

                level_select = 1;

                transform.position = new Vector2(-5.5f, 1.8f);
            break;
            case 1:
                flagLevel1.SetActive(true);
                flagLevel2.SetActive(false);
                flagLevel3.SetActive(false);
                flagLevel4.SetActive(false);

                level_select = 2;

                transform.position = new Vector2(-2.5f, 1.8f);
            break;

            case 2:
                flagLevel1.SetActive(true);
                flagLevel2.SetActive(true);
                flagLevel3.SetActive(false);
                flagLevel4.SetActive(false);

                level_select = 3;

                transform.position = new Vector2(0.5f, 1.8f);
            break;

            case 3:
                flagLevel1.SetActive(true);
                flagLevel2.SetActive(true);
                flagLevel3.SetActive(true);
                flagLevel4.SetActive(false);

                level_select = 4;

                transform.position = new Vector2(3.5f, 1.8f);
            break;

            case 4:
                flagLevel1.SetActive(true);
                flagLevel2.SetActive(true);
                flagLevel3.SetActive(true);
                flagLevel4.SetActive(true);

                level_select = 4;

                transform.position = new Vector2(3.5f, 1.8f);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (level_select == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //No hacer nada
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)
            && current_progress > 0)
            {
                level_select = 2;

                transform.position = new Vector2(-2.5f, 1.8f);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("enter");
            }
        }

        else if (level_select == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                level_select = 1;

                transform.position = new Vector2(-5.5f, 1.8f);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)
            && current_progress > 1)
            {
                level_select = 3;

                transform.position = new Vector2(0.5f, 1.8f);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("enter");
            }
        }

        else if (level_select == 3)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                level_select = 2;

                transform.position = new Vector2(-2.5f, 1.8f);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)
            && current_progress > 2)
            {
                level_select = 4;

                transform.position = new Vector2(3.5f, 1.8f);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("enter");
            }
        }

        else if (level_select == 4)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                level_select = 3;

                transform.position = new Vector2(0.5f, 1.8f);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //No hacer nada
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("enter");
            }
        }
    }

    public void enterLevel()
    {
        switch(level_select)
        {
            case 1:
                PlayerPrefs.SetInt("level_select", 1);
                SceneManager.LoadScene(2);
            break;
            case 2:
                PlayerPrefs.SetInt("level_select", 2);
                SceneManager.LoadScene(3);
            break;
            case 3:
                PlayerPrefs.SetInt("level_select", 3);
                SceneManager.LoadScene(4);
            break;
            case 4:
                PlayerPrefs.SetInt("level_select", 4);
                SceneManager.LoadScene(5);
            break;
        }
    }
}
