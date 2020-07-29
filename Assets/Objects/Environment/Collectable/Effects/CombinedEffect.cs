using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedEffect : BaseEffect
{
    [SerializeReference] protected BaseEffect[] _manuallySetEffects;

    private IEffect[] _effects;

    private void Awake()
    {
        IEffect[] effectsOnGameObject = this.GetComponents<IEffect>();

        HashSet<IEffect> distinctEffects = new HashSet<IEffect>();
        distinctEffects.UnionWith(effectsOnGameObject);
        distinctEffects.UnionWith(this._manuallySetEffects);

        // Remove all selves...
        while (distinctEffects.Remove(this)) ;

        this._effects = new IEffect[distinctEffects.Count];
        distinctEffects.CopyTo(this._effects);

        if (this._effects.Length < (this._manuallySetEffects.Length + effectsOnGameObject.Length - 1))
        {
            Debug.LogWarning($"{this.name}: Has more than one reference to some effects...\r\nRemember to only add manually the effects that are not part of this game object.");
        }
    }

    public override void Affect(BasicAffectable other)
    {
        foreach (IEffect effect in this._effects)
        {
            effect.Affect(other);
        }
    }
}
