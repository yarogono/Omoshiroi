using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class t_PlayerController : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 nextVec = speed * Time.deltaTime * inputVec.normalized;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void OnMove(InputValue value)
    {
        inputVec = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
}
