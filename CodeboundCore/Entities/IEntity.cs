using Codebound.Drawing;

namespace Codebound.Entities;

public interface IEntity
{
    string Name { get; set; }
    int BaseDef { get; set; }
    int BaseAtk { get; set; }
    int Def => GetDefense();
    int Atk => GetDamage();
    int Hp { get; set; }
    int MaxHp { get; set; }
    Icon Face { get; set; }
    void UpdateValues();
    int GetDamage();
    int GetDefense();
    int Hurt(int dmg, bool defIgnore = false);
    int Heal(int value);
}