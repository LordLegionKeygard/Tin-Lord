using UnityEngine;
using Pathfinding;
using Zenject;


public class CreatureHealth : BaseHealth
{
    private CreatureKnockBack _creatureKnockBackController;
    public AIPath AiPath { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public BaseAiStateChanger BaseAiStateChanger { get; private set; }
    public CreatureAnimator CreatureAnimator { get; private set; }
    public CreatureLevel CreatureLevel { get; private set; }
    [HideInInspector] public CreatureTakeDamageVFX CreatureTakeDamageVFX;

    public virtual void Awake()
    {
        CreatureLevel = GetComponent<CreatureLevel>();
        CreatureAnimator = GetComponent<CreatureAnimator>();
        _creatureKnockBackController = GetComponent<CreatureKnockBack>();
        CharacterController = GetComponent<CharacterController>();
        CreatureTakeDamageVFX = GetComponent<CreatureTakeDamageVFX>();
        BaseAiStateChanger = GetComponent<BaseAiStateChanger>();
        AiPath = GetComponent<AIPath>();
    }

    public virtual void Start()
    {
        SetStartStats();
    }

    public virtual void SetStartStats()
    {
        MaxHealth = CreatureLevel.EnemyInformation.Health[CreatureLevel.Level()];
        CurrentHealth = MaxHealth;
        // CreatureHealthSlider.SetMaxHealth(MaxHealth);
    }

    public override void CalculateDamage(float damage, KnockBackType knockBackType)
    {
        if (IsDeath()) return;

        TakeDamage(damage, knockBackType);
    }

    private void TakeDamage(float damage, KnockBackType knockBackType)
    {
        _creatureKnockBackController.TakeKnockbackPoints(knockBackType);
        CurrentHealth -= damage;
        UpdateSlider();
    }


    public virtual void UpdateSlider()
    {
        if (_isDeath) return;
        // CreatureHealthSlider.SetHealth(CurrentHealth);
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (CurrentHealth <= 0 && !IsDeath()) Death();
    }

    public virtual void Death()
    {
        _isDeath = true;
        // CreatureHealthSlider.gameObject.SetActive(false);
        CharacterController.enabled = false;
        AiPath.enabled = false;
    }
}
