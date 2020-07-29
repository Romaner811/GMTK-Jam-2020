using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggedAffectable : BasicAffectable
{
    public override void Trigger(IEnumerable<int> effects = null)
    {
        Debug.Log($"{this.name}: triggered!");
        base.Trigger(effects);
    }

    public override int CanTrigger(IEffect effect)
    {
        Debug.Log($"{this.name}: got an effect!");
        return base.CanTrigger(effect);
    }
    public override void CancelCanTrigger(int effectID)
    {
        Debug.Log($"{this.name}: lost an effect...");
        base.CancelCanTrigger(effectID);
    }
}
