using DG.Tweening;
using HCB.Core;
using HCB.WeaponSystem;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using TMPro;
using UnityEngine;

public class FinalTarget : MonoBehaviour
{
    BoxCollider _collider;
    public int _durability = 2;
    TextMeshProUGUI _durabilityText;
    
    private const string BIGMONEY_POOL_ID = "BigMoney";
    private const string MONEY_POOL_ID = "Money";
    private const float SPAWN_OFFSET = 0.5f;
    private const int MONEY_VALUE = 1;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _durabilityText = GetComponentInChildren<TextMeshProUGUI>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactor _player = other.GetComponentInParent<Interactor>();
        Projectile projectile = other.GetComponentInParent<Projectile>();
        if (_player != null)
        {
            
            Destroy(_collider);
            Player.Instance.IsWin = true;
            GameManager.Instance.CompeleteStage(true);
        }

        else if (projectile != null)
        {
           
            FireControl(other.gameObject);
            
        }
    }
    public void SetDurability(int durability)
    {
        _durability = durability;
        _durabilityText.text = _durability.ToString();
    }
    void FireControl(GameObject bullet)
    {
        //DOTween.Kill(bullet.transform);
        //Destroy(bullet);
        
        _durability--;
        HapticManager.Haptic(HapticTypes.RigidImpact);
        _durabilityText.text = _durability.ToString();

        if (_durability == 0)
        {
            Destroy(_collider);
            transform.DOLocalRotate(new Vector3(-90, 180, 0), 0.2f);
            _durabilityText.gameObject.SetActive(false);
            SpawnMoney();
        }

    }
    
    private void SpawnMoney() 
    {
        Vector3 spawnPoint = transform.position + Vector3.up * SPAWN_OFFSET;
        Money money = PoolingSystem.Instance.InstantiateAPS(BIGMONEY_POOL_ID, spawnPoint).GetComponentInChildren<Money>();
        money.Initialize(MONEY_VALUE);
    }
}
