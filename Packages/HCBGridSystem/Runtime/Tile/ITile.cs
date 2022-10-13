using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public interface ITile
    {
        Transform T { get; }
        Vector2Int Grid { get; }
        string TileID { get; }
        bool IsOccupied { get; }
        bool IsPurchased { get; }
        bool IsAvailable { get; }
        IPlaceable PlacedItem { get; }
        void StartInteraction(IPlaceable placeable);
        void EndInteraction(IPlaceable placeable);
        void PlaceItem(IPlaceable placeable);
        void RemoveItem(IPlaceable placeable);
        void LoadTileData(TileSaveData tileSaveData);
    }
}
