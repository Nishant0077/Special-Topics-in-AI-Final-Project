using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour {

    public float speed = 1;

    void Update()
    {
        

        //transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
    }
}
