using UnityEngine;
using static PlantData;

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

    private PlantState Plant(PlantData data, bool isMet)
    {
        var plant = Instantiate(m_PlantPrefab)
            .GetComponent<PlantState>();

        plant.Setup(data, transform, m_Pivot, isMet);
        return plant;
    }

    private void OnMouseDown()
    {
        if (Application.isPlaying == false ||
            MouseController.Instance.PlantData == null) return;

        if (m_CurrentPlantState != null)
        {
            if (m_CurrentPlantState?.State == StatePlant.Adult) return;
            Destroy(m_CurrentPlantState.gameObject);
        }

        ExecuteRequirements();
    }

    private void ExecuteRequirements()
    {
        var plantData = MouseController.Instance.PlantData;
        switch (plantData.Requirements)
        {
            case RequirementsType.INSIDE:
                var isValid = InsideRequirement.IsValid(plantData.TerrainType, m_Data.TerrainType);
                m_CurrentPlantState = Plant(plantData, isValid);
                break;
            case RequirementsType.ADJACENT:
                if (AdjacentRequirement.IsValid())
                {

                }
                break;
        }
    }
}
