using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartLevel_Text;

    private GameManager _gameManager;

    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartLevel_Text.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is Null in UIManager");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
        if(currentLives == 0)
        {
            _gameoverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
            _restartLevel_Text.gameObject.SetActive(true);
            _gameManager.GameOver();
        }
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
