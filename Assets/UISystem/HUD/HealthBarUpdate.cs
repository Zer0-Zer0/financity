using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour
{
    [SerializeField] RectTransform HealthBar;
    public void UpdateHealthBar()
    {
        float _maxScale = DataManager.playerData.GetMaxHealth();
        float _rawCurrentScale = DataManager.playerData.GetCurrentHealth();

        float _currentScale = _rawCurrentScale / _maxScale;

        HealthBar.LeanScaleX(_currentScale, 1f);
    }
}
