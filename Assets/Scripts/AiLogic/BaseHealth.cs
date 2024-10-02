using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    protected bool _isDeath;
    protected HealthSlider _healthSlider;
    protected GameObject _healthSliderObject;

    public virtual Tile BuildingTile() => null;
    public virtual Transform GetFoutTileTransform() => transform;

    public virtual bool IsDeath() => _isDeath;

    public virtual void CalculateDamage(float damage, KnockBackType knockBackType)
    {
        if (IsDeath()) return;
        TakeDamage(damage, knockBackType);
    }

    public virtual void TakeDamage(float damage, KnockBackType knockBackType)
    {
        CurrentHealth -= damage;
        UpdateSlider();
    }

    public virtual void UpdateSlider()
    {
        if (IsDeath()) return;
        _healthSlider.SetHealth(CurrentHealth);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0 && !IsDeath()) Death();
    }

    public virtual void Death()
    {
        DestroyHealthSlider();
        _isDeath = true;
    }

    public void DestroyHealthSlider()
    {
        if (_healthSliderObject == null) return;
        Destroy(_healthSliderObject);
    }



    public virtual void Heal(float amount)
    {

    }
}
