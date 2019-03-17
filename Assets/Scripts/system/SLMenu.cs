using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    private saveOrLoad SLstatus; 
    public List<SaveGame> saveGames;
	// Use this for initialization

	void Start () {
        switch (SLstatus)
        {
            case saveOrLoad.save:

                QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                QuickSaveSlot.GetComponent<Button>().onClick.AddListener(realSave);
                QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = slu.quickSaveName;
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
        saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
        slu.SaveGame(slu.quickSaveName);
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
        tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = (Content.transform.childCount-1).ToString();
        tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
        //saveList.Add(currentLevel);
        slu.SaveGame(tmpSaveSlot.name);
        if (Content.transform.childCount > 3)
        {
            RectTransform ContentRect;
            RectTransform SlotRect;
            SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
            ContentRect = Content.GetComponent<RectTransform>();
            ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y+SlotRect.sizeDelta.y);
            //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
        }
        saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
    }
    private void OnEnable()
    {
        saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
        switch (SLstatus)
        {
            case saveOrLoad.save:
                for (int i = 1; i < saveGames.Count; i++)
                {
                    QuickSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    QuickSaveSlot.GetComponent<Button>().onClick.AddListener(realSave);
                    QuickSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text = slu.quickSaveName;
                    QuickSaveSlot.name = slu.quickSaveName;
                    GameObject tmpSaveSlot = Instantiate(SaveSlot, Content.transform);
                    tmpSaveSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    tmpSaveSlot.GetComponent<Button>().onClick.AddListener(realSave);
                    tmpSaveSlot.transform.Find("Number").GetComponentInChildren<Text>().text =saveGames[i].savegameName;
                    tmpSaveSlot.name = "save" + (Content.transform.childCount - 1).ToString();
                    if (Content.transform.childCount > 3)
                    {
                        RectTransform ContentRect;
                        RectTransform SlotRect;
                        ContentRect = Content.GetComponent<RectTransform>();
                        SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y + SlotRect.sizeDelta.y);
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
                    tmpSaveSlot.name = "save" + (Content.transform.childCount).ToString();
                    if (Content.transform.childCount > 3)
                    {
                        RectTransform ContentRect;
                        RectTransform SlotRect;
                        SlotRect = tmpSaveSlot.GetComponent<RectTransform>();
                        ContentRect = Content.GetComponent<RectTransform>();
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y + SlotRect.sizeDelta.y);
                        //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                    }
                }
                break;
        }
       
        

    }
    void realSave()
    {
        Debug.Log("replaceSave!");
    }
    void realLoad()
    {
        GameObject text = EventSystem.current.currentSelectedGameObject;
        string selectedSave = text.transform.Find("Number").GetComponentInChildren<Text>().text;
        Debug.Log(selectedSave);
        foreach (var item in saveGames)
        {
            
            if (item.savegameName == selectedSave)
            {
                SceneManager.LoadScene(item.scene);
                DontDestroyOnLoad(slu.gameObject);
                DontDestroyOnLoad(gameObject);
                slu.LoadGame(EventSystem.current.currentSelectedGameObject.name);
                //Destroy(slu.gameObject);
                //Destroy(gameObject);
            }
        }
        
    }
    private void OnGUI()
    {
        
    }
    private void OnDisable()
    {
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
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y - SlotRect.sizeDelta.y);
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
                        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentRect.sizeDelta.y - SlotRect.sizeDelta.y);
                        //ContentRect.rect.Set(ContentRect.rect.x, ContentRect.rect.y, ContentRect.rect.width, ContentRect.rect.height + SlotRect.rect.height);
                    }
                }
                break;
        }
        
    }
}
