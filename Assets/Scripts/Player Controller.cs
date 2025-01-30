using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 3f;

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _groundCheckDistance = 10f;

    private Animator _anim;
    private Rigidbody _rb;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �v���C���[�̓���
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // �v���C���[�̈ړ�
        Vector3 velocity = new Vector3(moveX, 0f, moveZ).normalized * _moveSpeed * Time.deltaTime;
        transform.position += velocity;

        // �v���C���[�̉�]
        if (velocity.magnitude > 0)
        {
            Quaternion look = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, _rotationSpeed);
        }

        bool isGround = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance);

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse); // ������ɗ͂�������
        }

        if (moveX == 0 && moveZ == 0)
        {
            _anim.SetBool("isRunning", false);
            _anim.SetBool("isIdling", true);
        }
        else
        {
            _anim.SetBool("isIdling", false);
            _anim.SetBool("isRunning", true);
        }
    }
}
