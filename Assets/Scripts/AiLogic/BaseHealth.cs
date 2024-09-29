using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    protected bool _isDeath;

    public virtual Tile BuildingTile() => null;
    public virtual Transform GetFoutTileTransform() => transform;

    public virtual bool IsDeath() => _isDeath; 

    public virtual void CalculateDamage(float damage, KnockBackType knockBackType)
    {

    }

    public virtual void Heal(float amount)
    {
        
    }
}
