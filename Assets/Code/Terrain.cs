using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainData m_Data;
    [SerializeField] private GameObject m_PlantPrefab;
    [SerializeField] private Transform m_Pivot; 

    private SpriteRenderer m_Sprite;
    private PlantState m_CurrentPlantState;

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetupSprite();
    }
#endif

    private void Start()
    {
        SetupSprite();
    }
    
    private void SetupSprite()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Sprite.sprite = m_Data.Sprite;
    }

    private PlantState Plant(PlantData data, TerrainType terrainType)
    {
        var plant = Instantiate(m_PlantPrefab)
            .GetComponent<PlantState>();

        plant.Setup(data, terrainType, transform, m_Pivot);
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
