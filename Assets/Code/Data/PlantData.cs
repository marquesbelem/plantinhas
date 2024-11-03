using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Plantinhas/PlantData")]
public class PlantData : ScriptableObject
{
    public TerrainType TerrainType;
    public List<StateSprites> Sprites;
    public List<StateTime> WaitTimes;

    [Serializable]
    public struct StateSprites
    {
        public StatePlant State;
        public Sprite Sprite;
    }

    [Serializable]
    public struct StateTime
    {
        public StatePlant State;
        public float WaitTime;
    }
}
