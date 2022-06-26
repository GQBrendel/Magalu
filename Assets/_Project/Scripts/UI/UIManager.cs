using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _itemHolder;
    [SerializeField] private Image _itemHolderIcon;

    public void ShowItemIcon(Sprite newSprite)
    {
        _itemHolderIcon.sprite = newSprite;
        _itemHolder.gameObject.SetActive(true);
    }
}
