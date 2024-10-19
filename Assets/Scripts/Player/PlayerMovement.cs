using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private PlayerMovement enemyPlayer;
    
    private Vector3 _targetPosition;
    private bool _isMoving;
    private TurnController _turnController;

    private void Start()
    {
        _isMoving = false;

        _turnController = FindObjectOfType<TurnController>();
    }

    void Update()
    {
        if (_isMoving)
        {
            MovePlayer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z), moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) <= 0.5f)
        {
            _isMoving = false;

            _turnController.MovePlayer();
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, tileLayer))
        {
            Vector3 clickedTilePosition = hit.transform.position;
            
            if (Vector3.Distance(transform.position, clickedTilePosition) <= 0.5f)
            {
                Debug.Log("You are in this tile!");
                return;
            }
            
            if (Vector3.Distance(enemyPlayer.transform.position, clickedTilePosition) <= 0.5f)
            {
                Debug.Log("The enemy is in this tile!");
                return;
            }

            if (IsAdjacent(clickedTilePosition))
            {
                _targetPosition = clickedTilePosition;
                _isMoving = true;
            }
        }
    }

    private bool IsAdjacent(Vector3 clickedTilePosition)
    {
        float tileSize = 1f;

        return Vector3.Distance(transform.position, clickedTilePosition) <= tileSize * 1.5f;
    }
}