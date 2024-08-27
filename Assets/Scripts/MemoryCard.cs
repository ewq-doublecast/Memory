using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardBack;

    [SerializeField]
    private SceneController _sceneController;

    private int _id;

    public int Id
    {
        get
        {
            return _id;
        }
    }

    private void OnMouseDown()
    {
        if (_cardBack.activeSelf)
        {
            _cardBack.SetActive(false);
            _sceneController.RevealCard(this);
        }
    }

    public void SetCard(int id, Sprite sprite)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Unreveal()
    {
        _cardBack.SetActive(true);
    }
}
