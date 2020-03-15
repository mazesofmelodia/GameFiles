public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300
}

public class StatModifier
{
    public readonly float Value;            //Value of the stat modifier
    public readonly StatModType Type;       //The type of stat modifier
    public readonly int Order;              //Order of the stat modifier
    public readonly object Source;          //Source of the stat modifier

    public StatModifier(float value, StatModType type, int order, object source)
    {
        //Initalise the value and type of the modifier
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    //Contructor which just takes in the value and type
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    //Constructor which takes in value, type and order
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    //Constructor which takes in value, type and source
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
