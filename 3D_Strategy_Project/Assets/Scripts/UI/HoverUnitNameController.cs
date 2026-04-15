using TMPro;
using UnityEngine;

public class HoverUnitNameController : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private GameObject m_hoverUnitPanel;
    [SerializeField] private GameObject m_parentUnit;
    [SerializeField] private TMP_Text m_unitNameText;

    private void Awake()
    {
        m_unitNameText.text = m_parentUnit.name;
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        Vector3 cameraPos = m_camera.transform.position;
        // TODO: Исправить точку положения
        Vector3 lookPos = new Vector3(-cameraPos.x, cameraPos.y, -cameraPos.z);
        m_hoverUnitPanel.transform.LookAt(lookPos);        
    }

    public void ShowPanel()
    {
        m_hoverUnitPanel.SetActive(true);
    }

    public void HidePanel()
    {
        m_hoverUnitPanel.SetActive(false);
    }
}
