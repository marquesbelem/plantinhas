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

    public void Setup(PlantData data, TerrainType terrainType, Transform parent, Transform pivot)
    {
        transform.SetParent(parent, false);
        transform.position = pivot.position;
        name = data.name;

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

        if (IsValid() == false) return;
        
        SetState(StatePlant.Bud, m_TerrainTypeSelected);
    }

    private void Bud()
    {
        if (IsValid() == false) return;

        StartCoroutine(ExecuteState(StatePlant.Bud));
        SetState(StatePlant.Adult, m_TerrainTypeSelected);
    }

    private void Adult()
    {
        if (IsValid() == false) return;

        StartCoroutine(ExecuteState(StatePlant.Adult));
    }

    private bool IsValid()
    {
        return m_TerrainTypeSelected == m_Data.TerrainType;
    }

    private IEnumerator ExecuteState(StatePlant state)
    {
        var time = m_Data.WaitTimes.Find(t => t.State == state).WaitTime;

        yield return new WaitForSeconds(time);

        m_Sprite.sprite = m_Data.Sprites.Find(s => s.State == state).Sprite;
        m_Sprite.color = m_Data.Sprites.Find(s => s.State == state).Color;
    }
    #endregion
}
