using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle")]
public class PuzzlesDataBase : ScriptableObject
{
    public TypeOfPuzzles TypeOfPuzzle;
    public List<PieceController> PuzzlesList = new List<PieceController>();
}

[Serializable]
public enum TypeOfPuzzles{Animals, Fruits, Vehicles}
