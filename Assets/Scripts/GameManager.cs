using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Text pointsText;

    private int points = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (pointsText != null && points > 1)
        {
            pointsText.text = "Puntos: " + points.ToString();
        }
    }
}
