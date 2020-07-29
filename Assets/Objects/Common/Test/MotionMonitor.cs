using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMonitor : MonoBehaviour
{
    [SerializeReference] public Transform Subject;

    private Mover _mover;
    private Rigidbody _rb;

    private void OnEnable()
    {
        if (this.Subject == null)
        {
            if (this.TryGetComponent(out Transform subject))
            {
                Debug.LogWarning($"{this.name}: monitor subject missing!");
                this.Subject = subject;
            }
            else
            {
                Debug.LogError($"{this.name}: can't find monitor subject!");
            }
        }

        this._mover = this.Subject.GetComponent<Mover>();
        this._rb = this.Subject.GetComponent<Rigidbody>();
    }

    private void OnGUI()
    {
        string status = "";

        if (this._mover.IsMoving)
        {
            status += "M";
        }
        else
        {
            status += "-";
        }

        if (this._mover.IsGrounded)
        {
            status += "G";
        }
        else
        {
            status += "-";
        }

        if (this._mover.IsJumping)
        {
            status += "J";
        }
        else
        {
            status += "-";
        }

        string msg = $"Motion:\r\nm = {this._mover.Motion}x{this._mover.Speed}\r\nv = {this._rb.velocity}\r\nStatus:{status}\r\n";
        int lines = 4;

        int lineH = 15;
        int p = 15;

        int w = 200;
        int h = lines * lineH + p;

        int x = Screen.width - w - p;
        int y = p;

        GUI.Box(
            new Rect(x, y, w, h),
            msg
            );
    }
}
