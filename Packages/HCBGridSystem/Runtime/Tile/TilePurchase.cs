using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HCB.Core;
using DG.Tweening;

namespace HCB.GridSystem
{
    public class TilePurchase : MonoBehaviour
    {
        private MouseEventDetector _mouseEventDetector;
        private MouseEventDetector MouseEventDetector => _mouseEventDetector == null ? _mouseEventDetector = GetComponentInChildren<MouseEventDetector>() : _mouseEventDetector;

        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;
        public bool IsPurchased => Tile.IsPurchased;
        public bool IsPurchaseActivated { get; private set; }

        [SerializeField] private Transform _purchaseBody;       

        private const float PUNCH_STRENGTH = 0.25f;
        private const float PUNCH_TWEEN_DURATION = 0.4f;
        private const Ease PUNCH_EASE = Ease.InOutSine;

        private string _punchTweenID;

        [HideInInspector]
        public UnityEvent OnPurchaseActivated = new UnityEvent();

        private void Awake()
        {
            _punchTweenID = GetInstanceID() + "PunchTweenID";
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            TilePurchaseManager.Instance.AddTilePurchase(this);
            MouseEventDetector.OnMouseDowned.AddListener(Purchase);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            TilePurchaseManager.Instance.RemoveTilePurchase(this);
            MouseEventDetector.OnMouseDowned.RemoveListener(Purchase);
        }

        public void ActivatePurchase()
        {
            if (IsPurchaseActivated)
                return;

            IsPurchaseActivated = true;
            OnPurchaseActivated.Invoke();
        }

        private void Purchase()
        {
            if (IsPurchased)
                return;

            if (!IsPurchaseActivated)
                return;         

            if (HasPlayerEnoughMoney())
                Tile.PurchaseTile();
            else
                PunchTween();
        }        

        private void PunchTween()
        {
            DOTween.Complete(_punchTweenID);
            _purchaseBody.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_TWEEN_DURATION, vibrato: 1).SetEase(PUNCH_EASE).SetId(_punchTweenID);
        }

        private bool HasPlayerEnoughMoney()
        {
            return GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] >= Tile.TilePrice;
        }
    }
}
