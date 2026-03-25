using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataBase : MonoBehaviour
{
    [SerializeField] private MouseHover m_mouseHover;
    [SerializeField] private List<Unit> m_units;

    private void OnEnable()
    {
        foreach (var unit in m_units)
        {

        }
    }

    private void Awake()
    {
        
    }
}
