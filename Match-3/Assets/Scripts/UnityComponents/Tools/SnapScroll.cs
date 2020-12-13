using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class SnapScroll : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private float _snappingDuration = .15f;
    [SerializeField] private Transform[] _scrollElements = null;

    private ScrollRect _scrollRect;
    private Vector2 _startPosition;

    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _startPosition = _scrollElements[0].position;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        float minDistance = _scrollElements.Min(e => Mathf.Abs(_startPosition.x - e.position.x));
        Transform nearCenterElement = _scrollElements.Where(e => Mathf.Abs(_startPosition.x - e.position.x) == minDistance).First();
        float deltaX = _startPosition.x - nearCenterElement.position.x;

        for (int i = 0; i < _scrollElements.Length; i++)
        {
            Vector3 target = _scrollElements[i].position + deltaX * Vector3.right;
            _scrollElements[i].DOMove(target, _snappingDuration);
        }
    }
}
