using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    [SerializeField] private PageSwiper _swiper;

    #region Rotation
    [SerializeField] private float m_RotTime;
    [SerializeField] private float m_rotaionDegree;
    #endregion

    #region Stats
    [SerializeField] private GameObject _prefabsStats;
    [SerializeField] private Transform _statTransform;
    private bool isDrop = false;
    #endregion

    #region MovePanelOthersVideos
    [SerializeField] private float m_delayToMove;
    [SerializeField] private float m_offsetMoveOthersVideos;
    [SerializeField] private GameObject _othersVideos;
    private Vector3 m_endMoveOthersVideos;
    private Vector3 _origin;
    #endregion

    void Awake()
    {
        m_endMoveOthersVideos = new Vector3(_othersVideos.transform.position.x - Screen.width, _othersVideos.transform.position.y - m_offsetMoveOthersVideos, 0);
        _origin = new Vector3(_othersVideos.transform.position.x - Screen.width, _othersVideos.transform.position.y , 0);
    }

    private IEnumerator ArrowRotation(float degrees)
    {

        float t = 0;

        while(t <= 1)
        {
            t += Time.deltaTime / m_RotTime;
            Quaternion rotation = Quaternion.Euler(Vector3.forward * degrees);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation , Mathf.SmoothStep(0f, 1f, t));
            yield return null;           
        }
    }

    private IEnumerator MoveOtherVideos(Vector3 startPos, Vector3 endPos)
    {
        float t = 0;

        while(t <= 1)
        {
            t += Time.deltaTime / m_delayToMove;
            _othersVideos.transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public IEnumerator Stop()
    {
        while (!_swiper.m_isSwipable)
        {
            yield return new WaitForSeconds(m_delayToMove);
            _swiper.m_isSwipable = true;
        }
    }

    public void ShownDropDown()
    {
        _swiper.m_isSwipable = false;

        if (!isDrop)
        {
            isDrop = true;
            StartCoroutine(Stop());
            StartCoroutine(ArrowRotation(m_rotaionDegree));
            StartCoroutine(MoveOtherVideos(_othersVideos.transform.position, m_endMoveOthersVideos));
            _prefabsStats.SetActive(true);
        }
        else
        {
            HideDropDown();
        }
    }

    public void HideDropDown()
    {
        isDrop = false;
        StartCoroutine(Stop());
        StartCoroutine(ArrowRotation(0f));
        StartCoroutine(MoveOtherVideos(m_endMoveOthersVideos, _origin));
        _prefabsStats.SetActive(false);
    }
}
