using UnityEngine;
[ExecuteInEditMode]

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainData m_Data;

    private SpriteRenderer m_Sprite;
    private PlantState m_CurrentPlantState;

    private void OnEnable()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Sprite.sprite = m_Data.Sprite;
    }

    public PlantState Plant(PlantData data, TerrainType terrainType)
    {
        var go = new GameObject(); 
        go.transform.SetParent(transform, false);
        go.name = data.name;
        var plant = go.AddComponent<PlantState>();
        plant.GetComponent<PlantState>()
            .Setup(data, terrainType);
        return plant;
    }

    private void OnMouseDown()
    {
        if (Application.isPlaying == false) return;
        
        Debug.Log("OnMouseDown");
        if(m_CurrentPlantState != null)
        {
            if (m_CurrentPlantState?.State != StatePlant.Seed) return;
            Destroy(m_CurrentPlantState.gameObject);
        }

        m_CurrentPlantState = Plant(MouseController.Instance.plantData, m_Data.TerrainType);
    }
}
