using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour, IEffect
{
    public BaseEffect Effect;
    public UnityEvent<IAffectable> OnAffect;

    private IDictionary<IAffectable, int> _affectables;

    protected void Awake()
    {
        this._affectables = new Dictionary<IAffectable, int>();

        bool hasTriggerCollider = false;
        foreach (Collider collider in this.GetComponents<Collider>())
        {
            if (collider.isTrigger)
            {
                hasTriggerCollider = true;
                break;
            }
        }

        if (hasTriggerCollider == false)
        {
            Debug.LogError($"{this.name}: trigger collider missing!");
        }
    }

    private void OnDisable()
    {
        // TODO: avoid allocation in midst of runtime.
        var rows = new KeyValuePair<IAffectable, int>[this._affectables.Count];
        this._affectables.CopyTo(rows, 0);
        
        foreach (var row in rows)
        {
            this.Leave(row.Key, row.Value);
        }
    }

    public virtual void Affect(BasicAffectable other)
    {
        if (this.Effect != null)
        {
            this.Effect.Affect(other);
        }
        else
        {
            Debug.LogWarning($"{this.name}: has no effects?");
        }

        if (this.OnAffect != null)
        {
            this.OnAffect.Invoke(other);
        }
    }

    private void Leave(IAffectable interactor, int effectID)
    {
        interactor.CancelCanTrigger(effectID);
        this._affectables.Remove(interactor);
    }

    private void Leave(IAffectable interactor)
    {
        if (this._affectables.TryGetValue(interactor, out int effectID))
        {
            this.Leave(interactor, effectID);
        }
        else
        {
            // never supposed to happen.
            Debug.LogError($"{this.name}: was left by an unknown interactor {interactor.name}.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        // TODO: replace try get component
        if (other.attachedRigidbody.TryGetComponent(out IAffectable interactor))
        {
            this._affectables.Add(interactor, interactor.CanTrigger(this));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        // TODO: replace try get component
        if (other.attachedRigidbody.TryGetComponent(out IAffectable interactor))
        {
            this.Leave(interactor);
        }
    }
}
