using System.Collections.Generic;
using UnityEngine;
using static PlantData;

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainData m_Data;
    [SerializeField] private GameObject m_PlantPrefab;
    [SerializeField] private Transform m_Pivot;

    private SpriteRenderer m_Sprite;
    private PlantState m_CurrentPlantState;
    private bool m_IsClick;

    public TerrainData Data => m_Data;
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

        m_IsClick = true;

        if (m_CurrentPlantState != null)
        {
            if (m_CurrentPlantState?.State == StatePlant.Adult) return;
            Destroy(m_CurrentPlantState.gameObject);
        }

        switch (MouseController.Instance.PlantData.Requirements)
        {
            case RequirementsType.INSIDE:
                ExecuteInsideRequirements();
                break;
            case RequirementsType.ADJACENT:
                AdjacentRequirement.OnCompletedRaycast += ExecuteAdjacentRequirements;
                break;

        }
    }

    private void ExecuteInsideRequirements()
    {
        var plantData = MouseController.Instance.PlantData;
        var isValid = InsideRequirement.IsValid(plantData.TerrainType, m_Data.TerrainType);
        m_CurrentPlantState = Plant(plantData, isValid);

    }

    private void ExecuteAdjacentRequirements()
    {
        var plantData = MouseController.Instance.PlantData;
        var isValid = AdjacentRequirement.IsValid();
        m_CurrentPlantState = Plant(plantData, isValid);

        m_IsClick = false;
        AdjacentRequirement.Reset();
    }

    private void Update()
    {
        if (m_IsClick == false) return;

        var plantData = MouseController.Instance.PlantData;
        if (plantData.Requirements != RequirementsType.ADJACENT) return;
        AdjacentRequirement.Raycasts(plantData.TerrainType, this.gameObject);
    }
}
