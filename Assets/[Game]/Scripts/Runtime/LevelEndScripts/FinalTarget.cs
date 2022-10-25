using DG.Tweening;
using HCB.Core;
using HCB.WeaponSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalTarget : MonoBehaviour
{
    BoxCollider _collider;
    public int _durability = 2;
    TextMeshProUGUI _durabilityText;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _durabilityText = GetComponentInChildren<TextMeshProUGUI>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponentInParent<Player>();
        Projectile projectile = other.GetComponent<Projectile>();
        if (_player != null)
        {
            
            Destroy(_collider);
            _player.IsWin = true;
            GameManager.Instance.OnStageSuccess.Invoke();
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
        DOTween.Kill(bullet.transform);
        Destroy(bullet);
        
        _durability--;
        _durabilityText.text = _durability.ToString();

        if (_durability == 0)
        {
            Destroy(_collider);
            transform.DOLocalRotate(new Vector3(-90, 180, 0), 0.2f);
            _durabilityText.gameObject.SetActive(false);
            
        }

    }
}
