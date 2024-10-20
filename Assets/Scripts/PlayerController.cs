using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform parent;
    
    [Range(0.2f, 5)]
    [SerializeField] private float speed = 2;

    [Range(1, 50)]
    [SerializeField] private float sensitiveX = 5;
    [Range(1, 50)]
    [SerializeField] private float sensitiveY = 5;

    private Vector2 _moveInput;
    private Camera _playerCam;
    private float _camXRotation;
    private Vector2 _cameraDelta;
    private void Awake()
    {
        _playerCam = FindObjectOfType<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 150;
    }
    private Vector3 CalculateMoveDir()
    {
        var direction = parent.forward * _moveInput.y + parent.right * _moveInput.x;

        return direction * speed;
    }
    private void Update()
    {
        RotatePlayer(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");
        if (!characterController.isGrounded)
        {
            Move(transform.up * -1);
            return;
        }
        Move(CalculateMoveDir());
    }
    public void Move(Vector3 direction)
    {
        characterController.Move(direction * Time.deltaTime);
    }
    public void RotatePlayer(Vector2 delta)
    {
        var mouseX = delta.x * sensitiveX * Time.deltaTime;
        var mouseY = delta.y * sensitiveY * Time.deltaTime;

        _camXRotation -= mouseY;

        _camXRotation = Mathf.Clamp(_camXRotation, -90, 90);

        _playerCam.transform.localRotation = Quaternion.Euler(_camXRotation,0,0);
        parent.Rotate(Vector3.up * (mouseX));
    }
}
