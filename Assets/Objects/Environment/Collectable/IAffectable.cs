using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffectable
{
    string name { get; }  // for MonoBehavior compatibility.

    void Trigger(IEnumerable<int> effectsIDs = null);

    bool CanTriggersAny { get; }
    int CanTrigger(IEffect effect);
    void CancelCanTrigger(int effectID);
}
