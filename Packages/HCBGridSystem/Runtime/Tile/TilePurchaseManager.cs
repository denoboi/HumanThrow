using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using HCB.Core;
using System.Linq;

namespace HCB.GridSystem
{
    public class TilePurchaseManager : Singleton<TilePurchaseManager>
    {
        private List<TilePurchase> _tiles = new List<TilePurchase>();
        public List<TilePurchase> Tiles { get => _tiles; private set => _tiles = value; }

        private void OnEnable()
        {
            GridSystemEventManager.OnTileDataLoaded.AddListener(ActivateNextTilePurchase);
            GridSystemEventManager.OnTilePurchased.AddListener(ActivateNextTilePurchase);
        }

        private void OnDisable()
        {
            GridSystemEventManager.OnTileDataLoaded.RemoveListener(ActivateNextTilePurchase);
            GridSystemEventManager.OnTilePurchased.RemoveListener(ActivateNextTilePurchase);
        }

        public void AddTilePurchase(TilePurchase tile)
        {
            if (Tiles.Contains(tile))
                return;

            Tiles.Add(tile);
        }

        public void RemoveTilePurchase(TilePurchase tile)
        {
            if (!Tiles.Contains(tile))
                return;

            Tiles.Remove(tile);
        }

        private void ActivateNextTilePurchase()
        {
            SortTiles();

            foreach (var tile in Tiles)
            {
                if (tile.IsPurchased)
                    continue;

                tile.ActivatePurchase();
                break;
            }
        }

        private void SortTiles()
        {
            Tiles = Tiles.OrderByDescending(tile => tile.transform.position.z).ThenBy(tile => tile.transform.position.x).ToList();
        }
    }
}
