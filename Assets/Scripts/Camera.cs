using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Submarine submarineSript;
    public GameObject sub;
    public GameObject camera;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, sub.transform.position.y, transform.position.z);
    }
}
