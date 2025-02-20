using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    [Serializable]
    public class HistoryData
    {
        public TerrainType TerrainType;
        public string PlantNameData;
        public bool IsMet;
        public string TerrainName;
    }

    [Serializable]
    public class HistoriesData
    {
        public List<HistoryData> Data;
        public HistoriesData()
        {
            Data = new List<HistoryData>();
        }
    }

    public static HistoryController Instance;

    [SerializeField]
    private Button m_UndoButton;

    [SerializeField]
    private Button m_RedoButton;

    [SerializeField]
    private int m_Index = -1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        m_UndoButton.onClick.AddListener(Undo);
        m_RedoButton.onClick.AddListener(Redo);
        m_RedoButton.interactable = false;
        m_UndoButton.interactable = false;
    }

    private void OnDestroy()
    {
        m_UndoButton.onClick.RemoveListener(Undo);
        m_RedoButton.onClick.RemoveListener(Redo);
    }

    private void Undo()
    {
        if (m_Index <= 0)
        {
            m_UndoButton.interactable = false;
            DeleteAllFiles(1);
            return;
        }

        m_Index--;
        m_RedoButton.interactable = true;

        if (m_Index == 0)
        {
            m_UndoButton.interactable = false;
            DeleteAllFiles(1);
        }

        Load();
    }

    private void Load()
    {
        string pathFile = Path.Combine(GetFullPath(), $"{m_Index}.json");
        if (!File.Exists(pathFile))
        {
            Debug.LogError($"File not found: {pathFile}");
            return;
        }

        string json = File.ReadAllText(pathFile);
        HistoriesData historiesData = new HistoriesData();
        JsonUtility.FromJsonOverwrite(json, historiesData);

        foreach (var historyData in historiesData.Data)
            TerrainController.Instance.Load(historyData);
    }

    private void Redo()
    {
        var files = Directory.GetFiles(GetFullPath()).ToList().FindAll(f => !f.Contains(".meta")); ;
        if (m_Index >= files.Count - 1)
        {
            m_RedoButton.interactable = false;
            return;
        }

        m_Index++;
        m_UndoButton.interactable = true;

        if (m_Index >= files.Count - 1)
        {
            m_RedoButton.interactable = false;
        }

        Load();
    }

    public void Save()
    {
        m_Index++;
        HistoriesData historiesData = new HistoriesData();

        foreach (var terrain in TerrainController.Instance.Terrains)
        {
            HistoryData historyData = new HistoryData();
            historyData.TerrainType = terrain.Data.TerrainType;
            historyData.PlantNameData = terrain.PlantData?.name;
            historyData.IsMet = terrain.IsMeet;
            historyData.TerrainName = terrain.name;
            historiesData.Data.Add(historyData);
        }

        string pathFile = Path.Combine(GetFullPath(), $"{m_Index}.json");
        string json = JsonUtility.ToJson(historiesData, true);
        File.WriteAllText(pathFile, json);

        m_RedoButton.interactable = false;
        m_UndoButton.interactable = true;
    }

    private string GetFullPath()
    {
        string path = Path.Combine(Application.streamingAssetsPath, $"History");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }

    void OnApplicationQuit()
    {
        DeleteAllFiles(0);
    }

    private void DeleteAllFiles(int start)
    {
        var count = Directory.GetFiles(GetFullPath()).Length;
        for (int i = start; i < count; i++)
        {
            string pathFile = Path.Combine(GetFullPath(), $"{i}.json");
            File.Delete(pathFile);
        }

        Directory.Delete(GetFullPath(), true);
    }
}
