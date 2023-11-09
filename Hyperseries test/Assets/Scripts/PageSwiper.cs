using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    #region private
    private Vector3 pageLocation;
    private int pageIndex = 1;
    #endregion

    #region public
    public float percThold = 0.2f;
    public float easing = 0.5f;
    public List<GameObject>  pageNumber = new List<GameObject>();
    [HideInInspector] public bool m_isSwipable;
    #endregion
    void Start()
    {
        pageLocation = transform.position;
        m_isSwipable = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (m_isSwipable)
        {
            float diffX = eventData.pressPosition.x - eventData.position.x;
            float diffY = eventData.pressPosition.y - eventData.position.y;

            if(Mathf.Abs(diffX) > Mathf.Abs(diffY))
                transform.position = pageLocation - new Vector3(diffX, 0, 0);
            else if(Mathf.Abs(diffY) > Mathf.Abs(diffX))
                transform.position = pageLocation - new Vector3(0, diffY, 0);

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_isSwipable)
        {
            float diffX = eventData.pressPosition.x - eventData.position.x;
            float diffY = eventData.pressPosition.y - eventData.position.y;
            Vector3 newLocation = pageLocation;

            float percentageX = (eventData.pressPosition.y - eventData.position.x) / Screen.width;
            if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
            {
                if (Mathf.Abs(percentageX) >= percThold)
                {
                    if (percentageX > 0 && pageIndex != pageNumber.Count)
                    {
                        pageIndex++;
                        newLocation += new Vector3(-Screen.width, 0, 0);
                    }
                    else if (percentageX < 0 && pageIndex != 1)
                    {
                        pageIndex--;
                        newLocation += new Vector3(Screen.width, 0, 0);
                    }
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing));
                    pageLocation = newLocation;
                }
                else
                {
                    StartCoroutine(SmoothMove(transform.position, pageLocation, easing));
                }
            }
            else
            {              
                if(diffY >0)
                    StartCoroutine(SmoothMove(transform.position, pageLocation, easing));
                if (diffY < Screen.height && pageIndex == 1)
                    StartCoroutine(SmoothMove(transform.position, pageLocation, easing));

                
            }
        }
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void OnPresButton()
    {
        Vector3 newLocation = pageLocation;

        if (pageIndex == 1)
        {
            pageIndex = 2;
            newLocation += new Vector3(-Screen.width, 0, 0);
        }
        else if(pageIndex == 2)
        {
            pageIndex = 1;
            newLocation += new Vector3(Screen.width, 0, 0);
        }
        StartCoroutine(SmoothMove(transform.position, newLocation, easing));
        pageLocation = newLocation;
    }


}
