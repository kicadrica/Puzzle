using DG.Tweening;
using UnityEngine;


public class StarsController : MonoBehaviour
{
    [SerializeField] private GameObject firstStar;
    [SerializeField] private GameObject secondStar;
    [SerializeField] private GameObject thirdStar;
    
    private const float StartScale = 0.5f;
    private void Start()
    {
        StartStarsValue();
        GameController.OnPuzzleCountChanged += PlaceStars;
    }
    
    private void OnDestroy()
    {
        GameController.OnPuzzleCountChanged -= PlaceStars;
    }
    
    private void StartStarsValue()
    {
        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);

        firstStar.transform.localScale = Vector3.one * StartScale;
        secondStar.transform.localScale = Vector3.one * StartScale;
        thirdStar.transform.localScale =  Vector3.one * StartScale;
    }

    private void PlaceStars(int puzzleCount)
    {
        switch (puzzleCount) {
            case 0:
                StartStarsValue();
                break;
            case 1:
                ShowStar(firstStar, TypeOfSound.FirstStar);
                break;
            case 3:
                ShowStar(secondStar, TypeOfSound.SecondStar);
                break;
            case 4:
                ShowStar(thirdStar, TypeOfSound.ThirdStar);
                break;
        }
    }

    private void ShowStar(GameObject star, TypeOfSound sound)
    {
        star.SetActive(true);
        star.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        AudioManager.Instance.PlaySound(sound);
    }

    
    
}
