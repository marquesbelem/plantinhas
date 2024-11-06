using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_DragSpeed = 2;
    [SerializeField] private int m_Max;
    [SerializeField] private int m_Min;
    private Vector3 m_DragOrigin;

    void Update()
    {
        #region Zoom
        if (Input.GetKeyDown(KeyCode.Z) && 
            Camera.main.orthographicSize < m_Max)
        {
            Camera.main.orthographicSize++;
        }
        
        if (Input.GetKeyDown(KeyCode.X) 
            && Camera.main.orthographicSize > m_Min)
        {
            Camera.main.orthographicSize--;
        }
        #endregion


        #region Drag & Move Camera
        if (Input.GetMouseButtonDown(0))
        {
            m_DragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - m_DragOrigin);
        Vector3 move = new Vector3(pos.x * m_DragSpeed, pos.y * m_DragSpeed, 0);

        Camera.main.transform.Translate(move, Space.World);
        #endregion
    }
}
