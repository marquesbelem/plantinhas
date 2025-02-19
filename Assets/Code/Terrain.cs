using System;
using UnityEngine;
using static PlantData;

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainData m_Data;
    [SerializeField] private GameObject m_PlantPrefab;
    [SerializeField] private Transform m_Pivot;

    private SpriteRenderer m_Sprite;
    public PlantState CurrentPlantState;
    public PlantData CurrentPlantData;

    private bool m_IsClick;
    private AdjacentRequirement m_AdjacentRequirement;
    private InsideRequirement m_InsideRequirement;
    public TerrainData Data => m_Data;

    public bool IsMeet = false; 

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetupSprite();
    }
#endif

    private void Awake()
    {
        m_AdjacentRequirement = new AdjacentRequirement();
        m_InsideRequirement = new InsideRequirement();
    }

    private void Start()
    {
        SetupSprite();
    }

    public void SetupSprite()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Sprite.sprite = m_Data.Sprite;
    }

    public void SetData(TerrainData data)
    {
        m_Data = data;
    }

    public PlantState Plant(PlantData data, bool isMet)
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
        
        //FirstHistory();

        CurrentPlantData = MouseController.Instance.PlantData;

        if (CurrentPlantState != null)
        {
            if (CurrentPlantState?.State == StatePlant.Adult) return;
            Destroy(CurrentPlantState.gameObject);
        }

        switch (CurrentPlantData.Requirements)
        {
            case RequirementsType.INSIDE:
                InsideRequirementsExecute();
                break;
            case RequirementsType.ADJACENT:
                m_IsClick = true;
                m_AdjacentRequirement.OnCompletedRaycast += AdjacentRequirementsExecute;
                break;
        }

        HistoryController.Instance.Save();
    }

    private void InsideRequirementsExecute()
    {
        var isValid = m_InsideRequirement.IsValid(CurrentPlantData.TerrainType, m_Data.TerrainType);
        CurrentPlantState = Plant(CurrentPlantData, isValid);
        IsMeet = isValid;
    }

    private void AdjacentRequirementsExecute()
    {
        var isValid = m_AdjacentRequirement.IsValid();
        CurrentPlantState = Plant(CurrentPlantData, isValid);

        if (isValid)
        {
            try
            {
                m_AdjacentRequirement.TransformExecute(CurrentPlantData.Result);
            }
            catch { }

            try
            {
                m_AdjacentRequirement.ReplicaExecute(CurrentPlantData, CurrentPlantData.Result);
            }
            catch { }
        }

        m_IsClick = false;
        m_AdjacentRequirement.Reset();
        IsMeet = isValid;
    }

    private void Update()
    {
        if (m_IsClick == false) return;

        if (CurrentPlantData.Requirements != RequirementsType.ADJACENT) return;
        m_AdjacentRequirement.Raycasts(CurrentPlantData.TerrainType, this.gameObject);
    }
}
