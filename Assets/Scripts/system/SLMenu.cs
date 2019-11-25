using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class SLMenu : MonoBehaviour {
    private string saveToLoad;
    public string currentLevel;

    public List<SaveData> saveGames = new List<SaveData>();
    [SerializeField]
    private GameObject player;
    public GameObject QuickSaveSlot;
    public GameObject SaveSlot;
    public GameObject Content;
    private Manager manager;
    [SerializeField]
   // private List<GameObject> eventTrigger = new List<GameObject>();
    private enum saveOrLoad {
        save,
        load
    };
    [SerializeField]
    private saveOrLoad SLstatus;
    // Use this for initialization

    private void Awake()
    {

        currentLevel = SceneManager.GetActiveScene().name;
        player = GameObject.Find("Player");
        manager = GameObject.FindObjectOfType<Manager>();
        /*GameObject eventTriggerList = GameObject.FindGameObjectWithTag("FungusEventTrigger");
        Debug.Log(eventTriggerList);
        for (int i = 0; i <eventTriggerList.transform.childCount; i++)
        {
            eventTrigger.Add(eventTriggerList.transform.GetChild(i).gameObject);
        }*/


    }
    public void quickSave()
    {
        player = GameObject.Find("Player");
        saveAction("QuickSave");
    }
    void getSaves()
    {
        if (saveGames != null)
            saveGames.Clear();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "/saves")))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        //拿到存檔所在的資料夾
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/saves");
        int count = dir.GetFiles().Length;
        FileInfo[] files = dir.GetFiles();

        if (count > 0)
        {
            //Debug.Log("getFile!");
            for (int i = 0; i < count; i++)
            {

                //讀取json檔案並轉存成文字格式
                StreamReader file = new StreamReader(Application.persistentDataPath + "/saves/" + files[i].Name);
                string loadJson = file.ReadToEnd();
                file.Close();
                //新增一個物件類型為playerState的變數 loadData
                SaveData loadData = new SaveData();
                //使用JsonUtillty的FromJson方法將存文字轉成Json
                loadData = JsonUtility.FromJson<SaveData>(loadJson);
                //如果是快速存檔
                if (loadData.saveName == "QuickSave")
                {
                    saveGames.Insert(0, loadData);

                }
                else
                {
                    saveGames.Add(loadData);
                }
            }
        }
        else if (count == 0 && currentLevel != "Menu")
        {
            saveAction("QuickSave");
        }
        Debug.Log("SaveGamesIs" + count);
    }
    public void LoadAction(Scene scene, LoadSceneMode mode)
    {

        GameObject tmpPlayer;
        tmpPlayer = GameObject.Find("Player");
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(Application.persistentDataPath + "/saves/" + saveToLoad);
        string loadJson = file.ReadToEnd();
        file.Close();
        //新增一個物件類型為playerState的變數 loadData
        SaveData loadData = new SaveData();
        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<SaveData>(loadJson);
        //驗證用，將sammaru的位置變更為json內紀錄的位置
        tmpPlayer.transform.position = loadData.position;
        tmpPlayer.GetComponent<hurt>().HP1 = tmpPlayer.GetComponent<hurt>().TotalHP * loadData.playerHealth;
        Manager manager;
        manager =FindObjectOfType<Manager>();
        //調整eventTrigger
        //敵人列表
        List<GameObject> eventTrigger = new List<GameObject>();
        eventTrigger = manager.triggerList;
        for (int i = 0; i < loadData.eventTrigger.Count; i++)
        {
            eventTrigger[i].SetActive(loadData.eventTrigger[i]);
        }
        List<GameObject> enemys = new List<GameObject>();
        enemys = manager.enemyList;
        for (int i = 0; i < loadData.enemys.Count; i++)
        {
            enemys[i].SetActive(loadData.enemys[i]);
        }
        Destroy(gameObject);
    }
    public void quickLoad()
    {
        string selectedSave = "QuickSave";
        saveToLoad = selectedSave;
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
    void Start () {
        getSaves();
        //Debug.Log(Application.persistentDataPath);
        this.enabled = true;
        /*if (QuickSaveSlot == null && transform.Find("QuickSave").gameObject != null)
        {
            QuickSaveSlot = transform.Find("QuickSave").gameObject;
        }

        if (SaveSlot == null && transform.Find("QuickSave").gameObject != null)
        {
            SaveSlot = transform.Find("QuickSave").gameObject;
        }

        if (Content == null && transform.Find("Content").gameObject != null)
        {
            Content = transform.Find("Content").gameObject;
        }*/
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        switch (SLstatus)
        {
            case saveOrLoad.save:
                //快速存檔
                if (saveGames != null && saveGames[0].saveName == "QuickSave"&& QuickSaveSlot!=null) { 
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = saveGames[0].saveName;
                    QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text = saveGames[0].saveDate;
                    QuickSaveSlot.name = saveGames[0].saveName;
                }
                break;
            case saveOrLoad.load:
                if (saveGames != null&&saveGames[0].saveName=="QuickSave") { 
                QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { realLoad(); });
                QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = saveGames[0].saveName;
                QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text = saveGames[0].saveDate;
                QuickSaveSlot.name = saveGames[0].saveName;
                }
                break;
        }

        //Debug.Log("SaveiN:" + Application.persistentDataPath);

    }
    void Update()
    {
        if (hInput.GetButton("Start")&&manager!=null)
        {
            gameObject.SetActive(false);
        }
    }
    void setSLMenuContent(bool state)
    {
                if (state)
                {
                    RectTransform ContentRect;
                    RectTransform SlotRect;
                    ContentRect = Content.GetComponent<RectTransform>();
                    SlotRect = SaveSlot.GetComponent<RectTransform>();
                    ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y + SlotRect.sizeDelta.y);
                }
                else
                {
                    GameObject tmpSaveSlot = SaveSlot;
                    RectTransform ContentRect;
                    RectTransform SlotRect;
                    SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                    ContentRect = Content.GetComponent<RectTransform>();
                    ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y - (SlotRect.sizeDelta.y / 2));
                }
        
    }
	
    public void CreateSave()
    {
        saveAction("save" + (Content.transform.childCount - 1));
        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
        Debug.Log(tmpSaveSlot.GetComponent<Button>().onClick);
        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = "save"+(Content.transform.childCount-2).ToString();
        tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text = saveGames[Content.transform.childCount - 1].saveDate;
        //tmpSaveSlot.transform.Find("Plus").GetComponent<Text>().text ="";

        //tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text =saveGames[Content.transform.childCount - 1].saveDate;
        tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
        if (Content.transform.childCount > 3)
        {
            RectTransform ContentRect;
            RectTransform SlotRect;
            SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
            ContentRect = Content.GetComponent<RectTransform>();
            ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y+SlotRect.sizeDelta.y);
            //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
        }
        //saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
    }
    
    private void OnEnable()
    {
        getSaves();
        // saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
        if (saveGames != null&&QuickSaveSlot!=null) {
            switch (SLstatus)
            {
                case saveOrLoad.save:
                    if (saveGames != null && saveGames[0].saveName == "QuickSave"&& QuickSaveSlot != null) {
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text =saveGames[0].saveName;
                    QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text =saveGames[0].saveDate;
                    QuickSaveSlot.name =saveGames[0].saveName;
                    }
                    for (int i= 1; i <saveGames.Count; i++)
                    {
                        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
                        Debug.Log(tmpSaveSlot.GetComponent<Button>().onClick);
                        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text =saveGames[i].saveName;
                        tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text =saveGames[i].saveDate;
                        //tmpSaveSlot.transform.Find("Plus").GetComponent<Text>().text = "";

                        tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
                        if (Content.transform.childCount > 2)
                        {
                            setSLMenuContent(true);
                        }
                    }
                    break;
                case saveOrLoad.load:
                    if (saveGames != null && saveGames[0].saveName == "QuickSave")
                    {
                        QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                        QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { realLoad(); });
                        QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text =saveGames[0].saveName;
                        QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text =saveGames[0].saveDate;
                        QuickSaveSlot.name =saveGames[0].saveName;
                    }
                    for (int i = 1; i <saveGames.Count; i++)
                    {

                        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { realLoad(); });
                        Debug.Log(tmpSaveSlot.GetComponent<Button>().onClick);
                        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text =saveGames[i].saveName;
                        tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text =saveGames[i].saveDate;
                        tmpSaveSlot.name = "save" + (Content.transform.childCount).ToString();

                        Debug.Log("AddLoadListener");
                        //如果存檔總數大於三就把存檔視窗加長
                        if (Content.transform.childCount > 3)
                        {
                            setSLMenuContent(true);

                            //Debug.Log("long++");
                            //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                        }
                    }
                    break;
            }

        }

    }
    void replaceSave()
    {
    }
   

    

    public void realLoad()
    {
        Debug.Log("work!");
        GameObject text = EventSystem.current.currentSelectedGameObject;
        string selectedSave = text.transform.Find("Number").GetComponentInChildren<Text>().text;
        saveToLoad = selectedSave;
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

    private void OnGUI()
    {
        
    }
    private void OnDisable()
    {
        if (saveGames != null&&QuickSaveSlot!=null) { 
           
            switch (SLstatus)
            {
                case saveOrLoad.save:
                    for (int i = 0; i < saveGames.Count-1; i++)
                    {
                        Destroy(Content.transform.GetChild(i + 1).gameObject);
                        //如果存檔總數大於三就把存檔視窗加長
                        if (Content.transform.childCount > 3)
                        {
                            setSLMenuContent(false);
                        }
                    }
                    break;
                case saveOrLoad.load:
                    for (int i = 0; i < saveGames.Count-1; i++)
                    {
                        Destroy(Content.transform.GetChild(i).gameObject);
                        if (Content.transform.childCount > 2)
                        {

                            setSLMenuContent(false);

                            //Debug.Log("long--");
                            //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                        }
                    }
                    break;
            }
        }
        SceneManager.sceneLoaded -= LoadAction;

    }
}
