using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static TypeOfPuzzles PuzzleType = TypeOfPuzzles.Fruits;
    public static event Action<int> OnPuzzleCountChanged;
    
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button choiceAnimals;
    [SerializeField] private Button choiceFruits;
    [SerializeField] private Button choiceVehicles;
    [SerializeField] private PuzzleCreator puzzleCreator;
    
    private const float DelayBetweenLvl = 2f;

    private int _puzzleCount;
    
    private int CompletedPuzzles {
        get => _puzzleCount;
        set {
            _puzzleCount = value;
            OnPuzzleCountChanged?.Invoke(_puzzleCount);
            if (_puzzleCount == PuzzleCreator.PuzzleCount) {
                StartCoroutine(CompleteAllPuzzles());
            }
        }
    }
    
    private void Start()
    {
        PieceController.OnPuzzleComplete += CountCompletedPuzzles;
        
        choiceAnimals.onClick.AddListener(()=>OnTypeChanged(TypeOfPuzzles.Animals));
        choiceFruits.onClick.AddListener(()=>OnTypeChanged(TypeOfPuzzles.Fruits));
        choiceVehicles.onClick.AddListener(()=>OnTypeChanged(TypeOfPuzzles.Vehicles));
    }
    
    private void OnDestroy()
    {
        PieceController.OnPuzzleComplete -= CountCompletedPuzzles;
    }

    private void OnTypeChanged(TypeOfPuzzles type)
    {
        PuzzleType = type;
        CompletedPuzzles = 0;
        puzzleCreator.GenerateField();
        choicePanel.SetActive(false);
    }
    
    private void CountCompletedPuzzles()
    {
        CompletedPuzzles++;
    }
    
    private IEnumerator CompleteAllPuzzles()
    {
        yield return new WaitForSeconds(DelayBetweenLvl);

        CompletedPuzzles = 0;
        puzzleCreator.GenerateField();
    }
    
}
