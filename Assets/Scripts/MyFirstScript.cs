using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    //声明变量练习
    //public int ATK = 10;
    //public bool isDead = false;
    //public float HPMax = 100.0f;
    //public Vector3 positionNow;
    //public string playerName = "小明";
    // Start is called before the first frame update


    //声明移动速度
    public Vector3 positionTarget;
    public Vector3 rotationTarget;

    public GameObject go;
    void Start()
    {
        Application.targetFrameRate = 60;


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

        //连续移动效果
        go.transform.position = go.transform.position + positionTarget;
        go.transform.eulerAngles = go.transform.eulerAngles + rotationTarget;



    }

    //第一个方法
    //public void MyFirstFun()
    //{
    //    Debug.Log("Hello World!");
    //}

    //Debug.Log("Hello World!");
}
