using Codebound.Entities;
using Codebound.Entities.Opponents;
namespace Codebound.Items;

public interface IItem
{
    string Name { get; set; }
    string Description { get; set; }
    void Use(Hero user, Enemy target);
    string GetDescription();
}