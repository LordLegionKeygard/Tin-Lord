using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    protected bool _isDeath;
    protected BaseSlider _healthSlider;
    protected GameObject _healthSliderObject;

    public virtual Tile BuildingTile() => null;
    public virtual Transform GetFoutTileTransform() => transform;

    public virtual bool IsDeath() => _isDeath;

    public virtual Transform GetTransform() => transform;

    public virtual void CalculateDamage(float damage, float knockBackPoints)
    {
        if (IsDeath()) return;
        TakeDamage(damage, knockBackPoints);
    }

    public virtual void TakeDamage(float damage, float knockBackPoints)
    {
        CurrentHealth -= damage;
        UpdateSlider();
    }

    public virtual void UpdateSlider()
    {
        if (IsDeath()) return;
        _healthSlider.SetValue(CurrentHealth);
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
}
