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

public interface IEquippable
{
    public void Equip(CharacterDataContainer cdc);
    public void Dequip(CharacterDataContainer cdc);
}

public interface IConsumable
{
    public void Consume();
}

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
    public void PhysicsUpdate();
}
