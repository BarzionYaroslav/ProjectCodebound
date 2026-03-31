using Codebound.Entities;
using Codebound.Entities.Opponents;
using Codebound.System;
using Codebound.Drawing;

namespace Codebound.Items.Weapons;
public abstract class BaseWeapon: IItem
{
    public string Name { 
        get { return name; }
        set
        {
            if (!value.IsWhiteSpace() && value != null)
            {
                if (value.Length <= MaxNameSize)
                    name = value;
                else
                    throw new ArithmeticException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        } 
        }
    public string Description { 
        get { return description; }
        set
        {
            if (!value.IsWhiteSpace() && value != null)
            {
                if (value.Length <= MaxDescriptionSize)
                    description = value;
                else
                    throw new ArithmeticException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        } 
        }
    public int Atk
    {
        get { return atk; }
        set
        {
            if (value >= 0)
            {
                atk = value;
            }
        }
    }
    public virtual string GetDescription()
    {
        return $"{Name} - {GetDamage()} ATK\n{Description}";
    }
    public virtual string GetName()
    {
        return Name;
    }
    public virtual int GetDamage()
    {
        return Atk;
    }
    public virtual void Use(Hero user, Enemy target)
    {
        SoundManager.PlaySound("punch");
        new SpriteBuilder().SetSprite("punch_fx")
            .SetImageSpeed(1f)
            .SetPosition(
                target.Body[Enemy.BodyName].X + (target.Body[Enemy.BodyName].DrawWidth/2) - 8,
                target.Body[Enemy.BodyName].Y + (target.Body[Enemy.BodyName].DrawHeight/2) - 8
                )
            .SetDepth(0)
            .SetVanish(true)
            .Build();
    }
    private string name = DefaultName;
    private string description = DefaultDescription;
    private int atk = DefaultAtk;
    const string DefaultName = "Weapon";
    const string DefaultDescription = "UNOWEN";
    const int DefaultAtk = 2;
    const int MaxNameSize = 16;
    const int MaxDescriptionSize = 128;
}