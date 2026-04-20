namespace Codebound.Entities.Opponents;
public interface IEnemyActionStrategy
{
    int Delay { get; set; }
    void Act(Enemy owner);
}