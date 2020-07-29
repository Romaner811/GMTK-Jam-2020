using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAffectable : MonoBehaviour, IAffectable
{
    private IList<IEffect> _trigerrables;
    private int _lowestFreeID;

    private ISet<int> _canTrigger;

    public bool CanTriggersAny { get { return this._canTrigger.Count > 0; } }

    protected virtual void Awake()
    {
        this._canTrigger = new HashSet<int>();

        this._trigerrables = new List<IEffect>(3);
        this._lowestFreeID = 0;
    }

    public virtual void Trigger(IEnumerable<int> effects = null)
    {
        if (effects == null)
        {
            // TODO: avoid allocation within runtime.
            var ids = new int[this._canTrigger.Count];
            this._canTrigger.CopyTo(ids, 0);
            effects = ids;
        }

        foreach (int effectID in effects)
        {
            this._trigerrables[effectID].Affect(this);
        }
    }

    private int PullFreeID()
    {
        int id = this._lowestFreeID;

        int nextFreeID = this._lowestFreeID;
        for (; nextFreeID < this._trigerrables.Count; nextFreeID++)
        {
            if (this._trigerrables[nextFreeID] == null)
            {
                break;
            }
        }
        this._lowestFreeID = nextFreeID;

        return id;
    }

    public virtual int CanTrigger(IEffect effect)
    {
        int effectID = this.PullFreeID();

        this._canTrigger.Add(effectID);
        if (effectID < this._trigerrables.Count)
        {
            this._trigerrables[effectID] = effect;
        }
        else
        {
            this._trigerrables.Add(effect);
        }

        return effectID;
    }
    public virtual void CancelCanTrigger(int effectID)
    {
        this._canTrigger.Remove(effectID);
        this._trigerrables[effectID] = null;

        if (effectID < this._lowestFreeID)
        {
            this._lowestFreeID = effectID;
        }
    }
}
