using Codebound.Entities.Opponents;
using Codebound.Entities;
using Codebound.System;
using Codebound.Drawing;

namespace Codebound.Items.Weapons;
public class Sword : BaseWeapon
{
    public Sword(string name, string description, int attack)
    {
        Name = name;
        Description = description;
        Atk = attack;
    }

    public override void Use(Hero user, Enemy target)
    {
        SoundManager.PlaySound("sword3");
        new SpriteBuilder().SetSprite("sword_fx")
            .SetImageSpeed(1f)
            .SetPosition(
                target.Body[Enemy.BodyName].X + (target.Body[Enemy.BodyName].DrawWidth / 2) - 8,
                target.Body[Enemy.BodyName].Y + (target.Body[Enemy.BodyName].DrawHeight / 2) - 8
                )
            .SetDepth(0)
            .SetVanish(true)
            .Build();
    }
}