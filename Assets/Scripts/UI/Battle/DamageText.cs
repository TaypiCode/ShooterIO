using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _format;
    [SerializeField] private float _showTime;
    private float _timer = 0;
    private float _damage = 0;
    private void Start()
    {
        StartCoroutine(Timer());
    }
    public void ShowDamage(float damage)
    {
        _timer = _showTime;
        _damage += damage;
        _text.text = string.Format(_format, _damage.ToString());
    }
    private IEnumerator Timer()
    {
        while (true)
        {
            if (_timer <= 0 && _text.text != "")
            {
                _damage = 0;
                _text.text = "";
            }
            _timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
