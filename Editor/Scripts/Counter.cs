namespace Unity.VisualScripting.Test
{
    [UnitTitle("Counter")]
    [UnitCategory("Bezalel/Math")]
    [TypeIcon(typeof(Add<object>))]
    public class CounterNode : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        public ControlInput reset;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ValueInput Step;
        
        [DoNotSerialize]
        public ValueOutput Count;

        private float _counter;

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), OnEnter);
            reset = ControlInput(nameof(reset), OnReset);
            exit = ControlOutput(nameof(exit));

            Step = ValueInput<float>(nameof(Step), 1);
            Count = ValueOutput<float>(nameof(Count));

            Succession(enter, exit);
            Succession(reset, exit);
        }

        private ControlOutput OnEnter(Flow flow)
        {
            var step = flow.GetValue<float>(Step);
            _counter += step;
            flow.SetValue(Count, _counter);
            return exit;
        }

        private ControlOutput OnReset(Flow flow)
        {
            _counter = 0;
            flow.SetValue(Count, _counter);
            return null;
        }
    }
}
