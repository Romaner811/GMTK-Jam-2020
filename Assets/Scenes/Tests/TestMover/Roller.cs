using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    [SerializeReference] protected Transform _roller;

    private Rigidbody _rollerRB;
    private Vector3 _rollerPos;
    private Vector3 _rollerRot;

    private void Awake()
    {
        if (this._roller != null)
        {
            this.Initialize(_roller);
        }
    }

    private void OnEnable()
    {
        if (this._roller == null)
        {
            Debug.LogError($"{this.name}: missing roller!");
        }
    }

    public void Initialize(Transform roller)
    {
        this._roller = roller;
        this._rollerRB = roller.GetComponent<Rigidbody>();
        this._rollerPos = roller.localPosition;
        this._rollerRot = roller.localEulerAngles;
    }

    private void ResetRB()
    {
        this._rollerRB.velocity = Vector3.zero;
        this._rollerRB.angularVelocity = Vector3.zero;
    }

    private void ResetTransform()
    {
        this._roller.localPosition = this._rollerPos;
        this._roller.localEulerAngles = this._rollerRot;
    }

    public void ResetRoller()
    {
        this.ResetRB();
        this.ResetTransform();
    }
}
