using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float Leftpower = 3000f;
    public float Rightpower = 60f;
    public float raidV = 500f;
    public float raid = 10f;
    private static Vector3 pos;
    private static bool Leftdoit, Rightdoit, RightOtherdoit;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;             //获取鼠标位置用的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                   //鼠标射线在屏幕中的坐标
        //进行各种条件的判断
        if (Input.GetMouseButtonDown(0))                //当左键摁下
        {
            Leftdoit = true;
        }
        if (Input.GetMouseButtonUp(0))              //当左键抬起来
        {
            Leftdoit = false;
        }
        if (Rightdoit && Input.GetKey(KeyCode.Space))             //如果空格是摁下的且右键摁着的时候（反转）
        {
            Rightdoit = false;
            RightOtherdoit = true;
        }
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.Space))             //当右键摁下并且空格摁下的时候（反转）
        {
            Rightdoit = false;
            RightOtherdoit = true;
        }
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space))             //当右键抬起或者空格抬起的时候好（抬起其中一个）
        {
            Rightdoit = false;
            RightOtherdoit = false;
        }
        if (Input.GetMouseButton(1) && RightOtherdoit == false)                  //当右键摁着且没有空格摁着的时候（没有反转并且右键摁着的时候）
        {
            Rightdoit = true;
        }
        if (Input.GetMouseButtonUp(1) || RightOtherdoit != false)              //当油价an抬起来或者摁着空格的时候
        {
            Rightdoit = false;
        }





        if (Physics.Raycast(ray, out hitInfo, 100))             //
        {
            if (hitInfo.transform.tag == "Plane" && Leftdoit)
            {
                hitInfo.point = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                Collider[] colliders = Physics.OverlapSphere(hitInfo.point, 60000f);                //更改想外退的波的作用半径
                foreach (Collider hits in colliders)  //遍历碰撞器数组
                {
                    //如果这个物体有刚体组件
                    if (hits.GetComponent<Rigidbody>())
                    {
                        hits.GetComponent<Rigidbody>().isKinematic = true;
                        hits.GetComponent<Rigidbody>().isKinematic = false;
                        pos = hitInfo.point;
                        Vector3 dir = (hits.transform.position - pos);
                        hits.GetComponent<Rigidbody>().AddForce(dir * Leftpower * 1 / (dir.magnitude * dir.magnitude));
                    }
                }
            }
            else if (hitInfo.transform.tag == "Plane" && Rightdoit)
            {
                hitInfo.point = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                Collider[] colliders = Physics.OverlapSphere(hitInfo.point, raid);
                foreach (Collider hits in colliders)  //遍历碰撞器数组
                {
                    //如果这个物体有刚体组件
                    if (hits.GetComponent<Rigidbody>())
                    {
                        hits.GetComponent<Rigidbody>().isKinematic = true;
                        hits.GetComponent<Rigidbody>().isKinematic = false;
                        Vector3 fp = hitInfo.point - hits.transform.position;//向心力矢量，但此时向量模不正确 
                        Vector3 dir = -fp;
                        Quaternion rot = Quaternion.Euler(0f, -90f, 0f);
                        fp = rot * fp;
                        fp.y = 0;
                        fp = fp.normalized;
                        hits.GetComponent<Rigidbody>().AddForce(fp * raidV);
                        hits.GetComponent<Rigidbody>().AddForce(-dir * Rightpower);
                    }
                }
            }
            else if (hitInfo.transform.tag == "Plane" && RightOtherdoit)
            {
                Invoke("stop", 2f);
                hitInfo.point = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                Collider[] colliders = Physics.OverlapSphere(hitInfo.point, raid);
                foreach (Collider hits in colliders)  //遍历碰撞器数组
                {
                    //如果这个物体有刚体组件
                    if (hits.GetComponent<Rigidbody>())
                    {
                        hits.GetComponent<Rigidbody>().isKinematic = true;
                        hits.GetComponent<Rigidbody>().isKinematic = false;
                        Vector3 fp = hitInfo.point - hits.transform.position;//向心力矢量，但此时向量模不正确  
                        Vector3 dir = -fp;
                        Quaternion rot = Quaternion.Euler(0f, 90f, 0f);
                        fp = rot * fp;
                        fp.y = 0;
                        fp = fp.normalized;
                        hits.GetComponent<Rigidbody>().AddForce(fp * raidV);
                        hits.GetComponent<Rigidbody>().AddForce(-dir * Rightpower);
                    }
                }
            }
        }
    }
}   