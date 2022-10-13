using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.GridSystem
{
    public class TileCreateData : SerializedScriptableObject
    {
        [Tooltip("Add only non-purchased tiles.")]
        public List<TilePriceByGrid> Tiles = new List<TilePriceByGrid>();
    }

    [System.Serializable]
    public class TilePriceByGrid
    {
        public Vector2Int Grid;
        public float TilePrice;
    }
}
