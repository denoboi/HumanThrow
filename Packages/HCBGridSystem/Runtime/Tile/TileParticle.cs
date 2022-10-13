using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class TileParticle : MonoBehaviour
    {
        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;

        private const string PURCHASE_PARTICLE_ID = "PurchaseParticle";

        private void OnEnable()
        {
            Tile.OnTilePurchased.AddListener(OnPurchased);
        }

        private void OnDisable()
        {
            Tile.OnTilePurchased.RemoveListener(OnPurchased);
        }

        private void OnPurchased()
        {
            PoolingSystem.PoolingSystem.Instance.InstantiateAPS(PURCHASE_PARTICLE_ID, transform.position).GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
