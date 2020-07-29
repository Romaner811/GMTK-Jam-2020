using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapControlsEffect : BaseEffect
{
    [SerializeField] public Player.Action One;
    [SerializeField] public Player.Action Two;

    private int RandomizeAction(int except, int amount)
    {
        if (except != 0)
        {
            amount--;
        }

        int value = Random.Range(1, amount - 1);

        if ((except != 0) && (value >= except))
        {
            value++;
        }

        return value;
    }

    private void OnEnable()
    {
        int actionsTotal = System.Enum.GetValues(typeof(Player.Action)).Length;

        if ((this.One == Player.Action.None) && (this.Two == Player.Action.None))
        {
            this.One = (Player.Action)this.RandomizeAction(0, actionsTotal);
            this.Two = (Player.Action)this.RandomizeAction((int)this.One, actionsTotal);

            Debug.LogWarning($"{this.name}: randomized action: One: {this.One}; Two: {this.Two};");
        }
    }

    public void SwapControls(IDictionary<int, KeyCode> keySettings)
    {
        KeyCode tmp = keySettings[(int)this.One];
        keySettings[(int)this.One] = keySettings[(int)this.Two];
        keySettings[(int)this.Two] = tmp;
    }

    public override void Affect(BasicAffectable other)
    {
        this.SwapControls(other.GetComponent<InputProcessor>().ActionKeyCodes);
    }
}
