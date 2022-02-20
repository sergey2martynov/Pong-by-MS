using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action<string> ButtonClicked;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke(_button.gameObject.name);
    }
}