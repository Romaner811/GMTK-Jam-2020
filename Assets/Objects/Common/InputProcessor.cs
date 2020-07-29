using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    public static readonly Dictionary<int, KeyCode> DefaultSettings = new Dictionary<int, KeyCode>()
    {
        { (int)Player.Action.MoveForward, KeyCode.W },
        { (int)Player.Action.MoveBackward, KeyCode.S },
        { (int)Player.Action.MoveLeft, KeyCode.A },
        { (int)Player.Action.MoveRight, KeyCode.D },
        { (int)Player.Action.Jump, KeyCode.Space },
        { (int)Player.Action.Interact, KeyCode.E },
    };

    public Dictionary<int, KeyCode> ActionKeyCodes = new Dictionary<int, KeyCode>(InputProcessor.DefaultSettings);

    private bool CheckAction(int actionID, System.Func<KeyCode, bool> check)
    {
        if (this.ActionKeyCodes.TryGetValue(actionID, out KeyCode key))
        {
            return check(key);
        }
        else
        {
            Debug.LogWarning($"{this.name} unknown action id {actionID}");
            return false;
        }
    }

    public bool ShouldStart(int actionID)
    {
        return this.CheckAction(actionID, Input.GetKeyDown);
    }

    public bool ShouldStop(int actionID)
    {
        return this.CheckAction(actionID, Input.GetKeyUp);
    }
}
