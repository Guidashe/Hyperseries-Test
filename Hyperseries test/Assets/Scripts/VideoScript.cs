using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RenderTexture _videoTex;
    [SerializeField] private VideoPlayer _vPlayer;
    [SerializeField] private Image _currentImage;
    [SerializeField] private Sprite _playImage;
    [SerializeField] private Sprite _pauseImage;
    [SerializeField] private GameObject _timeArea;
    [SerializeField] private GameObject _reader;
    [SerializeField] private ChangeVideoTime _changeVideoTime;
    [SerializeField] private CanvasGroup _thumbsnail;
    [SerializeField] private CanvasGroup _readerCG;

    private RawImage _image;
    private bool _isPlay;
    private Slider _slider;
    private bool _isEnter;

    


    private void Start()
    {
        _image = GetComponent<RawImage>();
        _slider = GetComponentInChildren<Slider>();
        _timeArea.gameObject.SetActive(false);
    }
    public void OnPressButton()
    {
        _isPlay = true;
        _currentImage.sprite = _pauseImage;
        _timeArea.gameObject.SetActive(true);

        if (_vPlayer.isPlaying)
        {
            _isPlay = false;
            _currentImage.sprite = _playImage;
        }
    }


    private void ShowVideo()
    { 
        if(_isPlay)
        {
            _thumbsnail.alpha -= Time.deltaTime;
            _image.texture = _videoTex;
            _vPlayer.Play();
        }
        else
        {
            _vPlayer.Pause();
        }
    }

    private void FadeUIHandler()
    {
        _readerCG.alpha = _isEnter ? _readerCG.alpha += Time.deltaTime : _readerCG.alpha -= Time.deltaTime;
    }

    private IEnumerator TimerVideo()
    {
        if (!_changeVideoTime.m_isChanged)
        {
            _slider.maxValue = (float)_vPlayer.clip.length;
            _slider.value = (float)_vPlayer.clockTime;
        }
        else
        {
            _vPlayer.Pause();
            yield return new WaitForSeconds(1);
            _vPlayer.Play();
            _changeVideoTime.m_isChanged = false;
        }
    }


    private void Update()
    {
        ShowVideo();
        StartCoroutine(TimerVideo());
        FadeUIHandler();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isEnter = false;
    }
}
