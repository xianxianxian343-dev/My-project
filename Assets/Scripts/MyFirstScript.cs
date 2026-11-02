using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MyFirstScript : MonoBehaviour
{
    //声明变量练习
    //public int ATK = 10;
    //public bool isDead = false;
    //public float HPMax = 100.0f;
    //public Vector3 positionNow;
    //public string playerName = "小明";
    // Start is called before the first frame update


    //声明移动速度和旋转速度
    public Vector3 positionTarget;
    public Vector3 rotationTarget;

    public GameObject go;
    //public GameObject[] gos;
    public BoxCollider box;

    public StudyScript study;

    public Vector3 moveTar;
    public Rigidbody rb;

    public Text text1;




    void Start()
    {
        Application.targetFrameRate = 60;


        //获取其他脚本中的方法和属性,需要一个绑定该脚本的物体对象
        //Debug.Log(go.GetComponent<StudyScript>().number);
        //go.GetComponent<StudyScript>().GetMessage();
        //go.GetComponent<StudyScript>().SendMessage("GetMessage");

        //使用脚本给物体赋值
        //go = GameObject.Find("Cube");  //按物体名找到一个物体
        //go = GameObject.FindWithTag("Go");  //按物体标签找到一个物体
        //gos = GameObject.FindGameObjectsWithTag("Go");  //按物体标签找到物体数组

        //预制体是一个引用，为预制体实例化一个对象
        //quaternion q = Quaternion.Euler(0, 0, 0);
        //GameObject go2 = Instantiate(go, new Vector3(1, 1, 1), q);
        //go2.transform.localScale = new Vector3(1, 1, 2);
        //go2.transform.parent = transform;
        //go2.GetComponent<StudyScript>().number = 5;


        text1.text = "显示文字";

    }

    // Update is called once per frame
    void Update()
    {
        //获取物体的信息
        //Debug.Log("当前游戏物体名称为: " + go.name);
        //Debug.Log("位置为: " + go.transform.position);
        //Debug.Log("旋转为: " + go.transform.rotation.eulerAngles);
        //Debug.Log("缩放为: " + go.transform.localScale);

        ////设置物体的信息
        //go.name = "name1";
        ////go.transform.position = new Vector3(1,2,3);
        //go.transform.position = positionNow;
        //go.transform.eulerAngles = new Vector3(0,90,0);
        //go.transform.localScale = new Vector3(2,2,2);

        //连续移动和旋转效果
        //go.transform.position = go.transform.position + positionTarget;
        //go.transform.eulerAngles = go.transform.eulerAngles + rotationTarget;

        //局部坐标和世界坐标下用Translate和Rotate实现移动和旋转
        //go.transform.Translate(positionTarget * Time.deltaTime);
        //go.transform.Rotate(rotationTarget * Time.deltaTime);
        //go.transform.Translate(positionTarget * Time.deltaTime,Space.World);
        //go.transform.Rotate(rotationTarget * Time.deltaTime, Space.World);


        //销毁物体
        //Destroy(go);



        //指定获取组件方法和通用获取组件方法
        //go.transform.position ;
        //go.GetComponent<BoxCollider>();


        //键盘输入
        //Input.GetKeyDown(KeyCode.A)按键按下
        //Input.GetKeyUp(KeyCode.A)按键抬起
        //Input.GetKey(KeyCode.A)按键持续按下
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    Debug.Log("D键抬起");
        //}


        //鼠标点击输入
        //if (Input.GetKeyDown(KeyCode.Mouse0))  //持续按下Input.GetMouseButton(0)也可
        //{
        //    Debug.Log("鼠标左键按下");
        //}
        //if (Input.GetKeyDown(KeyCode.Mouse1))  //持续按下Input.GetMouseButton(1)也可
        //{
        //    Debug.Log("鼠标右键按下");
        //}

        //键盘轴映射
        //Input.GetAxis("Horizontal");
        //Debug.Log("水平轴A/D,左右方向键" + Input.GetAxisRaw("Horizontal"));
        //Input.GetAxis("Vertical");
        //Debug.Log("水平轴W/S,上下方向键" + Input.GetAxisRaw("Vertical"));


        //鼠标滚轮和移动输入
        //Input.GetAxis("Mouse ScrollWheel");
        //Input.GetAxis("Mouse X");
        //Input.GetAxis("Mouse Y");
        //Debug.Log("鼠标输入为" + Input.GetAxis("Mouse Y"));

        //鼠标位置
        //Debug.Log("鼠标位置为" +  Input.mousePosition);

        //调用含参和无参方法
        //MyFun(go);
        //Debug.Log(MyFun2());
        //Debug.Log(go.name);

        //为物体设置力和速度
        //rb.AddForce(moveTar);
        //rb.velocity = moveTar;



    }

    //第一个方法
    //public void MyFirstFun()
    //{
    //    Debug.Log("Hello World!");
    //}

    //Debug.Log("Hello World!");

    //public void MyFun(GameObject go2)
    //{
    //    Debug.Log(go2.name);


    //}

    //public int MyFun2()
    //{
    //    return 111;


    //}


    public void Button1Click()
    {
        Debug.Log("Button1Click");
    }




}
