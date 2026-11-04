using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game1PlayerScript : MonoBehaviour
{
    public GameObject go;
    public Vector3 moveInput;//WASD输入
    public Vector3 rotateInput;//鼠标选转输入
    public float speedMove = 10f;
    public float speedRotate = 3f;
    public AudioSource ad;//控制音响
    public AudioClip aclip;//音源
    public AudioSource BGM;
    public AudioClip bgm2;
    public Text coinText; // 用来连接显示金币数量的文字
    private int coinCount = 0; // 当前吃到的金币数量
    public GameObject gameOverUI; // 用来连接“游戏结束”文字物体
    private bool isGameOver = false; // 游戏是否已经结束



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
        coinText.text = "金币：" + coinCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }


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
        if (isGameOver)
        {
            return;
        }


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


    //等待两秒并且重新加载场景
    private IEnumerator RestartScene()
    {
        // 等待2秒
        yield return new WaitForSeconds(2f);

        // 取得当前场景的编号
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 重新加载这个场景
        SceneManager.LoadScene(sceneIndex);
    }




    private void OnTriggerEnter(Collider other)
    {

        if (isGameOver)
        {
            return;
        }


        if (other.tag == "Obstacle")
        {
            isGameOver = true;
            Debug.Log("碰到障碍物了");
            gameOverUI.SetActive(true);
            StartCoroutine(RestartScene());
        }




        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            ad.PlayOneShot(aclip, 0.5f);
            coinCount++;
            coinText.text = "金币：" + coinCount;
            //吃到金币后切换背景音乐
            if (BGM.clip.name == "Bgm_ZhanDou")

            {
                BGM.clip = bgm2;

                BGM.Play();
            }
        }
    }







}


