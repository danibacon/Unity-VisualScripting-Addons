using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Next Item")]
[UnitCategory("More/Collections")]
[TypeIcon(typeof(IEnumerable))]
public class NextItem : Unit
{
    /// <summary>
    /// The Control Input entered when we want a random element
    /// </summary>
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput Enter;

    /// <summary>
    /// Reset the current item index to init.
    /// </summary>
    [DoNotSerialize]
    public ControlInput Reset;
    
    /// <summary>
    /// The Control Output for when the query is complete.
    /// </summary>
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlOutput Exit;

    /// <summary>
    /// The ValueInput for the list we will be querying.
    /// </summary>
    [DoNotSerialize]
    public ValueInput List;

    /// <summary>
    /// The ValueOutput for the randomly selected value.
    /// </summary>
    [DoNotSerialize]
    public ValueOutput Value;

    private int _index = 0;
    
    protected override void Definition()
    {
        Enter = ControlInput(nameof(Enter), OnEnter);
        Reset = ControlInput(nameof(Reset), OnReset);
        Exit = ControlOutput(nameof(Exit));

        List = ValueInput<IList>("list");
        Value = ValueOutput<object>("item");

        Succession(Enter, Exit);
        Assignment(Enter, Value);
        Requirement(List, Enter);
    }

    private ControlOutput OnEnter(Flow flow)
    {
        var list = flow.GetValue<IList>(List);
        if (list == null) return Exit;
        
        if (list.Count == 0)
        {
            Debug.LogWarning("List is empty, null returned.");
            flow.SetValue(Value, null);
            return Exit;
        }

        flow.SetValue(Value, list[_index]);
        _index = ++_index > list.Count - 1 ? _index = 0 : _index;
        
        return Exit;
    }
    
    private ControlOutput OnReset(Flow flow)
    {
        var list = flow.GetValue<IList>(List);
        if (list == null || list.Count == 0)
        {
            flow.SetValue(Value, null);
            return null;
        }
        
        _index = 0;
        flow.SetValue(Value, list[_index]);
        return null;
    }
    
}