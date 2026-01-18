using UnityEngine;

[CreateAssetMenu]
public class TotemDatabase : ScriptableObject
{
    public Totem[] totem;

    public int TotemCount
    {
        get
        {
            return totem.Length;
        }
    }

    public Totem GetTotem(int index)
    {
        return totem[index];
    }
}
