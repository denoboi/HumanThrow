using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HCB.Core;
using Sirenix.OdinInspector;


namespace HCB.GridSystem
{
    public abstract class TileBase : MonoBehaviour, ITile
    {
        public Transform T => transform;
        public bool IsOccupied { get; protected set; }
        public bool IsAvailable { get => !IsOccupied && IsPurchased; }
        public IPlaceable LastInteractedItem { get; private set; }
        public IPlaceable PlacedItem { get; protected set; }
        public string TileID => Grid.x.ToString() + Grid.y.ToString();

        [ReadOnly]
        [FoldoutGroup("Cache")]
        [SerializeField] private bool _isPurchased;
        public bool IsPurchased { get => _isPurchased; private set => _isPurchased = value; }

        [ReadOnly]
        [FoldoutGroup("Cache")]
        [SerializeField] private float _tilePrice;
        public float TilePrice { get => _tilePrice; private set => _tilePrice = value; }

        [ReadOnly]
        [FoldoutGroup("Cache")]
        [SerializeField] private Vector2Int _grid = Vector2Int.zero;
        public Vector2Int Grid { get => _grid; private set => _grid = value; }

        [ReadOnly]
        [FoldoutGroup("Cache")]
        [SerializeField] private bool _isOffset;
        public bool IsOffset { get => _isOffset; private set => _isOffset = value; }        

        //Editor Event
        [SerializeField] private UnityEvent OnInitialized = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnInteractionStart = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnInteractionEnd = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnItemPlaced = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnItemRemoved = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnTilePurchased = new UnityEvent();

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            TileManager.Instance.AddTile(this);
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            TileManager.Instance.RemoveTile(this);
        }

        //Invokes in editor when its created
        public void Initialize(Vector2Int grid, bool isPurchased, float tilePrice, bool isOffset)
        {
            Grid = grid;            
            IsOffset = isOffset;
            IsPurchased = isPurchased;
            TilePrice = tilePrice;
            OnInitialized.Invoke();
        }

        public void StartInteraction(IPlaceable placeable)
        {
            LastInteractedItem = placeable;
            OnInteractionStart.Invoke();
        }

        public void EndInteraction(IPlaceable placeable)
        {
            if (LastInteractedItem == placeable)
                LastInteractedItem = null;

            OnInteractionEnd.Invoke();
        }

        public void PlaceItem(IPlaceable placeable)
        {
            if (IsOccupied || PlacedItem == placeable)
                return;

            IsOccupied = true;
            PlacedItem = placeable;
            OnItemPlaced.Invoke();
        }

        public void RemoveItem(IPlaceable placeable)
        {
            if (!IsOccupied || PlacedItem != placeable)
                return;

            IsOccupied = false;
            PlacedItem = null;
            OnItemRemoved.Invoke();
        }

        public void LoadTileData(TileSaveData tileSaveData)
        {
            IsPurchased = tileSaveData.IsPurchased;
        }

        public void PurchaseTile()
        {
            if (IsPurchased)
                return;

            IsPurchased = true;

            OnTilePurchased.Invoke();
            GridSystemEventManager.OnTilePurchased.Invoke();

            GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] -= TilePrice;
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}
