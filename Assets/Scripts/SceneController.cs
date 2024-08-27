using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private const int GridRows = 2;
    private const int GridColumns = 4;
    private const string SceneName = "SampleScene";

    [SerializeField]
    private float OffsetX = 2.0f;

    [SerializeField]
    private float OffsetY = 2.5f;

    [SerializeField]
    private MemoryCard _originalCard;

    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private TextMeshPro _scoreLabel;

    private int _score = 0;
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    public bool IsCanReveal
    {
        get
        {
            return _secondRevealed == null;
        }
    }

    private void Start()
    {
        Vector3 startPosition = _originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < GridColumns; i++)
        {
            for (int j = 0; j < GridRows; j++)
            {
                MemoryCard memoryCard;

                if (i == 0 && j == 0)
                {
                    memoryCard = _originalCard;
                }
                else
                {
                    memoryCard = Instantiate(_originalCard);
                }

                int index = j * GridColumns + i;
                int id = numbers[index];
                memoryCard.SetCard(id, _sprites[id]);

                float positionX = (i * OffsetX) + startPosition.x;
                float positionY = -(j * OffsetY) + startPosition.y;

                memoryCard.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }
    }

    public void RevealCard(MemoryCard memoryCard)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = memoryCard;
        }
        else
        {
            _secondRevealed = memoryCard;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _score++;
            _scoreLabel.text = $"Score: {_score}";
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneName);
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int randomIndex = Random.Range(i, newArray.Length);
            newArray[i] = newArray[randomIndex];
            newArray[randomIndex] = temp;
        }

        return newArray;
    }
}
