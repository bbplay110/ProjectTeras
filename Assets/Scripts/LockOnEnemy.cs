using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


public class LockOnEnemy : MonoBehaviour
{
    
    // Use this for initialization

    public float minRange = 0;
    public float maxRange = 100;
    public float rightAngle = 45;
    public float leftAngle = 45;
    public float UpAngle = 45;
    public float DownAngle = 45;
    public GameObject gameManeger;
    public bool Fov_look = false;
    public Color Fov_color = new Color(0, 1, 0, 0.04f);
    private List<Transform> target = new List<Transform>();
    public Dictionary<Transform, float> rank = new Dictionary<Transform, float>();
    public GameObject Arrow=null;
    private Transform get;

    void Start()
    {
        Arrow=Instantiate(Arrow) as GameObject;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        InFov();

    }

    //搜索敌人列表，看有没有敌人在视野范围内 
    void InFov()
    {
        foreach (Transform i in target)
        {
            float distance = (i.position - transform.position).magnitude;
            if (distance >= minRange && distance <= maxRange)
            {
                if (!rank.ContainsKey(i))
                {
                    rank.Add(i, distance);
                }
                else
                {
                    rank[i] = distance;
                }

            }
        }
        get_rank();

    }

    //选择最近的一个
    void get_rank()
    {
        List<float> list_distance = new List<float>();
        List<Transform> list_obj = new List<Transform>();
        float b = 0;
        if (rank.Count > 0)
        {
            foreach (Transform tra1 in rank.Keys)
            {  
                list_distance.Add(rank[tra1]);
            }
            b = list_distance.Min();
            foreach (Transform tra2 in rank.Keys)
            {
                if (rank[tra2] == b)
                {
                    list_obj.Add(tra2);
                }
            }
            get = list_obj[0];
            Debug.Log("get is " + get + "...distance is " + b);
            //Arrow.transform.position =get.Find("ArrowPosition").position;
        }
    }

    void OnDrawGizmos()
    {
        if (Fov_look) DrawFov();
    }

    void DrawFov()
    {
        Handles.color = Fov_color;
        //绘制扇形视野范围，第一个参数圆的中心点，第二参数圆的法线方向，第三个参数扇形开始点，第四个参数扇形的弧度，第五个参数扇形半径
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, -leftAngle, maxRange);
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, rightAngle, maxRange);

        Handles.DrawSolidArc(transform.position, transform.right, transform.forward, -UpAngle, maxRange);
        Handles.DrawSolidArc(transform.position, transform.right, transform.forward, DownAngle, maxRange);

        Handles.color = new Color(1, 0, 0, Fov_color.a);
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, -leftAngle, minRange);
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, rightAngle, minRange);

        Handles.DrawSolidArc(transform.position, transform.right, transform.forward, -UpAngle, minRange);
        Handles.DrawSolidArc(transform.position, transform.right, transform.forward, DownAngle, minRange);

        //绘制扇形的弧线，第一个参数圆的中心点，第二个参数圆的法线，第三个参数弧线起始点，第四个参数弧度，第五个参数圆弧半径
        Handles.color = new Color(Fov_color.r, Fov_color.g, Fov_color.b);
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, -leftAngle, maxRange);
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, rightAngle, maxRange);
        Handles.DrawWireArc(transform.position, transform.right, transform.forward, -UpAngle, maxRange);
        Handles.DrawWireArc(transform.position, transform.right, transform.forward, DownAngle, maxRange);

        Handles.DrawWireArc(transform.position, transform.up, transform.forward, -leftAngle, minRange);
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, rightAngle, minRange);
        Handles.DrawWireArc(transform.position, transform.right, transform.forward, -UpAngle, minRange);
        Handles.DrawWireArc(transform.position, transform.right, transform.forward, DownAngle, minRange);


    }
}
