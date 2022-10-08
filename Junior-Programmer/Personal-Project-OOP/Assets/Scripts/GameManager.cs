using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _companyNameTMP;

    string _companyName;

    //ENCAPSULATION
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadData();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SetCompanyNameInMain;

        if (_companyName != null)
        {
            LoadMainScene();
            Debug.Log("Direct load to main");
        }
    }

    private void OnDisable()
    {
        SaveData();
    }

    public void SetCompanyName()
    {
        _companyName = _companyNameTMP.text;
        Debug.Log(_companyName);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SetCompanyNameInMain(Scene scene, LoadSceneMode mode)
    {
        GameObject companyNameObject = GameObject.FindGameObjectWithTag("CompanyName");

        if (companyNameObject == null) { return; }
        _companyNameTMP = companyNameObject.GetComponent<TextMeshProUGUI>();

        if (_companyNameTMP == null) { return; }
        _companyNameTMP.text = _companyName;
    }

    [System.Serializable]
    class SaveFile
    {
        public string CompanyName;
        public int CompanyLastBudget;
    }

    void SaveData()
    {
        SaveFile saveFile = new SaveFile();

        saveFile.CompanyName = _companyName;

        saveFile.CompanyLastBudget = CompanyBudget.Instance.CurrentBudget;

        string json = JsonUtility.ToJson(saveFile);
        File.WriteAllText(Application.persistentDataPath + "/hardhatsavefile.json", json);

        Debug.Log("saved");
    }

    void LoadData()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/hardhatsavefile.json");

        if (json == null) { return; }
        SaveFile saveFile = saveFile = JsonUtility.FromJson<SaveFile>(json);

        _companyName = saveFile.CompanyName;

        if (saveFile.CompanyLastBudget != 0)
        {
            CompanyBudget.Instance.CurrentBudget = saveFile.CompanyLastBudget;
        }

        Debug.Log("loaded " + _companyName + " " + CompanyBudget.Instance.CurrentBudget);
    }
}
