using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonControllerBase : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action Clicked;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    protected  void  OnButtonClicked()
    {
        Clicked?.Invoke();
    }
}