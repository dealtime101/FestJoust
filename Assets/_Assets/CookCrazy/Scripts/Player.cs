using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour, IKitchenObjectParent {
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!CookCrazyGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (!CookCrazyGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        Vector3 capsuleBase = transform.position + Vector3.up * playerRadius;
        Vector3 capsuleTop = transform.position + Vector3.up * (playerHeight - playerRadius);

        // Effectuer un CapsuleCast pour détecter les obstacles
        bool canMove = !Physics.CapsuleCast(capsuleBase, capsuleTop, playerRadius, moveDir, out RaycastHit hitInfo, moveDistance, counterLayerMask);
        Debug.DrawRay(capsuleBase, moveDir * moveDistance, canMove ? Color.green : Color.red);

        if (canMove) {
            transform.position += moveDir * moveDistance;
            isWalking = true;
        } else {
            // Tenter de glisser le long de l'obstacle
            Vector3 slideDirection = Vector3.ProjectOnPlane(moveDir, hitInfo.normal).normalized;
            canMove = !Physics.CapsuleCast(capsuleBase, capsuleTop, playerRadius, slideDirection, out RaycastHit slideHit, moveDistance, counterLayerMask);
            Debug.DrawRay(capsuleBase, slideDirection * moveDistance, canMove ? Color.blue : Color.yellow);

            if (canMove) {
                transform.position += slideDirection * moveDistance;
            }
            isWalking = canMove;
        }

        if (isWalking) {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
