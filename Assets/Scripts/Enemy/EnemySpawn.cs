using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public bool Loop=false;

    public float SpawnTime;
    public bool DontSpawnOnStart=false;
    public int MaxSpawnScequnce=1;

    public GameObject[] Enemy;
    public int enemyAmount = 5;
    private float range;
    private Vector2 xz;

    // Use this for initialization
    void Start()
    {
        //Enemy_base Dummy = new Enemy_base(100,20,20,10);
        if (!DontSpawnOnStart)
            Spawn();
        else if (Loop)
            InvokeRepeating("Spawn", 0, SpawnTime);

        GetComponent<MeshRenderer>().enabled = false;
        range = gameObject.transform.localScale.x;

        
    }
    public void Spawn()
    {
        
        if (MaxSpawnScequnce > 0) { 
            for (int i = 1; i <= enemyAmount; i++)
            {
                xz = Random.insideUnitCircle * range;
                GameObject _Perfab = Instantiate(Enemy[Random.Range(0, Enemy.Length)], new Vector3(xz.x + transform.position.x, 0 + transform.position.y, xz.y + transform.position.z), new Quaternion(0, 0, 0, 0)) as GameObject;

            }
        }
        if (!Loop)
            MaxSpawnScequnce -= 1;
    }
    
}


