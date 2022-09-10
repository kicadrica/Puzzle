using System;
using UnityEngine;
using DG.Tweening;

public class PieceController : MonoBehaviour
{
    public static event Action OnPuzzleComplete;
    public PlaceController PieceOfPuzzlePlace;
    
    private PlaceController _place;
    private bool _isLocked = false;
    
    private SpriteRenderer _sr;
    private static int _newSortingOrder = 0;
    
    private Camera _camera;
    private float _posX;
    private float _posY;

    private void Start()
    {
        _camera = Camera.main;
        _sr = GetComponent<SpriteRenderer>();
    }
    
    private void OnMouseDown()
    {
        if (_isLocked) return;
        _posX = _camera.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        _posY = _camera.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        transform.DOScale(Vector3.one * 1.2f, 0.3f).SetEase(Ease.OutBounce);
        
        _newSortingOrder++;
        _sr.sortingOrder = _newSortingOrder;
    }
    private void OnMouseDrag()
    {
        if (_isLocked) return;
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x - _posX, mousePos.y - _posY);

    }

    private void OnMouseUp()
    {
        transform.DOScale(Vector3.one, 0.3f);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (_isLocked) return;
        if (!col) return;
        
        var place = col.GetComponent<PlaceController>();
        if (!place) return;
        if (place.HasPiece) return;
        _place = place;
        place.HasPiece = true;
        
        if (place == PieceOfPuzzlePlace) {
            _isLocked = true;
            transform.DOMove(place.transform.position, 0.4f).SetEase(Ease.OutBack).OnComplete(() => {
                col.enabled = false;
                OnPuzzleComplete?.Invoke();
                AudioManager.Instance.PlaySound(TypeOfSound.WholePuzzle);
            });
        }
        else {
            _isLocked = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (_isLocked) return;
        if (!col) return;
        
        var place = col.GetComponent<PlaceController>();
        if (!place) return;
        if (!place.HasPiece) return;
        if (place != _place) return;
        place.HasPiece = false;
    }

    
}
