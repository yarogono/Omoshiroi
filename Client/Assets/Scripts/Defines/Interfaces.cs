// 인터페이스 모음

public interface IInteractable
{

}

public interface ILootable
{

}

public interface IDroppable
{

}

public interface IEquipable
{

}

public interface IConsumable
{

}

public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}
