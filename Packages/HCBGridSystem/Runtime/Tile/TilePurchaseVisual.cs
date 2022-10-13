using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using HCB.Core;
using System;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class TilePurchaseVisual : MonoBehaviour
    {
        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;

        private TilePurchase _tilePurchase;
        private TilePurchase TilePurchase => _tilePurchase == null ? _tilePurchase = GetComponentInParent<TilePurchase>() : _tilePurchase;

        [SerializeField] private Transform _body;
        [Space(10)]
        [SerializeField] private Transform _buttonBody;
        [SerializeField] private SpriteRenderer _buttonRenderer;
        [Space(10)]
        [SerializeField] private Sprite _activeButtonSprite;
        [SerializeField] private Sprite _disableButtonSprite;
        [Space(10)]
        [SerializeField] private TextMeshPro _priceTextMesh;

        private const float BODY_SCALE_MULTIPLIER = 0.001f;
        private const float BUTTON_SCALE_MULTIPLIER = 1.1f;

        private const float BODY_SCALE_DURATION = 0.2f;
        private const float BUTTON_SCALE_DURATION = 0.85f;

        private const string DOLAR_SIGN = "$";

        private string _buttonScaleTweenID;
        private string _bodyScaleTweenID;

        private void Awake()
        {
            _buttonScaleTweenID = GetInstanceID() + "ButtonScaleTweenID";
            _bodyScaleTweenID = GetInstanceID() + "BodyScaleTweenID";
        }

        private void OnEnable()
        {
            GridSystemEventManager.OnTileDataLoaded.AddListener(CheckTileVisual);
            TilePurchase.OnPurchaseActivated.AddListener(CheckTileVisual);
            Tile.OnTilePurchased.AddListener(OnTilePurchased);
            EventManager.OnPlayerDataChange.AddListener(UpdateComponents);
        }

        private void OnDisable()
        {
            GridSystemEventManager.OnTileDataLoaded.RemoveListener(CheckTileVisual);
            TilePurchase.OnPurchaseActivated.RemoveListener(CheckTileVisual);
            Tile.OnTilePurchased.RemoveListener(OnTilePurchased);
            EventManager.OnPlayerDataChange.RemoveListener(UpdateComponents);
        }
        
        private void CheckTileVisual()
        {
            if (CanUpdateComponents())
            {
                _body.gameObject.SetActive(true);

                SetText();
                SetButton();
                ButtonScaleTween();
            }

            else
            {
                _body.gameObject.SetActive(false);
            }
        }

        private void OnTilePurchased()
        {
            DOTween.Kill(_buttonScaleTweenID);
            BodyScaleTween(() =>
            {
                _body.gameObject.SetActive(false);
            });
        }

        private void UpdateComponents()
        {
            if (!CanUpdateComponents())
                return;

            SetButton();
        }

        private void SetText()
        {
            _priceTextMesh.SetText(DOLAR_SIGN + HCBUtilities.ScoreShow(Tile.TilePrice));
        }

        private void SetButton()
        {
            _buttonRenderer.sprite = HasPlayerEnoughMoney() ? _activeButtonSprite : _disableButtonSprite;
        }

        private void ButtonScaleTween()
        {
            Vector3 targetScale = _buttonBody.localScale * BUTTON_SCALE_MULTIPLIER;

            DOTween.Kill(_buttonScaleTweenID);
            _buttonBody.DOScale(targetScale, BUTTON_SCALE_DURATION).SetEase(Ease.InOutSine).SetId(_buttonScaleTweenID).SetLoops(-1, LoopType.Yoyo);
        }

        private void BodyScaleTween(Action onComplete)
        {
            Vector3 targetScale = _body.localScale * BODY_SCALE_MULTIPLIER;

            DOTween.Kill(_bodyScaleTweenID);
            _body.DOScale(targetScale, BODY_SCALE_DURATION).SetEase(Ease.Linear).SetId(_bodyScaleTweenID).OnComplete(() => onComplete?.Invoke());
        }

        private bool CanUpdateComponents()
        {
            if (Tile.IsPurchased)
                return false;

            if (!TilePurchase.IsPurchaseActivated)
                return false;

            return true;
        }

        private bool HasPlayerEnoughMoney()
        {
            return GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] >= Tile.TilePrice;
        }
    }
}
