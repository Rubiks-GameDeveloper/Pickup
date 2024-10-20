using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractionItem : MonoBehaviour
{
    public UnityEvent interact;

    public void InteractItem()
    {
        interact.Invoke();
    }
}
