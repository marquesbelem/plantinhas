using System;
using System.Collections;
using UnityEngine;
public enum StatePlant
{
    None,
    Seed,
    Bud,
    Adult
}

[RequireComponent(typeof(SpriteRenderer))]
public class PlantState : MonoBehaviour
{
    [SerializeField] private StatePlant m_State;
    [SerializeField] private PlantData m_Data;

    private SpriteRenderer m_Sprite;
    private TerrainType m_TerrainTypeSelected;
    private Action m_OnChangedState;

    public StatePlant State { get { return m_State; } }

    #region MonoBehaviour
    void Start()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        m_OnChangedState -= OnChangedState;
    }
    #endregion

    public void SetState(StatePlant state, TerrainType terrainType)
    {
        m_State = state;
        m_TerrainTypeSelected = terrainType;
        m_OnChangedState?.Invoke();
    }

    public void Setup(PlantData data, TerrainType terrainType)
    {
        m_Data = data;
        m_OnChangedState += OnChangedState;
        SetState(StatePlant.Seed, terrainType);
    }

    #region States
    private void OnChangedState()
    {
        switch (m_State)
        {
            case StatePlant.None:
                None();
                break;
            case StatePlant.Seed:
                Seed();
                break;
            case StatePlant.Bud:
                Bud();
                break;
            case StatePlant.Adult:
                Adult();
                break;
            default:
                break;
        }
    }

    private void None()
    {
        m_Sprite.sprite = m_Data.Sprites.Find(s => s.State == StatePlant.None).Sprite;
    }

    private void Seed()
    {
        StartCoroutine(ExecuteState(StatePlant.Seed));

        if (m_TerrainTypeSelected == m_Data.TerrainType)
        {
            Debug.Log("seed");
            SetState(StatePlant.Bud, m_TerrainTypeSelected);
        }
    }

    private void Bud()
    {
        if (m_TerrainTypeSelected == m_Data.TerrainType)
        {
            Debug.Log("Bud");
            StartCoroutine(ExecuteState(StatePlant.Bud));
            SetState(StatePlant.Adult, m_TerrainTypeSelected);
        }
    }

    private void Adult()
    {
        if (m_TerrainTypeSelected == m_Data.TerrainType)
        {

            Debug.Log("Adult");
            StartCoroutine(ExecuteState(StatePlant.Adult));
        }
    }

    private IEnumerator ExecuteState(StatePlant state)
    {
        var time = m_Data.WaitTimes.Find(t => t.State == state).WaitTime;
        yield return new WaitForSeconds(time);
        m_Sprite.sprite = m_Data.Sprites.Find(s => s.State == state).Sprite;
    }
    #endregion
}
