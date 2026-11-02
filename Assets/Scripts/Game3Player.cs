using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Player : MonoBehaviour
{
    public GameObject go;
    public Vector3 positionTarget;
    public Vector3 rotationTarget;
    
    public float speedMove = 10f;
    public float speedRotate = 3f;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        positionTarget.x = Input.GetAxis("Horizontal");
        positionTarget.z = Input.GetAxis("Vertical");
        go.transform.Translate(speedMove * positionTarget * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        rotationTarget.y = mouseX;
        
        float mouseY = -Input.GetAxis("Mouse Y");
        rotationTarget.x = mouseY;
        go.transform.Rotate(speedRotate * rotationTarget);
    }
}
