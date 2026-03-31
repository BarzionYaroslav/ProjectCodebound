using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class EnemyBuilder<T> : IEnemyBuilder<T> where T: Enemy, new()
{
    public EnemyBuilder<T> SetName(string name)
    {
        _enemy.Name = name;
        return this;
    }

    public EnemyBuilder<T> SetDef(int def)
    {
        _enemy.BaseDef = def;
        return this;
    }

    public EnemyBuilder<T> SetAtk(int atk)
    {
        _enemy.BaseAtk = atk;
        return this;
    }

    public EnemyBuilder<T> SetHp(int hp)
    {
        _enemy.MaxHp = hp;
        _enemy.Hp = hp;
        return this;
    }

    public EnemyBuilder<T> SetFace(Icon icon)
    {
        _enemy.Face = icon;
        return this;
    }

    public EnemyBuilder<T> SetBody(Dictionary<string, Sprite> dict)
    {
        ComplexSpriter newBody = new ComplexSpriter(_enemy.Expectations);
        newBody.Add(dict);
        _enemy.ReplaceBody(newBody);
        return this;
    }

    public T Build()
    {
        _enemy.AfterPrep();
        return _enemy;
    }

    private T _enemy = new();
}