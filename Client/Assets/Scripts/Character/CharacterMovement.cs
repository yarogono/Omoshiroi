using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;

    [Header("움직임에 관한 설정")]
    [SerializeField][Tooltip("질량.\n 높을수록 impulse에 움직이는 거리가 작다.")] private float mass = 1.0f;
    [SerializeField][Tooltip("충격시간.\n 높을수록 impulse가 느리게 감소한다.\n = impulse에 의해 더 멀리 날라간다.")] private float DampingTime = 0.3f;
    [SerializeField][Tooltip("떨어지는 최대 속도")] private const float MinVerticalSpeed = -3.0f;
    [SerializeField][Tooltip("상승하는 최대 속도")] private const float MaxVerticalSpeed = 3.0f;

    private Vector3 _currentImpulse;
    private float verticalVelocity;

    private float _knockoutTime;

    public Vector3 Velocity { get => CalculateVelocity(); }
    public Vector3 DeltaVByImpulse { get; private set; }
    public Vector3 ControlVelocity { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 속도 변화를 부드럽게 낮춘다.
        if (DeltaVByImpulse.magnitude > 0.1f)
            DeltaVByImpulse = Vector3.SmoothDamp(DeltaVByImpulse, Vector3.zero, ref _currentImpulse, DampingTime);
        else if (DeltaVByImpulse.magnitude > 0f)
        {
            DeltaVByImpulse = Vector3.zero;
            _currentImpulse = Vector3.zero;
        }

        // 중력 계산 => g 가속도
        // 너무 빨라지지 않도록 clamping 하였음.
        if (!_controller.isGrounded)
        {
            verticalVelocity = Mathf.Clamp(verticalVelocity + Physics.gravity.y * Time.deltaTime, MinVerticalSpeed, MaxVerticalSpeed);
        }

        // 조작 불가 시간
        if (_knockoutTime > 0)
            _knockoutTime -= Time.deltaTime;

        // 캐릭터 위치 변화량 이동
        _controller.Move(Velocity * Time.deltaTime);
    }

    public void Reset()
    {
        DeltaVByImpulse = Vector3.zero;
        verticalVelocity = 0f;
        ControlVelocity = Vector3.zero;
    }

    /// <summary>
    /// 충격력 impulse = mass * delta velocity.<br/>
    /// 속도변화량 delta velocity = impulse / mass
    /// </summary>
    /// <param name="impulse">적용 충격량 impulse</param>
    /// <param name="knockoutDuration">조작 불가능 시간</param>
    public void AddImpulse(Vector3 impulse, float knockoutDuration = 0.3f)
    {
        DeltaVByImpulse += impulse / mass;
        ApplyKnockOut(knockoutDuration);
    }

    /// <summary>
    /// 조작 불가능 시간을 업데이트하나, 최대값만 적용
    /// </summary>
    /// <param name="knockoutTime">조작 불가능 시간</param>
    private void ApplyKnockOut(float knockoutTime)
    {
        _knockoutTime = Mathf.Max(_knockoutTime, knockoutTime);
    }

    /// <summary>
    /// 조작하려는 속도
    /// </summary>
    /// <param name="velocity">속도</param>
    public void ControlMove(Vector3 velocity)
    {
        ControlVelocity = velocity;
    }

    /// <summary>
    /// 조작 여부에 따라서 속도를 계산
    /// </summary>
    /// <returns>계산된 속도</returns>
    private Vector3 CalculateVelocity()
    {
        Vector3 ret;
        if (_knockoutTime > 0 || !_controller.isGrounded)
        {
            ret = Vector3.up * verticalVelocity + DeltaVByImpulse;
        }
        else
        {
            ret = ControlVelocity + Vector3.up * verticalVelocity + DeltaVByImpulse;
        }
        return ret;
    }
}
