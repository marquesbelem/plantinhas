using UnityEngine;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance;
    public PlantData PlantData => m_PlantData;

    private SpriteRenderer m_SpriteRenderer;
    private PlantData m_PlantData;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.enabled = false;
    }

    private void Update()
    {
        #region Move Sprite
        if (m_PlantData != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
        #endregion
    }

    public void SelectedPlant(PlantData plant)
    {
        m_PlantData = plant;
        m_SpriteRenderer.sprite = m_PlantData.Sprites.Find(s => s.State == StatePlant.Adult).Sprite;
        m_SpriteRenderer.enabled = true;
    }

    public void Deselectedplant()
    {
        m_PlantData = null;
        m_SpriteRenderer.enabled = false;
    }
}
