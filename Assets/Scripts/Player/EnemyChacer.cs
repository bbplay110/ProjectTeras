using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyChacer : MonoBehaviour {
   private float m_uptimer = 0;
    private float m_currLife = 0;
    private float m_RotSpeed = 10;

    public float Range = 20;
    public float Speed =100;
    public float LifeTime = 5;
    public GameObject Explosion;
    private bool trace = false;
    public Dictionary<Transform,float> canTrack= new Dictionary<Transform,float>();
    public List<GameObject> Enemys = new List<GameObject>();
    int shootableMask;
	// Use this for initialization
	void Start () {
        shootableMask = LayerMask.NameToLayer("Enemy");
        Enemys.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Enemys.Add(GameObject.FindGameObjectWithTag("Boss"));
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!trace)
        {
            moveForward(null);
            Debug.Log("Get is null ");
        }
        else
        {

        }
        foreach (GameObject target in Enemys)
        {

            RaycastHit hit;
            Ray DistanceCount = new Ray();
            DistanceCount.origin = transform.position;
            DistanceCount.direction = target.transform.position - transform.position;
            Debug.DrawLine(transform.position, target.transform.position, Color.blue, Range);
            if(Physics.Raycast(DistanceCount,out hit,Range,shootableMask))
            {
                trace = true;
                Debug.Log("hit!");
                float Dis=Vector3.Distance(transform.position, hit.transform.position);
                if (!canTrack.ContainsKey(target.transform))
                {
                    canTrack.Add(target.transform, Dis);
                }
                else
                {
                    canTrack[target.transform] = Dis;
                }
                Debug.Log("canTrackList:"+target.transform+","+Dis);
                get_rank();
            }
        };

	}
    void get_rank()
    {
        List<float> list_distance = new List<float>();
        List<Transform> list_obj = new List<Transform>();
        float b = 0;
        if (canTrack.Count > 0)
        {
            foreach (Transform tra1 in canTrack.Keys)
            {
                list_distance.Add(canTrack[tra1]);
            }
            b = list_distance.Min();
            foreach (Transform tra2 in canTrack.Keys)
            {
                if (canTrack[tra2] == b)
                {
                    list_obj.Add(tra2);
                }
            }
            moveForward(list_obj[0]);
            Debug.Log("Get is " + list_obj[0]);
            //Arrow.transform.position =get.Find("ArrowPosition").position;
        }
    }
    private void moveForward(Transform m_Target)
    {


        // 为更加逼真，0.5秒前只前进和减速不进行追踪
        if (m_uptimer < 0.5f)
        {
            m_uptimer += Time.deltaTime;
            Speed -= 2 * Time.deltaTime;
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        else if (m_Target == null && (m_uptimer >= 0.5f))
        {
            Speed += 2 * Time.deltaTime;
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        else if(m_Target != null && m_uptimer >= 0.5f)
        {
            // 开始追踪敌人
            Vector3 target = (m_Target.position - transform.position).normalized;
            float a = Vector3.Angle(transform.forward, target) / m_RotSpeed;
            if (a > 0.1f || a < -0.1f)
                transform.forward = Vector3.Slerp(transform.forward, target, Time.deltaTime / a).normalized;
            else
            {
                Speed += 2 * Time.deltaTime;
                transform.forward = Vector3.Slerp(transform.forward, target, 1).normalized;
            }

            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        m_currLife += Time.deltaTime;
        if (m_currLife > LifeTime)
        {
            // 超过生命周期爆炸（不同与击中敌人）
            Destroy(gameObject);
            Destroy(Instantiate(Explosion, transform.position, Quaternion.identity), 1.2f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
