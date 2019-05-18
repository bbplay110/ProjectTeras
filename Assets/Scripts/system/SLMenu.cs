using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class SLMenu : MonoBehaviour {
    
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
        manager = GameObject.FindObjectOfType<Manager>();
        /*GameObject eventTriggerList = GameObject.FindGameObjectWithTag("FungusEventTrigger");
        Debug.Log(eventTriggerList);
        for (int i = 0; i <eventTriggerList.transform.childCount; i++)
        {
            eventTrigger.Add(eventTriggerList.transform.GetChild(i).gameObject);
        }*/

        player = GameObject.Find("Player");
    }
    void Start () {

        //Debug.Log(Application.persistentDataPath);
        getSaves();
        switch (SLstatus)
        {
            case saveOrLoad.save:
                //快速存檔
                if (manager.saveGames != null && manager.saveGames[0].saveName == "QuickSave") { 
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = manager.saveGames[0].saveName;
                    QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text = manager.saveGames[0].saveDate;
                    QuickSaveSlot.name = manager.saveGames[0].saveName;
                }
                break;
            case saveOrLoad.load:
                if (manager.saveGames != null&&manager.saveGames[0].saveName=="QuickSave") { 
                QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { realLoad(); });
                QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = manager.saveGames[0].saveName;
                QuickSaveSlot.name = manager.saveGames[0].saveName;
                }
                break;
        }

        //Debug.Log("SaveiN:" + Application.persistentDataPath);

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
	void getSaves()
    {
        if(manager.saveGames != null)
            manager.saveGames.Clear();  
        if(!Directory.Exists(Path.Combine(Application.persistentDataPath, "/saves")))
        {
            Directory.CreateDirectory(Application.persistentDataPath+"/saves");
        }
        //拿到存檔所在的資料夾
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath+"/saves");
        int count = dir.GetFiles().Length;
        FileInfo[] files = dir.GetFiles();

        if (count > 0) {
            //Debug.Log("getFile!");
            for (int i = 0; i < count; i++)
            {

                //讀取json檔案並轉存成文字格式
                StreamReader file = new StreamReader(Application.persistentDataPath+"/saves/"+files[i].Name);
                string loadJson = file.ReadToEnd();
                file.Close();
                //新增一個物件類型為playerState的變數 loadData
                SaveData loadData = new SaveData();
                //使用JsonUtillty的FromJson方法將存文字轉成Json
                loadData = JsonUtility.FromJson<SaveData>(loadJson);
                //如果是快速存檔
                if (loadData.saveName == "QuickSave")
                {
                    manager.saveGames.Insert(0, loadData);

                }
                else
                {
                    manager.saveGames.Add(loadData);
                }
            }
        }
        else if (count == 0 && manager.currentLevel != "Menu")
        {
            manager.saveAction("QuickSave");
        }
        Debug.Log("SaveGamesIs" + count);
    }
	// Update is called once per frame
	void Update () {
        if (hInput.GetButton("Start"))
        {
            gameObject.SetActive(false);
        }
	}
    public void CreateSave()
    {
        manager.saveAction("save" + (Content.transform.childCount - 1));
        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
        Debug.Log(tmpSaveSlot.GetComponent<Button>().onClick);
        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = "save"+(Content.transform.childCount-1).ToString();
        tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text = manager.saveGames[Content.transform.childCount - 1].saveDate;

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
        if (manager.saveGames != null) {
            
            switch (SLstatus)
            {
                case saveOrLoad.save:
                    if (manager.saveGames != null && manager.saveGames[0].saveName == "QuickSave") {
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = manager.saveGames[0].saveName;
                    QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text = manager.saveGames[0].saveDate;
                    QuickSaveSlot.name = manager.saveGames[0].saveName;
                    }
                    for (int i= 1; i < manager.saveGames.Count; i++)
                    {


                        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { replaceSave(); });
                        Debug.Log(tmpSaveSlot.GetComponent<Button>().onClick);
                        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = manager.saveGames[i].saveName;
                        tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text = manager.saveGames[i].saveDate;

                        tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
                        if (Content.transform.childCount > 3)
                        {
                            setSLMenuContent(true);
                        }
                    }
                    break;
                case saveOrLoad.load:
                    if (manager.saveGames != null && manager.saveGames[0].saveName == "QuickSave")
                    {
                        QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                        QuickSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { realLoad(); });
                        QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = manager.saveGames[0].saveName;
                        QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text = manager.saveGames[0].saveDate;
                        QuickSaveSlot.name = manager.saveGames[0].saveName;
                    }
                    for (int i = 1; i < manager.saveGames.Count; i++)
                    {

                        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(delegate() { realLoad(); });
                        Debug.Log(tmpSaveSlot.GetComponent<Button>().onClick);
                        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = manager.saveGames[i].saveName;
                        tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text = manager.saveGames[i].saveDate;
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
        manager.SaveToLoad = selectedSave;
        Debug.Log(selectedSave);
        foreach (var item in manager.saveGames)
        {
            if (item.saveName == selectedSave)
            {
                //transform.parent = null;
                DontDestroyOnLoad(manager.gameObject);
                SceneManager.LoadScene(item.currentLevel);
                SceneManager.sceneLoaded += manager.LoadAction;
            }
        }
    }

    private void OnGUI()
    {
        
    }
    private void OnDisable()
    {
        if (manager.saveGames != null) { 
            SceneManager.sceneLoaded -= manager.LoadAction;
            switch (SLstatus)
            {
                case saveOrLoad.save:
                    for (int i = 0; i < manager.saveGames.Count-1; i++)
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
                    for (int i = 0; i < manager.saveGames.Count-1; i++)
                    {
                        Destroy(Content.transform.GetChild(i).gameObject);
                        if (Content.transform.childCount > 3)
                        {

                            setSLMenuContent(false);

                            //Debug.Log("long--");
                            //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                        }
                    }
                    break;
            }
        }

    }
}
