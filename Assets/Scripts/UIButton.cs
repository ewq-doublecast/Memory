using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UIButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetObject;

    [SerializeField]
    private string _targetMessage;

    [SerializeField]
    private Color _highlightColor = Color.cyan;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = _highlightColor;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, transform.localScale.z);
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(0.25f, 0.25f, transform.localScale.z);

        if (_targetObject != null)
        {
            _targetObject.SendMessage(_targetMessage);
        }
    }
}
