using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetStats : MonoBehaviour
{
    [SerializeField] private SaveStats _saveStatsScript;
    [SerializeField] private List<TextMeshProUGUI> _showStats;


    void Update()
    {
        if (_saveStatsScript.m_isSave)
        {
            _showStats[0].text = _saveStatsScript.m_episodeName;
            _showStats[1].text = _saveStatsScript.m_numberOfView;
        }
    }
}
