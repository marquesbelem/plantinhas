using System.Collections.Generic;
using UnityEngine;

public class SelectionPlantsController : MonoBehaviour
{
    [SerializeField]
    private List<PlantData> m_PlantsData;
    [SerializeField]
    private GameObject m_Prefab; 

    private void Start()
    {
        CreateSelections();
    }

    private void CreateSelections()
    {
        foreach (var data in m_PlantsData)
        {
            var go = Instantiate(m_Prefab);
            go.transform.SetParent(transform, false);
            var select = go.GetComponent<SelectPlant>();
            select.Setup(data);
        }
    }
}
