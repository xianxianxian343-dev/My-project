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
    public float speedMove = 0.1f;
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


    [Header("三跑道移动")]
    [Min(0.1f)] public float laneWidth = 2f;
    [Min(0.01f)] public float laneChangeDuration = 0.35f;

    // -1、0、1 分别表示左、中、右跑道，起始位置作为中间跑道。
    private int targetLane = 0;
    private float centerLaneX;
    private float laneChangeStartX;
    private float laneChangeElapsed;

    private Animator animator;

    // 是否正在地面上
    private bool isGrounded;

    // 是否需要执行跳跃
    private bool jumpRequested;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        centerLaneX = go.transform.position.x;
        laneChangeStartX = centerLaneX;
        laneChangeElapsed = laneChangeDuration;
        coinText.text = "金币：" + coinCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            //animator.SetBool("isMoving", false);
            return;
        }


        // 原来的连续左右移动保留作学习对照。
        //moveInput.x = Input.GetAxis("Horizontal");
        moveInput.x = 0f;

        bool leftPressed = Input.GetKeyDown(KeyCode.A);
        bool rightPressed = Input.GetKeyDown(KeyCode.D);
        if (leftPressed != rightPressed)
        {
            ChangeLane(leftPressed ? -1 : 1);
        }
        
        //moveInput.z = Input.GetAxis("Vertical");
        moveInput.z = 0.5f; // 自动向前，速度由 speedMove 控制

        bool isMoving = moveInput.x != 0f || moveInput.z != 0f;
        animator.SetBool("isMoving", isMoving);

        go.transform.Translate(speedMove * moveInput * Time.deltaTime);

        float switchDuration = Mathf.Max(laneChangeDuration, 0.01f);
        laneChangeElapsed = Mathf.Min(laneChangeElapsed + Time.deltaTime, switchDuration);
        float laneProgress = laneChangeElapsed / switchDuration;
        Vector3 lanePosition = go.transform.position;
        lanePosition.x = Mathf.Lerp(laneChangeStartX, centerLaneX + targetLane * laneWidth, laneProgress);
        go.transform.position = lanePosition;

        // 保留原来的手动动画计时作对照；改用 Animator 自身计时，确保返回 run 的过渡完成。
        //animator.SetFloat("laneChangeProgress", laneProgress);
        animator.SetBool("isChangingLane", laneProgress < 1f);

        float mouseX = Input.GetAxis("Mouse X");
        rotateInput.y = mouseX;
        //go.transform.Rotate(speedRotate * rotateInput);

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

        bool isJumping = !isGrounded;//|| jumpRequested || rb.velocity.y > 0.1f;
        animator.SetBool("isJumping", isJumping);



    }

    private void ChangeLane(int direction)
    {
        int nextLane = targetLane + direction;
        if (nextLane < -1 || nextLane > 1)
        {
            return;
        }

        targetLane = nextLane;
        laneChangeStartX = go.transform.position.x;
        laneChangeElapsed = 0f;

        //animator.SetFloat("laneChangeProgress", 0f);
        // 左右状态使用各自的动作时长作为基础速度，再按换道用时调整播放速度。
        animator.SetFloat("laneChangeSpeed", 1f / Mathf.Max(laneChangeDuration, 0.01f));
        animator.SetBool("isChangingLane", true);
        animator.ResetTrigger("changeLeft");
        animator.ResetTrigger("changeRight");
        animator.SetTrigger(direction < 0 ? "changeLeft" : "changeRight");
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


