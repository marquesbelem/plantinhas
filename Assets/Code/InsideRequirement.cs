public class InsideRequirement
{
    public bool IsValid(TerrainType terrainRequirement, TerrainType currentTerrain)
    {
        return terrainRequirement == currentTerrain;
    }
}
