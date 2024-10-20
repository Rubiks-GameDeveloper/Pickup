using UnityEngine;
[RequireComponent(typeof(Animator))]
public class GarageDoorInteract : InteractionItem
{
    private Animator _animator;
    public void Start()
    {
        _animator = GetComponent<Animator>();
        interact.AddListener(ChangeDoorState);
    }

    private void ChangeDoorState()
    {
        _animator.SetBool("isOpen", !_animator.GetBool("isOpen"));
        _animator.SetBool("isClose", !_animator.GetBool("isOpen"));
    }
}
