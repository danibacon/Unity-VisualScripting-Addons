using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


[UnitCategory("Bezalel/Physics")]
[UnitSurtitle("Rigidbody 2D")]
[UnitTitle("Set Velocity Y")]
[UnitShortTitle("Set Velocity Y")]
[TypeIcon(typeof(Rigidbody2D))]
public sealed class SetVelocityY : Unit
{

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput enter { get; private set; }
    
    [DoNotSerialize]
    [PortLabelHidden]
    [NullMeansSelf]
    public ValueInput valueRigidBody2D; 
    
    [DoNotSerialize]
    [PortLabelHidden]
    public ValueInput valueIn;
    
    [DoNotSerialize]
    [PortLabelHidden]
    public ValueOutput valueOut;

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlOutput exit { get; private set; }


    protected override void Definition()
    {
        
        valueRigidBody2D = ValueInput<Rigidbody2D>(nameof(valueRigidBody2D),null).NullMeansSelf();
        valueIn = ValueInput<float>("Velocity", 0);
        valueOut = ValueOutput<Vector2>(nameof(valueOut));
        
        enter = ControlInput(nameof(enter), Enter);
        exit = ControlOutput(nameof(exit));
    }

    private ControlOutput Enter(Flow flow)
    {
        var body = flow.GetValue<Rigidbody2D>(valueRigidBody2D);
        var vy = flow.GetValue<float>(valueIn);
        var vx = body.linearVelocity.x;

        body.linearVelocity = new Vector2(vx, vy);
        
        flow.SetValue(valueOut, body.linearVelocity);
        return exit;
    }

}