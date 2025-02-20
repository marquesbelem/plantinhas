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

    private Coroutine m_Coroutine;
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

    public void Setup(PlantData data, bool isMet)
    {
        name = data.name;
        m_Data = data;

        if (m_Coroutine != null)
            StopCoroutine(m_Coroutine);

        if (data == null)
        {
            SetState(StatePlant.None);
            m_Data = null;
            return;
        }

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
        m_Sprite.sprite = null;
    }

    private void Seed()
    {
        m_Coroutine = StartCoroutine(ExecuteState(StatePlant.Seed));
        SetState(StatePlant.Bud);
    }

    private void Bud()
    {
        m_Coroutine = StartCoroutine(ExecuteState(StatePlant.Bud));
        SetState(StatePlant.Adult);
    }

    private void Adult()
    {
        m_Coroutine = StartCoroutine(ExecuteState(StatePlant.Adult));
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
