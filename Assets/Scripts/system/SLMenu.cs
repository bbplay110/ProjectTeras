using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class SLMenu : MonoBehaviour {
    public string currentLevel;
    public GameObject QuickSaveSlot;
    public GameObject SaveSlot;
    public GameObject Content;
    public SaveLoadUtility slu;

    public List<string> saveList = new List<string>();
    [SerializeField]
    private enum saveOrLoad {
        save,
        load
    };
    [SerializeField]
    private string SaveToLoad;
    private saveOrLoad SLstatus; 
    public List<SaveData> saveGames;
	// Use this for initialization

	void Start () {
        switch (SLstatus)
        {
            case saveOrLoad.save:

                QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                QuickSaveSlot.GetComponent<Button>().onClick.AddListener(replaceSave);
                QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = slu.quickSaveName;
                QuickSaveSlot.transform.Find("Date").GetComponent<Text>().text=
                QuickSaveSlot.name = slu.quickSaveName;
                break;
            case saveOrLoad.load:

                QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                QuickSaveSlot.GetComponent<Button>().onClick.AddListener(realLoad);
                QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = slu.quickSaveName;
                QuickSaveSlot.name = slu.quickSaveName;
                break;
        }

        Debug.Log("SaveiN:" + Application.persistentDataPath);
        currentLevel = SceneManager.GetActiveScene().name;
        if (slu == null && (transform.parent.GetComponent<SaveLoadUtility>()!=null))
        {
            
            slu = transform.parent.GetComponent<SaveLoadUtility>();
        }
        getSaves();

        saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
        slu.SaveGame(slu.quickSaveName);
    }
	void getSaves()
    {
        //拿到存檔所在的資料夾
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        int count = dir.GetFiles().Length;
        FileInfo[] files = dir.GetFiles();
        for (int i = 0; i < count; i++)
        {
            //讀取json檔案並轉存成文字格式
            StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath,files[i].Name));
            string loadJson = file.ReadToEnd();
            file.Close();
            //新增一個物件類型為playerState的變數 loadData
            SaveData loadData = new SaveData();
            //使用JsonUtillty的FromJson方法將存文字轉成Json
            loadData = JsonUtility.FromJson<SaveData>(loadJson);
            saveGames.Add(loadData);
        }
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
        GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
        tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
        tmpSaveSlot.GetComponent<Button>().onClick.AddListener(realSave);
        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = "save"+(Content.transform.childCount-1).ToString();

        //tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text =saveGames[Content.transform.childCount - 1].saveDate;
        tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
        //saveList.Add(currentLevel);
        save();
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
    void save()
    {
        SaveData tmpSaveData = new SaveData();
        tmpSaveData.currentLevel = currentLevel;
        tmpSaveData.position = GameObject.Find("Player").transform.position;

        string saveString = JsonUtility.ToJson(tmpSaveData);
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "myPlayer"));
        file.Write(saveString);
        file.Close();

    }
    private void OnEnable()
    {
       // saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
        switch (SLstatus)
        {
            case saveOrLoad.save:
                for (int i = 1; i < saveGames.Count; i++)
                {
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(replaceSave);
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = slu.quickSaveName;
                    QuickSaveSlot.name = slu.quickSaveName;
                    GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                    tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    tmpSaveSlot.GetComponent<Button>().onClick.AddListener(replaceSave);
                    tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text =saveGames[i].savegameName;
                    tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text = saveGames[i].saveDate;
                    tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
                    if (Content.transform.childCount > 3)
                    {
                        RectTransform ContentRect;
                        RectTransform SlotRect;
                        ContentRect = Content.GetComponent<RectTransform>();
                        SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y + SlotRect.sizeDelta.y);
                        Debug.Log("long++");
                        //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                    }
                }
                break;
            case saveOrLoad.load:
                for (int i = 1; i < saveGames.Count; i++)
                {
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(realLoad);
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = slu.quickSaveName;
                    QuickSaveSlot.name = slu.quickSaveName;
                    GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                    tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    tmpSaveSlot.GetComponent<Button>().onClick.AddListener(realLoad);
                    tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = saveGames[i].savegameName;
                    tmpSaveSlot.transform.Find("Date").GetComponent<Text>().text = saveGames[i].saveDate;
                    tmpSaveSlot.name = "save" + (Content.transform.childCount).ToString();
                    if (Content.transform.childCount > 3)
                    {
                        RectTransform ContentRect;
                        RectTransform SlotRect;
                        SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                        ContentRect = Content.GetComponent<RectTransform>();
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y + SlotRect.sizeDelta.y);

                        Debug.Log("long++");
                        //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                    }
                }
                break;
        }
       
        

    }
    void replaceSave()
    {
    }
    void realLoad()
    {
        GameObject text = EventSystem.current.currentSelectedGameObject;
        string selectedSave = text.transform.Find("Number").GetComponentInChildren<Text>().text;
        SaveToLoad = selectedSave;
        Debug.Log(selectedSave);
        foreach (var item in saveGames)
        {
            if (item.savegameName == selectedSave)
            {
                DontDestroyOnLoad(slu.gameObject);
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene(item.scene);
                SceneManager.sceneLoaded += LoadAction;
            }
        }
    }
    void LoadAction(Scene scene, LoadSceneMode mode)
    {
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, SaveToLoad));
        string loadJson = file.ReadToEnd();
        file.Close();
        
        //新增一個物件類型為playerState的變數 loadData
        SaveData loadData = new SaveData();
        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<SaveData>(loadJson);
        //驗證用，將sammaru的位置變更為json內紀錄的位置
        GameObject.Find("sammaru").transform.position = loadData.position;

        Destroy(slu.gameObject);
        Destroy(gameObject);
    }
    private void OnGUI()
    {
        
    }
    private void OnDisable()
    {

        SceneManager.sceneLoaded -= LoadAction;
        switch (SLstatus)
        {
            case saveOrLoad.save:
                for (int i = 0; i < saveGames.Count-1; i++)
                {
                    Destroy(Content.transform.GetChild(i + 1).gameObject);
                    if (Content.transform.childCount > 3)
                    {
                        GameObject tmpSaveSlot = SaveSlot;
                        RectTransform ContentRect;
                        RectTransform SlotRect;
                        SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                        ContentRect = Content.GetComponent<RectTransform>();
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y - (SlotRect.sizeDelta.y/2));

                        Debug.Log("long--");
                        //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                    }
                }
                break;
            case saveOrLoad.load:
                for (int i = 0; i < saveGames.Count-1; i++)
                {
                    Destroy(Content.transform.GetChild(i).gameObject);
                    if (Content.transform.childCount > 3)
                    {
                       
                        GameObject tmpSaveSlot = SaveSlot;
                        RectTransform ContentRect;
                        RectTransform SlotRect;
                        SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                        ContentRect = Content.GetComponent<RectTransform>();
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y - (SlotRect.sizeDelta.y/2));

                        Debug.Log("long--");
                        //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                    }
                }
                break;
        }
        
    }
}
