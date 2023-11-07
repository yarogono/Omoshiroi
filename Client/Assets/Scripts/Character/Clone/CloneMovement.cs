using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    // 외부에서 객체의 정보를 가져가기 위함
    public Vector3 Velocity
    {
        get => _lastVelocity;
    }
    public Vector3 Position
    {
        get => transform.position;
    }

    // 호출 시점 관련
    public float AfterLastOrderTime
    {
        get => Time.realtimeSinceStartup - _lastOrderTime;
    }
    public float LastOrderTime
    {
        get => _lastOrderTime;
    }
    private float _lastOrderTime;

    // 속도 관련
    private bool _velFlag;
    private Vector3 _lastVelocity;
    private Vector3 _orderedVelocity;

    // 위치 관련
    private bool _posFlag;
    private Vector3 _lastPosition;

    // Sync 모듈
    private CloneSync _sync;

    // 조금씩 느려지도록
    [Header("오랫동안 업데이트가 없으면 조금씩 느려져 멈추도록 설정함.")]
    [SerializeField]
    [Tooltip("제한 시간(초)")]
    private float _limitTime;

    [SerializeField]
    [Tooltip("멈추는 시간")]
    private float _dampTime;
    private Vector3 _dampingVelocity;

    void Start()
    {
        gameObject.TryGetComponent<CloneSync>(out _sync);
        // TODO
        // _sync의 이벤트에 MoveClone 혹은 Move를 등록하도록 한다.
        _sync.OnCloneEvent += NetworkInput;
    }

    void Update()
    {
        UpdateMovement();
        if (AfterLastOrderTime > _limitTime)
            SmoothlyStop();
    }

    public void NetworkInput(float velocity, float animTime, int state, Vector3 position)
    {
        
    }

    public void MoveClone(Vector3 position, Vector3 velocity)
    {
        _lastPosition = position;
        _posFlag = true;
        _orderedVelocity = velocity;
        _velFlag = true;
        _lastOrderTime = Time.realtimeSinceStartup;
    }

    public void MoveClone(Vector3 position)
    {
        _lastPosition = position;
        _posFlag = true;
        _lastOrderTime = Time.realtimeSinceStartup;
    }

    private void UpdateMovement()
    {
        Vector3 deltaPosition = transform.position;

        if (_posFlag)
        {
            deltaPosition = _lastPosition;
            _posFlag = false;
        }

        if (_velFlag)
        {
            deltaPosition += _orderedVelocity * Time.deltaTime;
            _lastVelocity = _orderedVelocity;
            _velFlag = false;
        }
        else
            deltaPosition += _lastVelocity * Time.deltaTime;

        transform.position = deltaPosition;
    }

    private void SmoothlyStop()
    {
        _lastVelocity = Vector3.SmoothDamp(
            _lastVelocity,
            Vector3.zero,
            ref _dampingVelocity,
            _dampTime
        );
    }
}
