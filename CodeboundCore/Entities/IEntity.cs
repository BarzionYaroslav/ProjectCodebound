namespace Codebound.Entities;

public interface IEntity
{
    string Name { get; set; }
    int Def { get; set; }
    int Atk { get; set; }
    int Hp { get; set; }
    int MaxHp { get; set; }
    void UpdateValues();
}