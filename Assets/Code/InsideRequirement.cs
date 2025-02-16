public static class InsideRequirement
{
    public static bool IsValid(TerrainType terrainRequirement, TerrainType currentTerrain)
    {
        return terrainRequirement == currentTerrain;
    }
}
