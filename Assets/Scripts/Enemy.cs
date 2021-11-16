using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public enum State
    {
        roam,
        chasing,
        attacking,
        retreating
    }
    
    public float minY;
    public float maxY;

    public float randomY;
    protected bool right = true;
    protected GameObject target;
    protected float targetDistance;
    protected State state;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Roam()
    {
        if (right)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x > (Screen.width / 100) + 2)
            {
                right = false;
            }
        }

        if (right == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x < -(Screen.width / 100) - 2)
            {
                right = true;
            }
        }
    }

   


}
