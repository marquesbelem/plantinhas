using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AdjacentRequirement
{
    private static float m_RayDistance = 2f;
    private static Color m_RayColor = Color.red;
    private static List<Terrain> m_AdjacentTerrainsMet = new List<Terrain>();
    private static List<Terrain> m_AdjacentAnyTerrains = new List<Terrain>();
    public static List<Terrain> AdjacentAnyTerrains => m_AdjacentAnyTerrains;
    public static Action OnCompletedRaycast;

    public static bool IsValid()
    {
        return m_AdjacentTerrainsMet.Count > 0;
    }

    public static void Raycasts(TerrainType terrainRequirement, GameObject target)
    {
        Vector2 targetPosition = target.transform.position;
        float radianRotation = target.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;

        Vector2[] directions = {
            new Vector2(Mathf.Cos(radianRotation), Mathf.Sin(radianRotation)), // Direção do "cima"
            new Vector2(Mathf.Cos(radianRotation + Mathf.PI), Mathf.Sin(radianRotation + Mathf.PI)), // Direção do "baixo"
            new Vector2(Mathf.Cos(radianRotation + Mathf.PI / 2), Mathf.Sin(radianRotation + Mathf.PI / 2)), // Direção da "esquerda"
            new Vector2(Mathf.Cos(radianRotation - Mathf.PI / 2), Mathf.Sin(radianRotation - Mathf.PI / 2))  // Direção da "direita"
        };

        foreach (var direction in directions)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(targetPosition, direction, m_RayDistance);
            Debug.DrawRay(targetPosition, direction * m_RayDistance, m_RayColor);

            foreach (var hit in hits)
            {
                if (hit.collider == null || hit.collider.gameObject == target) continue;

                if (hit.collider.TryGetComponent<Terrain>(out var component))
                {
                    if (!m_AdjacentAnyTerrains.Contains(component))
                        m_AdjacentAnyTerrains.Add(component);

                    if (component.Data.TerrainType != terrainRequirement ||
                        m_AdjacentTerrainsMet.Contains(component))
                        continue;
                    m_AdjacentTerrainsMet.Add(component);
                }
            }
        }

        OnCompletedRaycast?.Invoke();
    }

    public static void Reset()
    {
        m_AdjacentTerrainsMet.Clear();
        m_AdjacentAnyTerrains.Clear();
        OnCompletedRaycast = null;
    }
}