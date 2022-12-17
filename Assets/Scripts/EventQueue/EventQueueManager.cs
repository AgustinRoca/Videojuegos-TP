using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    static public EventQueueManager instance;

    public Queue<ICommand> Events => _events;
    public Queue<ICommand> Movements => _movements;

    [SerializeField] bool _isPlayerFrozen = false;
    private Queue<ICommand> _events = new Queue<ICommand>();
    private Queue<ICommand> _movements = new Queue<ICommand>();

    private void Awake()
    {
        if(instance != null) Destroy(this.gameObject);
        instance = this; 
    }
    private void Update()
    {
        while(_events.Count > 0){
            _events.Dequeue().Execute();
        }
        while(_movements.Count > 0 && !_isPlayerFrozen){
            _movements.Dequeue().Execute();
        }
        Movements.Clear();
    }

    public void AddCommand(ICommand cmd) => _events.Enqueue(cmd);
    public void AddMovementCommand(ICommand cmd) => _movements.Enqueue(cmd);
}