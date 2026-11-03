using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Game1PlayerScript : MonoBehaviour
{
    public GameObject go;
    public Vector3 moveInput;
    public Vector3 rotateInput;
    public float speedMove = 10f;
    public float speedRotate = 3f;
    public AudioSource ad;
    public AudioClip aclip;
    public AudioSource BGM;
    public AudioClip bgm2;


    [Header("跳跃设置")]
    public float jumpForce = 6.5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Rigidbody rb;

    // 是否正在地面上
    private bool isGrounded;

    // 是否需要执行跳跃
    private bool jumpRequested;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");
        go.transform.Translate(speedMove * moveInput * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        rotateInput.y = mouseX;
        go.transform.Rotate(speedRotate * rotateInput);

        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(
                groundCheck.position,
                groundCheckRadius,
                groundLayer,
                QueryTriggerInteraction.Ignore
            );
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }



    }

    private void FixedUpdate()
    {
        if (jumpRequested)
        {
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );

            jumpRequested = false;
        }
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
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            ad.PlayOneShot(aclip, 0.5f);

            //吃到金币后切换背景音乐
            if (BGM.clip.name == "Bgm_ZhanDou")

            {
                BGM.clip = bgm2;

                BGM.Play();
            }
        }
    }







}


