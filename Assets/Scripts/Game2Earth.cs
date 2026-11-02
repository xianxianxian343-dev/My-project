using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Earth : MonoBehaviour
{

    public GameObject go;
    public GameObject go2;
    public Vector3 rotationTarget;
    public Vector3 rotationTarget2;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        go.transform.Rotate(rotationTarget * Time.deltaTime);
        go2.transform.Rotate(rotationTarget2 * Time.deltaTime);
    }
}
