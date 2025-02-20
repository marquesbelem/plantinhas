using System;
using UnityEngine;
using static PlantData;

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainData m_Data;

    private SpriteRenderer m_Sprite;
    public PlantState PlantState;
    public PlantData PlantData;

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

    public void Plant(PlantData data, bool isMet)
    {
        PlantState.Setup(data, isMet);
    }

    private void OnMouseDown()
    {
        if (Application.isPlaying == false ||
            MouseController.Instance.PlantData == null) return;

        //FirstHistory();

        PlantData = MouseController.Instance.PlantData;

        if (PlantState?.State == StatePlant.Adult) return;

        switch (PlantData.Requirements)
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
        var isValid = m_InsideRequirement.IsValid(PlantData.TerrainType, m_Data.TerrainType);
        Plant(PlantData, isValid);
        IsMeet = isValid;
    }

    private void AdjacentRequirementsExecute()
    {
        var isValid = m_AdjacentRequirement.IsValid();
        Plant(PlantData, isValid);

        if (isValid)
        {
            try
            {
                m_AdjacentRequirement.TransformExecute(PlantData.Result);
            }
            catch { }

            try
            {
                m_AdjacentRequirement.ReplicaExecute(PlantData, PlantData.Result);
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

        if (PlantData.Requirements != RequirementsType.ADJACENT) return;
        m_AdjacentRequirement.Raycasts(PlantData.TerrainType, this.gameObject);
    }
}
