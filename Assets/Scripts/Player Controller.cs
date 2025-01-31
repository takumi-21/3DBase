using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Rigidbody _rb = null;

    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _jumpPower = 10f;

    private bool _isGround = true;

    private void Start()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // プレイヤーの入力
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // プレイヤーの移動
        Vector3 velocity = new Vector3(moveX, 0f, moveZ).normalized * _moveSpeed * Time.deltaTime;
        transform.position += velocity;

        // プレイヤーの回転
        if (velocity.magnitude > 0)
        {
            Quaternion look = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, _rotationSpeed);
        }

        float radius = 0.2f;
        float maxDistance = 0.2f;
        Vector3 origin = transform.position + Vector3.up * 0.3f;
        _isGround = Physics.SphereCast(origin, radius, Vector3.down, out _, maxDistance);

        if (!_isGround)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isRun", false);
            _animator.SetBool("isJump", true);
        }
        else
        {
            if (moveX == 0 && moveZ == 0)
            {
                _animator.SetBool("isRun", false);
                _animator.SetBool("isJump", false);
                _animator.SetBool("isIdle", true);
            }
            else
            {
                _animator.SetBool("isIdle", false);
                _animator.SetBool("isJump", false);
                _animator.SetBool("isRun", true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isGround && Input.GetKey(KeyCode.Space))
        {
            _rb.velocity = new Vector3(0f, _jumpPower, 0f);

        }
    }
}
