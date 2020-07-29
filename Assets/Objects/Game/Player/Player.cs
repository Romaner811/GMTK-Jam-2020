using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Action
    {
        None,
        MoveForward, MoveBackward, MoveRight, MoveLeft,
        Jump, Interact
    }

    [SerializeReference] protected Mover _mover;
    [SerializeReference] protected BasicAffectable _affectable;
    [SerializeReference] protected InputProcessor _input;

    private Vector2 _motion;

    private void OnEnable()
    {
        if (this._mover == null)
        {
            Debug.LogError($"{this.name}: missing Mover!");
        }

        if (this._affectable == null)
        {
            Debug.LogError($"{this.name}: missing BasicAffectable!");
        }

        if (this._input == null)
        {
            Debug.LogError($"{this.name}: missing InputProcessor!");
        }
    }

    private void UpdateMotionFromInput()
    {
        // Forward
        if (this._input.ShouldStart((int)Action.MoveForward))
        {
            this._motion.y++;
        }
        else if (this._input.ShouldStop((int)Action.MoveForward))
        {
            this._motion.y--;
        }

        // Backward
        if (this._input.ShouldStart((int)Action.MoveBackward))
        {
            this._motion.y--;
        }
        else if (this._input.ShouldStop((int)Action.MoveBackward))
        {
            this._motion.y++;
        }

        // Left
        if (this._input.ShouldStart((int)Action.MoveLeft))
        {
            this._motion.x--;
        }
        else if (this._input.ShouldStop((int)Action.MoveLeft))
        {
            this._motion.x++;
        }

        // Right
        if (this._input.ShouldStart((int)Action.MoveRight))
        {
            this._motion.x++;
        }
        else if (this._input.ShouldStop((int)Action.MoveRight))
        {
            this._motion.x--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMotionFromInput();
        this._mover.SetMotion(this._motion);

        // Jump
        if (this._input.ShouldStart((int)Action.Jump))
        {
            this._mover.SetJumping(true);
        }
        else if (this._input.ShouldStop((int)Action.Jump))
        {
            this._mover.SetJumping(false);
        }

        // Interact
        if (this._input.ShouldStart((int)Action.Interact))
        {
            this._affectable.Trigger();
        }
    }
}
