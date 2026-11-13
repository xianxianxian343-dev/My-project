using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    public GameObject go;
    public Vector3 moveInput;   // WASD输入
    public Vector3 rotateInput; // 鼠标旋转输入
    public float speedMove = 10f; // 根据你的截图面板，默认改成了 10
    public float speedRotate = 3f;

    [Header("跳跃设置")]
    public float jumpForce = 5f;  // 根据你的截图面板，默认改成了 5
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Rigidbody rb;

    [Header("三跑道移动")]
    [Min(0.1f)] public float laneWidth = 2f;
    [Min(0.01f)] public float laneChangeDuration = 0.35f;

    private int targetLane = 0;
    private float centerLaneX;
    private float laneChangeStartX;
    private float laneChangeElapsed;

    private Animator animator;
    private bool isGrounded;
    private bool jumpRequested;

    void Start()
    {
        // 如果忘记拖 go，默认就是脚本挂着的当前玩家物体
        if (go == null) go = gameObject;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        centerLaneX = go.transform.position.x;
        laneChangeStartX = centerLaneX;
        laneChangeElapsed = laneChangeDuration;
    }

    void Update()
    {
        // 问一下总管：游戏结束了吗？结束了就不跑了
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        moveInput.x = 0f;

        bool leftPressed = Input.GetKeyDown(KeyCode.A);
        bool rightPressed = Input.GetKeyDown(KeyCode.D);
        if (leftPressed != rightPressed)
        {
            ChangeLane(leftPressed ? -1 : 1);
        }

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

        animator.SetBool("isChangingLane", laneProgress < 1f);

        float mouseX = Input.GetAxis("Mouse X");
        rotateInput.y = mouseX;

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

        bool isJumping = !isGrounded;
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

        animator.SetFloat("laneChangeSpeed", 1f / Mathf.Max(laneChangeDuration, 0.01f));
        animator.SetBool("isChangingLane", true);
        animator.ResetTrigger("changeLeft");
        animator.ResetTrigger("changeRight");
        animator.SetTrigger(direction < 0 ? "changeLeft" : "changeRight");
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        // 撞到障碍物，直接向 GameManager 汇报！
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }

        // 吃到金币，销毁金币，并向 GameManager 汇报！
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.AddCoin();
        }
    }
}
