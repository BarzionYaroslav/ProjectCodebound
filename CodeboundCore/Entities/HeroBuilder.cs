using Codebound.Drawing;
namespace Codebound.Entities;

public class HeroBuilder
{
    private Hero _hero = new Hero();

    public HeroBuilder SetName(string name)
    {
        _hero.Name = name;
        return this;
    }
    public HeroBuilder SetAtk(int atk)
    {
        _hero.Atk = atk;
        return this;
    }
    public HeroBuilder SetDef(int def)
    {
        _hero.Def = def;
        return this;
    }
    public HeroBuilder SetHp(int hp)
    {
        _hero.MaxHp = hp;
        _hero.Hp = hp;
        return this;
    }
    public HeroBuilder SetMana(int mana)
    {
        _hero.MaxMana = mana;
        _hero.Mana = mana;
        return this;
    }

    public HeroBuilder SetFace(string faceAsset, float imageSpeed = 0f)
    {
        _hero.Face.Dispose();
        _hero.Face = new Icon(faceAsset, imageSpeed);
        return this;
    }

    public Hero Build() => _hero;
}