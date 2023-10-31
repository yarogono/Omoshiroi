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
    [SerializeField][Tooltip("질량.\n 높을수록 충격에 움직이는 거리가 작다.")][Range(0.5f, 2f)] private float _mass = 1.0f;
    [SerializeField][Tooltip("충격시간.\n 높을수록 충격이 오래 지속된다.\n = 충격에 의해 더 멀리 날라간다.")] private float DampingTime = 0.2f;
    [SerializeField][Tooltip("중력으로 인한 추락 최대 속도")] private const float MinVerticalSpeed = -3.0f;
    [SerializeField][Tooltip("조작으로 인한 최대 속도")] private const float MaxSpeed = 0.5f;

    private Vector3 _impactForce;
    private Vector3 _controlForce;
    private Vector3 _dampingVelocity;

    private Vector3 _physicsVelocity;
    private Vector3 _controlVelocity;
    private float _verticalVelocity;

    private float _knockoutTime;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 조작 불가 시간
        if (_knockoutTime > 0)
            _knockoutTime -= Time.deltaTime;

        // 중력 계산 => g 가속도
        // 너무 빨라지지 않도록 clamping 하였음.
        if (!_controller.isGrounded)
        {
            _verticalVelocity = Mathf.Max(_verticalVelocity + Physics.gravity.y * Time.deltaTime, MinVerticalSpeed);
        }

        // 충격을 부드럽게 줄인다.
        _impactForce = Vector3.SmoothDamp(_impactForce, Vector3.zero, ref _dampingVelocity, DampingTime);

        // 캐릭터 이동
        _physicsVelocity = PhysicsVelocity(_physicsVelocity);
        if (_knockoutTime > 0 || !_controller.isGrounded)
            _controlVelocity = Vector3.zero;
        else
            _controlVelocity = ControlVelocity(_controlVelocity);
        _controller.Move((_physicsVelocity + _controlVelocity) * Time.deltaTime);
    }

    public void Reset()
    {
        _impactForce = Vector3.zero;
        _controlForce = Vector3.zero;
        _dampingVelocity = Vector3.zero;
        _controlVelocity = Vector3.zero;
        _physicsVelocity = Vector3.zero;
        _verticalVelocity = 0;
        _knockoutTime = 0;
    }

    /// <summary>
    /// 해당 방향으로 충격/힘을 받는다. 속도 변화에 제한이 없다.
    /// </summary>
    /// <param name="impact">적용 충격/힘</param>
    /// <param name="knockoutDuration">조작 불가능 시간</param>
    public void AddImpact(Vector3 impact, float knockoutDuration = 0.3f)
    {
        _impactForce += impact;
        ApplyKnockOutTime(knockoutDuration);
    }

    /// <summary>
    /// 조작 불가능 시간을 업데이트하나, 최대값만 적용
    /// </summary>
    /// <param name="knockoutTime">조작 불가능 시간</param>
    private void ApplyKnockOutTime(float knockoutTime)
    {
        _knockoutTime = Mathf.Max(_knockoutTime, knockoutTime);
    }

    /// <summary>
    /// 조작하려는 힘으로 최대 속도가 제한되어 있다.
    /// </summary>
    /// <param name="force">힘</param>
    public void ControlMove(Vector3 force)
    {
        _controlForce = force;
    }

    /// <summary>
    /// 주어진 값만큼 위치를 변경시키나, 중간에 Collider가 있으면 막힌다.
    /// </summary>
    /// <param name="deltaPosition">이동하는 벡터값</param>
    public void DeltaMovement(Vector3 deltaPosition)
    {
        _controller.Move(deltaPosition);
    }

    /// <summary>
    /// 주어진 위치로 무조건 위치를 변경시킨다.
    /// </summary>
    /// <param name="position">world position</param>
    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    /// <summary>
    /// 조작 힘으로 인한 속도를 변화시킨다.
    /// </summary>
    /// <param name="currentVelocity">조작으로 인한 현재 속도</param>
    /// <returns>조작으로 변한 속도</returns>
    private Vector3 ControlVelocity(Vector3 currentVelocity)
    {
        currentVelocity += _controlForce * Time.deltaTime;
        float speed = currentVelocity.magnitude;
        // 최대속도 제한
        if (speed > MaxSpeed)
            currentVelocity = currentVelocity.normalized * Mathf.Clamp(speed, -MaxSpeed, MaxSpeed);

        return currentVelocity;
    }

    /// <summary>
    /// 충격/낙하로 인한 속도를 변화시킨다.
    /// </summary>
    /// <param name="currentVelocity"></param>
    /// <returns>충격/낙하로 변한 속도</returns>
    private Vector3 PhysicsVelocity(Vector3 currentVelocity)
    {
        currentVelocity += _impactForce * Time.deltaTime + _verticalVelocity * Vector3.up;
        return currentVelocity;
    }
}
