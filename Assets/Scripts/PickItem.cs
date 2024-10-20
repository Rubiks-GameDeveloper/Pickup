using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PickItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerInteractionTip;
    [SerializeField] private TextMeshProUGUI playerDragTip;
    [SerializeField] private GameObject pickablePos;
    [SerializeField] private float dragDistance;
    [Range(0, 10)]
    [SerializeField] private float approachSpeed = 1.5f;
    private Transform _cameraTransform;
    private bool _isDragging;
    private GameObject _draggableItem;
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private RaycastHit? GetRaycastItemByLayer(string layerMask)
    {
        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, dragDistance, LayerMask.GetMask(layerMask));
        if(hit.transform != null) return hit;
        return null;
    }

    private void ItemDragging()
    {
        if (!_isDragging)
        {
            _draggableItem = GetRaycastItemByLayer("Draggable")?.transform.gameObject; 
            ShowDragTip();
        }

        if (Input.GetKey(KeyCode.Mouse0) && _draggableItem != null)
        {
            DragItem();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && _isDragging)
        {
            DropItem();
        }
    }

    private void ShowDragTip()
    {
        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, dragDistance);
        if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Draggable"))
        {
            playerDragTip.text = "Hold Left Mouse Button for item drag";
        }
        else
        {
            playerDragTip.text = "";
        }
    }

    private void ItemInteraction()
    {
        var interactItem = GetRaycastItemByLayer("Interactable")?.transform.gameObject;
        if (interactItem != null)
        {
            playerInteractionTip.text = "Press E";
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactItem.GetComponent<InteractionItem>().InteractItem();
            }
        }
        else
        {
            playerInteractionTip.text = "";
        }
        
    }

    private void DropItem()
    {
        _draggableItem.transform.SetParent(null, true);
        _draggableItem.GetComponent<PickableItem>().Drop();
        _isDragging = false;
        _draggableItem = null;
        playerDragTip.text = "";
    }

    private void DragItem()
    {
        if (_draggableItem.transform.parent != transform) _draggableItem.transform.SetParent(transform, true);
        _draggableItem.GetComponent<PickableItem>().Drag(pickablePos, approachSpeed);
        _isDragging = true;
        playerDragTip.text = "";
    }

    private void Update()
    {
        ItemDragging();
        ItemInteraction();
    }
}
