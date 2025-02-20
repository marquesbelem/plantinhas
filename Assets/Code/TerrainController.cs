using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HistoryController;

public class TerrainController : MonoBehaviour
{
    public static TerrainController Instance;
    public List<Terrain> Terrains { get; set; }
    [SerializeField]
    private List<TerrainData> m_TerrainsData = new List<TerrainData>();
    [SerializeField]
    private List<PlantData> m_PlantsData = new List<PlantData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Terrains = new List<Terrain>();
        Terrains.AddRange(FindObjectsByType<Terrain>(FindObjectsSortMode.None).ToList());
        HistoryController.Instance.Save();
    }

    public void Load(HistoryData data)
    {
        var terrainFind = Terrains.Find(t => t.name == data.TerrainName);
        if (terrainFind == null)
            return;

        var terrainData = m_TerrainsData.Find(t => t.TerrainType == data.TerrainType);
        terrainFind.SetData(terrainData);
        terrainFind.SetupSprite();

        if (string.IsNullOrEmpty(data.PlantNameData))
            return;

        var plantData = m_PlantsData.Find(p => p.name == data.PlantNameData);
        if (plantData != null)
            terrainFind.Plant(plantData, data.IsMet);
    }
}
