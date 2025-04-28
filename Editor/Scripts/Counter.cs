using Unity.VisualScripting;

[UnitTitle("Counter")]
[UnitCategory("More/Math")]
[TypeIcon(typeof(Add<object>))]
public class CounterNode : Unit
{
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput Enter;

    [DoNotSerialize]
    public ControlInput Reset;

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlOutput Exit;

    [DoNotSerialize]
    public ValueInput Step;
    
    [DoNotSerialize]
    public ValueOutput Count;
    
    [Inspectable]
    public bool CustomStep;

    private float _counter;
    private float _step = 1;

    protected override void Definition()
    {
        Enter = ControlInput(nameof(Enter), OnEnter);
        Reset = ControlInput(nameof(Reset), OnReset);
        Exit = ControlOutput(nameof(Exit));

        if (CustomStep)
            Step = ValueInput<float>(nameof(Step), _step);
        
        Count = ValueOutput<float>(nameof(Count));

        Succession(Enter, Exit);
        // Succession(Reset, Exit);
    }

    private ControlOutput OnEnter(Flow flow)
    {
        if(CustomStep) _step = flow.GetValue<float>(Step);
        _counter += _step;
        flow.SetValue(Count, _counter);
        return Exit;
    }

    private ControlOutput OnReset(Flow flow)
    {
        _counter = 0;
        flow.SetValue(Count, _counter);
        return null;
    }
}