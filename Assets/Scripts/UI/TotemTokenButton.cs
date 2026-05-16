using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class TotemTokenButton : MonoBehaviour
{
    public int Index { get; private set; } = -1;

    [SerializeField] private Image tokenImage;
    [SerializeField] private Image highlightImage;
    private Button button;
    private Action<int> onSelected;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (tokenImage == null)
            tokenImage = GetComponentInChildren<Image>();

        button.onClick.RemoveAllListeners();
    }

    public void Setup(int index, Sprite sprite, Action<int> onSelectedCallback)
    {
        Index = index;
        onSelected = onSelectedCallback;

        if (tokenImage != null)
            tokenImage.sprite = sprite;

        button.onClick.AddListener(() => onSelected?.Invoke(Index));

        SetSelected(false);
    }

    public void SetSelected(bool selected)
    {
        if (highlightImage != null)
            highlightImage.enabled = selected;
        else if (tokenImage != null)
            tokenImage.color = selected ? new Color(1f, 0.95f, 0.6f) : Color.white;
    }
}
