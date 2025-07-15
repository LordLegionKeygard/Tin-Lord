using UnityEngine;

public class MissionQuantSystem : MonoBehaviour
{
    [SerializeField] private float _quants;
    public int GetQuants() => (int)_quants;
    public void SetQuants(float value) => _quants = value;
    public void ChangeQuants(float amount) => _quants += amount;
}
