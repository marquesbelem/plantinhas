using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Plantinhas/PlantData")]
public class PlantData : ScriptableObject
{
    public TerrainType TerrainType;

    [Space(10)]
    [Header("Requirements")]
    public RequirementsType Requirements;
    public ResultData Result;

    [Space(10)]
    [Header("Visual")]
    public List<StateSprites> Sprites;
    public List<StateTime> WaitTimes;

    [Serializable]
    public struct StateSprites
    {
        public StatePlant State;
        public Sprite Sprite;
        public Color32 Color;
    }

    [Serializable]
    public struct StateTime
    {
        public StatePlant State;
        public float WaitTime;
    }

    public enum RequirementsType
    {
        INSIDE,
        ADJACENT
    }

    public enum ResultType
    {
        TRANSFORM,
        REPLICA
    }
}
