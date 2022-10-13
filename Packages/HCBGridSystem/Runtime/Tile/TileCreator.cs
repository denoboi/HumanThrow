using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
using UnityEditor;
#endif

namespace HCB.GridSystem.Samples
{
    public class TileCreator : MonoBehaviour
    {
        public List<TileBase> Tiles { get => _tiles; private set => _tiles = value; }

        [SerializeField] private int _row;
        [SerializeField] private int _column;
        [Space(10)]
        [SerializeField] private float _cellSize;   
        [Space(10)]
        [SerializeField] private Transform _tileParent;
        [SerializeField] private GameObject _tilePrefab;
        [Space(10)]
        [SerializeField] private TileCreateData _tileCreateData;

        [ReadOnly]
        [FoldoutGroup("Cache")]
        [SerializeField] private List<TileBase> _tiles = new List<TileBase>();

#if UNITY_EDITOR
        [Button]
        private void CreateTiles()
        {
            if (PrefabStageUtility.GetPrefabStage(gameObject) != null)
            {
                Debug.LogError("You can not create tiles in prefab mode");
                return;
            }

            ClearTiles();

            Vector3 centerPosition = new Vector3(_column / 2f - 0.5f, 0f, _row / 2f - 0.5f) * _cellSize;
            Vector3 offsetPosition = (_tileParent.position - centerPosition);

            for (int z = 0; z < _row; z++)
            {
                for (int x = 0; x < _column; x++)
                {                    
                    Vector3 spawnPosition = new Vector3(x, 0, z) * _cellSize + offsetPosition;
                    TileBase tile = InstantiateTile(spawnPosition, " " + x + "" + z);                    

                    Vector2Int grid = new Vector2Int(x, z);
                    TilePriceByGrid tilePriceByGrid = GetTilePriceByGrid(grid);

                    bool isOffset = (x + z) % 2 != 1;
                    if (tilePriceByGrid != null && tilePriceByGrid.TilePrice > 0)
                    {
                        tile.Initialize(grid, false, tilePriceByGrid.TilePrice, isOffset);
                    }

                    else
                    {
                        tile.Initialize(grid, true, 0f, isOffset);
                    }

                    Tiles.Add(tile);
                }
            }
        }
#endif

#if UNITY_EDITOR
        private TileBase InstantiateTile(Vector3 spawnPosition, string nameSuffix) 
        {

            TileBase tile = (PrefabUtility.InstantiatePrefab(_tilePrefab) as GameObject).GetComponent<TileBase>();
            tile.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            tile.transform.SetParent(_tileParent);
            tile.gameObject.name += nameSuffix;
            return tile;
        }
#endif

        private TilePriceByGrid GetTilePriceByGrid(Vector2Int grid) 
        {
            TilePriceByGrid tilePriceByGrid = null;
            foreach (var tileData in _tileCreateData.Tiles)
            {
                if (tileData.Grid == grid)
                {
                    tilePriceByGrid = tileData;
                    break;
                }
            }
            return tilePriceByGrid;
        }         

        private void ClearTiles()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                if (Tiles[i] != null)
                    DestroyImmediate(Tiles[i].gameObject);
            }
            Tiles.Clear();
        }
    }
}
