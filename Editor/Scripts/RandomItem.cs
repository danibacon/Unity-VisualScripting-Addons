using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[UnitTitle("Random Item")]
[UnitCategory("Bezalel/Collections")]
[TypeIcon(typeof(IEnumerable))]
public class RandomItem : Unit
{
    /// <summary>
    /// The Control Input entered when we want a random element
    /// </summary>
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput Enter;

    /// <summary>
    /// Reset the random item selection in case of no repeats
    /// </summary>
    [DoNotSerialize]
    public ControlInput Reset;
    
    /// <summary>
    /// The Control Output for when the query is complete.
    /// </summary>
    [DoNotSerialize]
    public ControlOutput Exit;

    /// <summary>
    /// The ValueInput for the collection/list we will be querying.
    /// </summary>
    [DoNotSerialize]
    public ValueInput List;

    /// <summary>
    /// The ValueOutput for the randomly selected value.
    /// </summary>
    [DoNotSerialize]
    public ValueOutput Value;

    [Inspectable]
    public bool NoRepeats;
    
    private IList _shuffledList = new List<object>();

    protected override void Definition()
    {
        Enter = ControlInput(nameof(Enter), OnEnter);
        if (NoRepeats)
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

        if (NoRepeats)
        {
            // If queue is empty, repopulate it
            if (_shuffledList.Count == 0)
            {
                _shuffledList = ShallowClone(list);
                Shuffle(_shuffledList);
            }
        
            // Dequeue and return the first item
            var first = _shuffledList[0];
            _shuffledList.RemoveAt(0);
            flow.SetValue(Value, first);
        }
        else
        {
            //Lists have fast random access, so... use it.
            //Prevents overhead associated with .ElementAt
            flow.SetValue(Value, list[Random.Range(0, list.Count)]);
        }

        return Exit;
    }
    
    private ControlOutput OnReset(Flow flow)
    {
        _shuffledList.Clear();
        flow.SetValue(Value, null);
        return null;
    }
    
    private static IList ShallowClone(IList sourceList)
    {
        if (sourceList == null)
            return null;

        // Get the type of the elements in the source list (if any)
        Type elementType = null;
        if (sourceList.Count > 0)
        {
            elementType = sourceList[0].GetType();
        }
        else
        {
            // If the list is empty, try to infer the element type from the generic arguments
            var listType = sourceList.GetType();
            if (listType.IsGenericType)
            {
                elementType = listType.GetGenericArguments().FirstOrDefault();
            }
        }

        // Create a new list of the same type
        IList clonedList = null;
        if (elementType != null)
        {
            var genericListType = typeof(List<>).MakeGenericType(elementType);
            clonedList = (IList)Activator.CreateInstance(genericListType);
        }
        else
        {
            // If we couldn't determine the element type, create a non-generic ArrayList
            clonedList = new ArrayList();
        }

        // Copy the elements
        foreach (var item in sourceList)
            clonedList.Add(item);

        return clonedList;
    }

    private static void Shuffle(IList list)
    {
        if (list is not { Count: > 1 })
            return;

        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}