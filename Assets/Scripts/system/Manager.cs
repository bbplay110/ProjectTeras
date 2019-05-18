using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class Manager : MonoBehaviour {
    public string currentLevel;
    public List<GameObject> triggerList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    private List<bool> enemyListReal = new List<bool>();
    public delegate void onSceneLoad();
    public List<SaveData> saveGames = new List<SaveData>();
    private string saveToLoad;
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

    public string SaveToLoad
    {
        get
        {
            return saveToLoad;
        }

        set
        {
            saveToLoad = value;
        }
    }

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        foreach (var item in enemyList)
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
    public void saveAction(string name)
    {
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "/saves")))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        SaveData tmpSaveData = new SaveData();
        tmpSaveData.saveName = name;
        tmpSaveData.currentLevel = currentLevel;
        tmpSaveData.position = player.transform.position;
        tmpSaveData.saveDate = DateTime.Now.ToString();
        tmpSaveData.playerHealth = player.GetComponent<hurt>().HP1 / player.GetComponent<hurt>().TotalHP;
        //找manager抓敵人跟事件列表
        Manager manager = GameObject.FindObjectOfType<Manager>();
        //抓事件列表起來存
        List<GameObject> eventTrigger = new List<GameObject>();
        eventTrigger = manager.triggerList;
        foreach (var item in eventTrigger)
        {
            tmpSaveData.eventTrigger.Add(item.activeSelf);
        }
        //檢查敵人列表
        manager.checkEnemyList();
        tmpSaveData.enemys = manager.EnemyListReal;


        string saveString = JsonUtility.ToJson(tmpSaveData);
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/saves/" + name);
        file.Write(saveString);
        file.Close();
        saveGames.Add(tmpSaveData);

    }
    public void quickSave()
    {
        player = GameObject.Find("Player");
        saveAction("QuickSave");
    }
    public void quickLoad()
    {
        string selectedSave = "QuickSave";
        SaveToLoad = selectedSave;
        Debug.Log(selectedSave);
        foreach (var item in saveGames)
        {
            if (item.saveName == selectedSave)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene(item.currentLevel);
                SceneManager.sceneLoaded += LoadAction;
            }
        }
    }
    public void LoadAction(Scene scene, LoadSceneMode mode)
    {
        GameObject tmpPlayer;
        tmpPlayer = GameObject.Find("Player");
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(Application.persistentDataPath + "/saves/" + SaveToLoad);
        string loadJson = file.ReadToEnd();
        file.Close();
        //新增一個物件類型為playerState的變數 loadData
        SaveData loadData = new SaveData();
        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<SaveData>(loadJson);
        //驗證用，將sammaru的位置變更為json內紀錄的位置
        tmpPlayer.transform.position = loadData.position;
        tmpPlayer.GetComponent<hurt>().HP1 = tmpPlayer.GetComponent<hurt>().TotalHP * loadData.playerHealth;
        //調整eventTrigger

        //敵人列表
        List<GameObject> eventTrigger = new List<GameObject>();
        eventTrigger = triggerList;
        for (int i = 0; i < loadData.eventTrigger.Count; i++)
        {
            eventTrigger[i].SetActive(loadData.eventTrigger[i]);
        }
        List<GameObject> enemys = new List<GameObject>();
        enemys = enemyList;
        for (int i = 0; i < loadData.enemys.Count; i++)
        {
            enemys[i].SetActive(loadData.enemys[i]);
        }
        Destroy(gameObject);
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
