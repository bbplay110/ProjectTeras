using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Manager : MonoBehaviour {
    public List<GameObject> triggerList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    private List<bool> enemyListReal = new List<bool>();
    public delegate void onSceneLoad();
    public static event onSceneLoad sceneloadAction;
    public GameObject player;
    
    public Flowchart Flowchart;

    public List<bool> EnemyListReal
    {
        get
        {
            return enemyListReal;
        }

        set
        {
            enemyListReal = value;
        }
    }

    private void Awake()
    {
        foreach(var item in enemyList)
        {
            if ((item == null) || (item.activeSelf == false))
            {
                EnemyListReal.Add(false);
            }
            else if (item != null && item.activeSelf)
            {
                EnemyListReal.Add(true);
            }
        }
    }
    public void checkEnemyList()
    {
        for (var item=0;item< enemyList.Count;item++)
        {
            if ((enemyList[item] == null) || (enemyList[item].activeSelf == false))
            {
                EnemyListReal[item]=false;
            }
            else if (enemyList[item] != null && enemyList[item].activeSelf)
            {
                EnemyListReal[item]=true;
            }
        }
    }
    // Use this for initialization
    void Start () {
        //sceneloadAction();
        player= GameObject.Find("Player");
    }

	void Talk(Block block)
    {
        Time.timeScale = 0;
        block.Execute();

    }

	// Update is called once per frame
	void Update () {
		
	}
}
