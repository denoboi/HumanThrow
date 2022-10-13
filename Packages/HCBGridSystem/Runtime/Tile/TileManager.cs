using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using HCB.Core;

using System.Linq;

namespace HCB.GridSystem
{
    public class TileManager : Singleton<TileManager>
    {
        private List<ITile> _tiles = new List<ITile>();
        public List<ITile> Tiles { get => _tiles; private set => _tiles = value; }

        private ITile[,] _tileGrid = new ITile[0, 0];
        public ITile[,] TileGrid { get => _tileGrid; private set => _tileGrid = value; }

        private Dictionary<string, ITile> _tilesByID = new Dictionary<string, ITile>();
        public Dictionary<string, ITile> TilesByID { get => _tilesByID; private set => _tilesByID = value; }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(LoadTiles);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(LoadTiles);
        }

        private void Start()
        {
            SubscribeGameManager();
        }

        public void AddTile(ITile tile)
        {
            if (TilesByID.ContainsKey(tile.TileID))
                return;

            Tiles.Add(tile);
            TilesByID.Add(tile.TileID, tile);
        }

        public void RemoveTile(ITile tile)
        {
            if (!TilesByID.ContainsKey(tile.TileID))
                return;

            Tiles.Remove(tile);
            TilesByID.Remove(tile.TileID);
        }

        public ITile GetRandomTile()
        {
            ITile randomTile = null;
            Tiles.Shuffle();

            foreach (var tile in Tiles)
            {
                if (tile.IsAvailable)
                {
                    randomTile = tile;
                    break;
                }
            }
            return randomTile;
        }

        public ITile GetTile(string tileID)
        {
            try
            {
                ITile tile = TilesByID[tileID];
                return tile;
            }
            catch
            {
                return null;
            }
        }

        public ITile GetTile(int x, int y)
        {
            try
            {
                ITile tile = TileGrid[x, y];
                return tile;
            }
            catch
            {
                return null;
            }
        }

        public bool HasAvailableTile()
        {
            bool hasTile = false;

            foreach (var tile in Tiles)
            {
                if (tile.IsAvailable)
                {
                    hasTile = true;
                    break;
                }
            }

            return hasTile;
        }

        public int GetAvailableTileCount()
        {
            int available = 0;
            foreach (var tile in Tiles)
            {
                if (tile.IsAvailable)
                {
                    available++;
                }
            }
            return available;
        }

        private void SetTileGrid()
        {
            int x = Tiles.Max(tile => tile.Grid.x) + 1;
            int y = Tiles.Max(tile => tile.Grid.y) + 1;

            TileGrid = new ITile[x, y];

            foreach (var tile in Tiles)
            {
                TileGrid[tile.Grid.x, tile.Grid.y] = tile;
            }
        }

        private void SaveTiles()
        {
            List<TileSaveData> tileSaveDatas = new List<TileSaveData>();
            foreach (var tile in Tiles)
            {
                tileSaveDatas.Add(new TileSaveData(tile.TileID, tile.IsPurchased));
            }
            GridSystemManager.Instance.GridSystemData.TileSaveDatas = tileSaveDatas;
        }

        private void LoadTiles()
        {
            foreach (var tileSaveData in GridSystemManager.Instance.GridSystemData.TileSaveDatas)
            {
                ITile tile = GetTile(tileSaveData.TileID);
                if (tile == null)
                    continue;

                tile.LoadTileData(tileSaveData);
            }

            SetTileGrid();
            GridSystemEventManager.OnTileDataLoaded.Invoke();
        }

        private void SubscribeGameManager()
        {
            GridSystemManager.Instance.OnApplicationPauseActions.Add(() => SaveTiles());
            GridSystemManager.Instance.OnApplicationQuitActions.Add(() => SaveTiles());
        }
    }
}
