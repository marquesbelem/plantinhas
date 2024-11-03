using UnityEngine;

public class MouseController : MonoBehaviour
{
    public PlantData plantData;

    public static MouseController Instance;

    private void Awake()
    {
        Instance = this;
    }
}
