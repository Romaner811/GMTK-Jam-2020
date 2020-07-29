using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogEffect : BaseEffect
{
    public override void Affect(BasicAffectable other)
    {
        Debug.Log($"{this.name}: InteractedBy {other.name}");
    }
}
