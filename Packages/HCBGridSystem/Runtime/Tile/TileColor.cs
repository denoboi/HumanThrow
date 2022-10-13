using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class TileColor : MonoBehaviour
    {
        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;

        private SpriteRenderer _renderer;
        private SpriteRenderer SpriteRenderer => _renderer == null ? _renderer = GetComponentInChildren<SpriteRenderer>() : _renderer;

        private float DefaultAlpha => SpriteRenderer.color.a;

        [SerializeField] private TileColorData _colorData;

        private void OnEnable()
        {
            GridSystemEventManager.OnTileDataLoaded.AddListener(UpdateTileColor);
            Tile.OnTilePurchased.AddListener(UpdateTileColor);
            Tile.OnInteractionStart.AddListener(SetHighlightColor);
            Tile.OnInteractionEnd.AddListener(() => SetColor(GetDefaultColor()));
        }

        private void OnDisable()
        {
            GridSystemEventManager.OnTileDataLoaded.RemoveListener(UpdateTileColor);
            Tile.OnTilePurchased.RemoveListener(UpdateTileColor);
            Tile.OnInteractionStart.RemoveListener(SetHighlightColor);
            Tile.OnInteractionEnd.RemoveListener(() => SetColor(GetDefaultColor()));
        }

        //Listens Tile Editor Event   
        public void UpdateTileColor()
        {
            SetColor(GetDefaultColor());
        }

        public void SetColor(Color color)
        {
            color.a = DefaultAlpha;
            SpriteRenderer.color = color;
        }

        private void SetHighlightColor()
        {
            SetColor(GetHighlightColor());
        }

        private Color GetDefaultColor()
        {
            Color color;
            if (Tile.IsPurchased)
            {
                color = Tile.IsOffset ? _colorData.ActiveOffsetColor : _colorData.ActiveDefaultColor;
            }

            else
            {
                color = Tile.IsOffset ? _colorData.DeactiveOffsetColor : _colorData.DeactiveDefaultColor;
            }
            return color;
        }

        private Color GetHighlightColor()
        {
            bool isAvailable = false;

            if (Tile.IsPurchased && (Tile.PlacedItem == null || Tile.PlacedItem == Tile.LastInteractedItem))
            {
                isAvailable = true;
            }

            Color color = isAvailable ? _colorData.ActiveHighlightColor : _colorData.DeactiveHighlightColor;
            return color;
        }
    }
}
