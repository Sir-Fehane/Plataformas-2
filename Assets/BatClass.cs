using UnityEngine;

public class BatClass : MonoBehaviour
{
    Rigidbody2D rigidbody;
    int par_impar;
    float time_move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        par_impar = 1;
        time_move = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time_move)
        {
            if (par_impar % 2 == 0)
            {
                rigidbody.linearVelocity = new Vector2(0, 5f);
            }
            else
            {
                rigidbody.linearVelocity = new Vector2(0, -5f);
            }
            par_impar++;
            time_move ++; 
        }
    }
}
