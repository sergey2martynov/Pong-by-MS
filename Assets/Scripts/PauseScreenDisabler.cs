using UnityEngine;

public class PauseScreenDisabler : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private GameObject _pauseText;

    public void SetPauseScreenActive(bool isActive)
    {
        _pausePanel.SetActive(isActive);
        _pauseText.SetActive(isActive);
    }
}