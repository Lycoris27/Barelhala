using System;
using Unity.Properties;

namespace Unity.Behavior
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(
        name: "Ability Activator",
        description: "Runs one ability per activation, then waits for the next trigger to continue.",
        icon: "Icons/random",
        category: "Flow")]
    internal partial class AbilityList : Composite
    {
        private int m_Index = 0;

        protected override Status OnStart()
        {
            if (Children.Count == 0)
                return Status.Failure;

            // Clamp index in case of manual reset or removal of children
            if (m_Index >= Children.Count)
                m_Index = 0;

            // Start only the current ability in sequence
            StartNode(Children[m_Index]);
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (m_Index >= Children.Count)
                return Status.Success;

            var childStatus = Children[m_Index].CurrentStatus;

            if (childStatus == Status.Success || childStatus == Status.Failure)
            {
                // When the current ability finishes, move to the next
                m_Index++;

                // Stop execution — this node finishes here.
                // Next time it’s triggered, it will start the next ability.
                return Status.Success;
            }

            return Status.Running;
        }

        protected override void OnEnd()
        {
            // Optionally wrap around when reaching the end
            if (m_Index >= Children.Count)
                m_Index = 0;
        }
    }
}