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
    private Action m_OnChangedState_CorretilyMet;
    private Action m_OnChangedState_WrongMet;

    public StatePlant State { get { return m_State; } }

    #region MonoBehaviour
    void Start()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        m_OnChangedState_CorretilyMet -= OnChangedStateCorretilyMet;
    }
    #endregion

    public void SetState(StatePlant state)
    {
        m_State = state;
        m_OnChangedState_CorretilyMet?.Invoke();
        m_OnChangedState_WrongMet?.Invoke();
    }

    public void Setup(PlantData data, Transform parent, Transform pivot, bool isMet)
    {
        transform.SetParent(parent, false);
        transform.position = pivot.position;
        name = data.name;

        m_Data = data;
        if (isMet)
            m_OnChangedState_CorretilyMet += OnChangedStateCorretilyMet;
        else
            m_OnChangedState_WrongMet += OnChangedStateWrongMet;

        SetState(StatePlant.Seed);
    }

    #region Events 
    private void OnChangedStateCorretilyMet()
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

    private void OnChangedStateWrongMet()
    {
        switch (m_State)
        {
            case StatePlant.None:
                None();
                break;
            case StatePlant.Seed:
                Seed();
                break;
        }
    }
    #endregion

    #region States
    private void None()
    {
        m_Sprite.sprite = m_Data.Sprites.Find(s => s.State == StatePlant.None).Sprite;
    }

    private void Seed()
    {
        StartCoroutine(ExecuteState(StatePlant.Seed));
        SetState(StatePlant.Bud);
    }

    private void Bud()
    {
        StartCoroutine(ExecuteState(StatePlant.Bud));
        SetState(StatePlant.Adult);
    }

    private void Adult()
    {
        StartCoroutine(ExecuteState(StatePlant.Adult));
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
