using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CharacterSpriteRotator
{
    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _weapon;
    private Vector3 _weaponBase;

    public void Register(CharacterDataContainer container)
    {
        container.InputActions.OnMoveEvent += MoveInput;
        container.InputActions.OnAimEvent += AimInput;
        _weaponBase = _weapon.transform.position;
    }

    public void Register(CloneDataContainer container)
    {
        container.Sync.OnCloneEvent += NetworkInput;
        _weaponBase = _weapon.transform.position;
    }

    public void NetworkInput(Vector3 velocity, float animTime, int state, Vector3 position)
    {
        switch((eStateType)state)
        {
            case eStateType.Aim:
            case eStateType.Attack:
                RotateWaepon(velocity.normalized);
                break;
            case eStateType.Run:
            case eStateType.Walk:
                RotateMain(velocity.normalized);
                break;
            default:
                break;
        }
    }

    public void MoveInput(Vector2 direction)
    {
        RotateMain(direction);
    }

    public void AimInput(Vector2 direction)
    {
        RotateWaepon(direction);
    }

    private void RotateMain(Vector2 direction)
    {
        float direc = direction.x * _main.transform.localScale.x;
        if (direc < 0)
        {
            Vector3 target = _main.transform.localScale;
            target.x = target.x * (-1);
            _main.transform.localScale = target;
        }
    }

    private void RotateWaepon(Vector2 direction)
    {
        Vector2 axis;
        if (direction.x < 0)
        {
            _weapon.transform.localScale = new Vector3() { x = -1, y = 1, z = 1 };
            axis = new Vector2() { x = -1, y = 0 };

            float angle = Vector2.Angle(axis, direction);
            if (direction.y > 0)
                _weapon.transform.localEulerAngles = new Vector3() { x = 90, y = 0, z = -angle };
            else
                _weapon.transform.localEulerAngles = new Vector3() { x = 90, y = 0, z = angle };
        }
        else
        {
            _weapon.transform.localScale = Vector3.one;
            axis = new Vector2() { x = 1, y = 0 };

            float angle = Vector2.Angle(axis, direction);
            if (direction.y < 0)
                _weapon.transform.localEulerAngles = new Vector3() { x = 90, y = 0, z = -angle };
            else
                _weapon.transform.localEulerAngles = new Vector3() { x = 90, y = 0, z = angle };
        }
    }
}
