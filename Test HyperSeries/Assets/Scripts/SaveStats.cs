using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveStats : MonoBehaviour
{
    public string m_episodeName;
    public string m_numberOfView;
    [HideInInspector] public bool m_isSave;


    [SerializeField] private TMP_InputField _textmeshPro_obj;
    [SerializeField] private TMP_InputField _textmeshPro_obj2;
    [SerializeField] private GameObject _prefabs;
    

    public void onSave()
    {
        m_episodeName = _textmeshPro_obj.text;
        m_numberOfView = _textmeshPro_obj2.text;

        _textmeshPro_obj.text = "";
        _textmeshPro_obj2.text = "";

        m_isSave = true;
    }
}
