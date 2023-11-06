using System.Collections.Generic;

public abstract class StateMachine
{
    public eStateType previousStateType { get; private set; }
    public eStateType currentStateType { get; private set; }
    protected IState currentState;
    public Dictionary<eStateType, BaseState> States { get; private set; } = new Dictionary<eStateType, BaseState>();

    public void ChangeState(eStateType newState)
    {
        if (States.ContainsKey(newState))
        {
            currentState?.Exit();

            previousStateType = currentStateType;
            currentStateType = newState;

            currentState = States[newState];

            currentState?.Enter();
        }
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }

    public void AddState(eStateType statetype, BaseState state)
    {
        States.Add(statetype, state);
    }
}