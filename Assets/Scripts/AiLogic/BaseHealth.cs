using UnityEngine;

public class BaseHealth : MonoBehaviour
{
	protected float _maxHealth;
	protected float _currentHealth;
	protected bool _isDeath;
	public bool _isCanTarget = true;
	protected BaseSlider _healthSlider;
	protected GameObject _healthSliderObject;

	public float GetCurrentHealth() => _currentHealth;
	public float GetMaxHealth() => _maxHealth;
	public virtual float CalculateHealthFromPercent(int percent) => _maxHealth * percent / 100;
	public virtual Tile BuildingTile() => null;
	public virtual Transform GetFoutTileTransform() => transform;
	public virtual bool IsDeath() => _isDeath;
	public virtual bool IsCanTarget() => _isCanTarget;
	public virtual Transform GetTransform() => transform;

	public virtual void CalculateDamage(float damage, float knockBackPoints)
	{
		if (IsDeath()) return;
		TakeDamage(damage, knockBackPoints);
	}

	public virtual void TakeDamage(float damage, float knockBackPoints)
	{
		if (!_isCanTarget) return;
		
		_currentHealth -= damage;
		UpdateSlider();
	}

	public virtual void UpdateSlider()
	{
		if (IsDeath()) return;
		_healthSlider.SetValue(_currentHealth);
		CheckDeath();
	}

	public virtual void LoadHealth(float newHealth)
    {

    }

	public void CheckDeath()
	{
		if (_currentHealth <= 0 && !IsDeath()) Death();
	}

	public virtual void Death()
	{
		DestroyHealthSlider();
		_isDeath = true;
	}

	public void SetCanTarget(bool state) => _isCanTarget = state;

	public void HideSlider()
	{
		_healthSlider.HideSlider();
	}

	public void DestroyHealthSlider()
	{
		if (_healthSliderObject == null) return;
		Destroy(_healthSliderObject);
	}
}