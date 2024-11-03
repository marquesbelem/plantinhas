using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainData", menuName = "Plantinhas/TerrainData")]
public class TerrainData : ScriptableObject
{
    public Sprite Sprite;
    public TerrainType TerrainType;
    public int OrderToAnimation;
}

[Serializable]
public enum TerrainType
{
    None,
    Mountain,
    Grass,
    Dirt,
    Water,
    Lava,
    Mangrove,
    Sand
}