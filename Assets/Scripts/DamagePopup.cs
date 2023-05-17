using System.Globalization;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float fadeOut;
    
    private readonly Vector3 _moveVector = new(0, 0.5f, 0);

    private bool _setUp;
    private TextMeshProUGUI _textMesh;
    private Color _textColor;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(float damageAmount)
    {
        Debug.Log(_textMesh);
        _textMesh.SetText(damageAmount.ToString(CultureInfo.CurrentCulture));
        _textColor = _textMesh.color;
        _setUp = true;
    }

    private void Update()
    {
        if (!_setUp)
            return;
        transform.position += _moveVector * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            _textColor.a -= Time.deltaTime / fadeOut;
            if (_textColor.a <= 0)
                Destroy(gameObject);
            _textMesh.color = _textColor;
        }
    }
}