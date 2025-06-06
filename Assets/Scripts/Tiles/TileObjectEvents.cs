using UnityEngine;

public class TileObjectEvents : MonoBehaviour
{
    [Header("ToxicGas")]
    [SerializeField] private int _toxicGasTicksNumber;
    [SerializeField] private GameObject _view;
    public int GetToxicGasTicks() => _toxicGasTicksNumber;
    public bool IsToxicGasActive() => _toxicGasTicksNumber > 0;

    private void Start()
    {
        CustomEvents.OnTimeTick += EventTick;
    }

    public void ActiveEvent(int ticksNumber)
    {
        if (IsToxicGasActive()) return;
        _toxicGasTicksNumber = ticksNumber;
        _view.SetActive(true);
    }

    private void EventTick()
    {
        if (_toxicGasTicksNumber == 0) return;

        if (_toxicGasTicksNumber == 1)
        {
            UnactiveEvent();
        }

        _toxicGasTicksNumber--;
    }

    public void UnactiveEvent()
    {
        _view.SetActive(false);
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= EventTick;
    }
}
