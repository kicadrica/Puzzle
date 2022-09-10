using System.Collections.Generic;
using UnityEngine;

public class PuzzleCreator : MonoBehaviour
{
    [SerializeField] private Transform[] pieceOfPuzzlePoints;
    [SerializeField] private List<Transform> placeOfPuzzlePoints;
    [SerializeField] private Transform pieceOfPuzzleParent;
    [SerializeField] private Transform placeOfPuzzleParent;
    [SerializeField] private PuzzlesDataBase[] allPuzzles;

    [HideInInspector] public const int PuzzleCount = 4;
    private readonly List<PieceController> _dynamicPuzzlesList = new List<PieceController>();
    private readonly List<PieceController> _puzzlesOnScene = new List<PieceController>();

    private void Start()
    {
        GenerateField();
    }


    public void GenerateField()
    {
        foreach (Transform child  in pieceOfPuzzleParent) {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child  in placeOfPuzzleParent) {
            Destroy(child.gameObject);
        }
       
        _dynamicPuzzlesList.Clear();
        _dynamicPuzzlesList.AddRange(ReturnTypeOfPuzzles(GameController.PuzzleType));

        Shuffle(_dynamicPuzzlesList);
        if (_puzzlesOnScene.Count > 0) {
            for (int i = 0; i < _puzzlesOnScene.Count; i++) {
                _dynamicPuzzlesList.Remove(_puzzlesOnScene[i]);
            }
        }

        _puzzlesOnScene.Clear();
        InstantiatePuzzle();
    }

    private void InstantiatePuzzle()
    {
        Shuffle(placeOfPuzzlePoints);
        
        for (int i = 0; i < PuzzleCount; i++) {
            var pieceOfPuzzle = Instantiate(_dynamicPuzzlesList[i], pieceOfPuzzleParent);
            pieceOfPuzzle.transform.position = new Vector2(pieceOfPuzzlePoints[i].position.x, 
            pieceOfPuzzlePoints[i].position.y);

            var placeOfPuzzle = pieceOfPuzzle.PieceOfPuzzlePlace.transform;
            placeOfPuzzle.SetParent(placeOfPuzzleParent);
            placeOfPuzzle.position = new Vector2(placeOfPuzzlePoints[i].position.x,
            placeOfPuzzlePoints[i].position.y);
            
            _puzzlesOnScene.Add(_dynamicPuzzlesList[i]);
        }
    }

    private List<PieceController> ReturnTypeOfPuzzles(TypeOfPuzzles type)
    {
        for (int i = 0; i < allPuzzles.Length; i++) {
            if (allPuzzles[i].TypeOfPuzzle == type) {
                return allPuzzles[i].PuzzlesList;
            }
        }
        return null;
    }

    private void Shuffle<T>( List<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }

    
    
  


}
