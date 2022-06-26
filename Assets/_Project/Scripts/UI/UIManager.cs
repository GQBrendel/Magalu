using nl.DTT.OSRVR.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void FinishedUpgradeShenanigans();
    public FinishedUpgradeShenanigans OnUpgradeFinished;

    public static UIManager Instance;

    [SerializeField] private LeanFadeGroup _itemHolder;
    [SerializeField] private Image _itemHolderIcon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        _itemHolder.FadeInstant(false);
    }

    public void ShowItemIcon(Sprite newSprite)
    {
        _itemHolderIcon.sprite = newSprite;
        _itemHolder.gameObject.SetActive(true);
        _itemHolder.FadeGroup(true);
    }

    internal void MoveUpgradeIcon()
    {
        Vector2 newSize = new Vector2(2,2);
        LeanTween.scale(_itemHolderIcon.gameObject, newSize, GameConfig.Instance.IconMovementTime);
        LeanTween.moveLocalY(_itemHolderIcon.gameObject, -600, GameConfig.Instance.IconMovementTime).setOnComplete(FinishedMoveIcon);
    }

    private void FinishedMoveIcon()
    {
        _itemHolder.FadeGroup(false);
        StartCoroutine(WaitAndReset());

        IEnumerator WaitAndReset()
        {
            yield return new WaitForSeconds(GameConfig.Instance.IconFadeTime);
            Reset();
        }
    }

    private void Reset()
    {
        OnUpgradeFinished?.Invoke();

        Vector2 newSize = new Vector2(1, 1);

        LeanTween.scale(_itemHolderIcon.gameObject, newSize, 0f);
        LeanTween.moveLocalY(_itemHolderIcon.gameObject, 0, 0f);
        _itemHolder.gameObject.SetActive(false);
    }
}