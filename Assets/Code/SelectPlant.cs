using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectPlant : MonoBehaviour
{
    public PlantData PlantData => m_PlantData;

    private Image m_Image; 
    private PlantData m_PlantData;
    private Button m_Button; 

    private void Awake()
    {
        m_Image = GetComponent<Image> ();
        m_Button = GetComponent<Button> ();
        m_Button.onClick.AddListener(OnClickButton);
    }

    public void Setup(PlantData plant)
    {
        m_PlantData = plant;
        m_Image.sprite = m_PlantData.Sprites.Find(s => s.State == StatePlant.Adult).Sprite;
    }

    private void OnClickButton()
    {
        MouseController.Instance.SelectedPlant(m_PlantData);
    }

}
