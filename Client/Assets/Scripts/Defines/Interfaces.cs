// 인터페이스 모음

public interface IInteractable
{
    public void Interact();
}

public interface ILootable
{
    public void Loot();
}

public interface IDroppable
{
    public void Drop();
}

public interface IEquipable
{
    public void Equip();
    public void Dequip();
}

public interface IConsumable
{
    public void Consume();
}

public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}
