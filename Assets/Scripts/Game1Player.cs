using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Game1PlayerScript : MonoBehaviour
{
    public GameObject go;
    public Vector3 positionTarget;
    public Vector3 rotationTarget;
    public float speedMove = 10f;
    public float speedRotate = 3f;
    public AudioSource ad;
    public AudioClip aclip;
    public AudioSource BGM;
    public AudioClip bgm2;




    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        positionTarget.x = Input.GetAxis("Horizontal");
        positionTarget.z = Input.GetAxis("Vertical");
        go.transform.Translate(speedMove * positionTarget * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        rotationTarget.y = mouseX;
        go.transform.Rotate(speedRotate * rotationTarget);

    }

    //碰撞打印物体名字
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}

    //触发打印物体名
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("触发" + other.gameObject.name);
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("持续触发" + other.gameObject.name);
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("离开触发" + other.gameObject.name);
    //}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            ad.PlayOneShot(aclip,0.5f);

            //吃到金币后切换背景音乐
            if (BGM.clip.name == "Bgm_ZhanDou")

            {
                BGM.clip = bgm2;

                BGM.Play();
            }
        }
    }







}
