using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChangeVideoTime : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private VideoPlayer _vPlayer;
    private Slider _slider;
    [HideInInspector] public bool m_isChanged;

    private void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        m_isChanged = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _vPlayer.time = _slider.value;
        m_isChanged = true;
    }
}
