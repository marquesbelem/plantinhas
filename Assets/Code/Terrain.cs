using UnityEngine;
[ExecuteInEditMode]

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainData m_Data;
    [SerializeField] private GameObject m_PlantPrefab;

    private SpriteRenderer m_Sprite;
    private PlantState m_CurrentPlantState;

    private void OnEnable()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Sprite.sprite = m_Data.Sprite;
    }

    public PlantState Plant(PlantData data, TerrainType terrainType)
    {
        var go = Instantiate(m_PlantPrefab);
        go.transform.SetParent(transform, false);
        go.name = data.name;
        var plant = go.GetComponent<PlantState>();
        plant.Setup(data, terrainType);
        return plant;
    }

    private void OnMouseDown()
    {
        if (Application.isPlaying == false || 
            MouseController.Instance.PlantData == null) return;
        
        if(m_CurrentPlantState != null)
        {
            if (m_CurrentPlantState?.State == StatePlant.Adult) return;
            Destroy(m_CurrentPlantState.gameObject);
        }

        m_CurrentPlantState = Plant(MouseController.Instance.PlantData, m_Data.TerrainType);
    }
}
