using UnityEngine;
using UnityEngine.UI;

public class ScrollToCard : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _content;
    private float _fixedShift = 158f; // Фиксированное смещение для каждого элемента после 2-го

    // Этот метод вызывается извне, когда пользователь выбирает карточку
    public void SelectCard(int index, int totalCards)
    {
        ScrollToSelectedCard(index, totalCards);
    }

    private void ScrollToSelectedCard(int index, int totalCards)
    {
        // Если карточка первая или вторая, оставляем ScrollView в начале
        if (index <= 1)
        {
            _scrollRect.horizontalNormalizedPosition = 0; // Показываем первую и вторую карточку
        }
        else
        {
            if(index == totalCards)
            {
                index--;
            }
            // Рассчитываем целевую позицию для сдвига начиная с третьей карточки
            float targetPosition = (index - 1) * _fixedShift;

            // Получаем ширину контента и видимой области
            float totalContentWidth = _content.rect.width;
            float scrollRectWidth = _scrollRect.GetComponent<RectTransform>().rect.width;

            // Вычисляем нормализованную позицию для ScrollRect
            float normalizedPosition = targetPosition / (totalContentWidth - scrollRectWidth);

            // Устанавливаем позицию прокрутки
            _scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(normalizedPosition);
        }
    }
}
