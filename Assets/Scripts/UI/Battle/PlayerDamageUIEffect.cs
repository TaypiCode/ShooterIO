using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageUIEffect : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private Image _image;
    [SerializeField] private float _showTime;
    [SerializeField] private float _maxAlpha;
    private Timer _timer;
    private void Start()
    {
        _timer = gameObject.AddComponent<Timer>();
    }
    private void Update()
    {
        _color.a = Mathf.Clamp(_timer.GetTime(), 0, _maxAlpha);
        _image.color = _color;
    }
    public void GetDamage()
    {
        _timer.SetTimer(_showTime);
    }
}
