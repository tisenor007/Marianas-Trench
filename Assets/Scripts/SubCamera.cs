using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    public GameObject sub;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, sub.transform.position.y, transform.position.z);
    }
}
