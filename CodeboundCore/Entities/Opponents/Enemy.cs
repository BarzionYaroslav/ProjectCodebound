using Codebound.Drawing;
using Codebound.System;
using Codebound.System.Randomness;
namespace Codebound.Entities.Opponents;

public class Enemy : BaseEntity, IDisposable
{
    public List<IEnemyActionStrategy> ActionList => actionList.ToList();
    public ComplexSpriterHollow Body
    {
        get
        {
            return new ComplexSpriterHollow(body);
        }
    }

    public Enemy()
    {
        Expectations.Add(BodyName);
        this.body = new ComplexSpriter(Expectations);
        actionList = defaultActionList;
        currentAction = defaultAction;
        body.Init();
        face.Init();
        RandomizeAction();
        GameManager.UpdateStarted += UpdateValues;
    }

    public override void Dispose()
    {
        base.Dispose();
        body.Dispose();
        GameManager.UpdateStarted -= UpdateValues;
    }

    public virtual void AfterPrep() { }

    public virtual int DoAction()
    {
        if (hp == 0)
            return 0;
        if (currentAction == null)
            GameManager.Instance.MainPanel.RText = fallbackText;
        else
        {
            currentAction.Act(this);
            return currentAction.Delay;
        }
        return 0;
    }

    public virtual void RandomizeAction()
    {
        currentAction = actionList.GetRandom();
    }

    public virtual void SetAction(IEnemyActionStrategy action)
    {
        currentAction = action;
    }

    public virtual void ChangeActionList(IRandomList<IEnemyActionStrategy> list)
    {
        if (list!=null)
        {
            actionList = list;
        }
    }

    public override int Hurt(int dmg, bool defIgnore = false)
    {
        int damage = dmg;
        if (!defIgnore)
            damage -= Def;
        if (damage > 0)
        {
            Hp -= damage;
            return damage;
        }
        else
        {
            Hp -= chipDamage;
            return chipDamage;
        }
    }
    public void ReplaceBody(ComplexSpriter bodyNew)
    {
        if (bodyNew != null)
        {
            body.Dispose();
            body = bodyNew;
            body.Init();
        }
        else
            throw new NullReferenceException();
    }

    protected ComplexSpriter body;
    private IRandomList<IEnemyActionStrategy> actionList;
    private IEnemyActionStrategy currentAction;
    public HashSet<string> Expectations { get; private set; } = new HashSet<string>();
    private readonly IRandomList<IEnemyActionStrategy> defaultActionList =
        new RandomList<IEnemyActionStrategy>([
            new EnemyPunchStrategy(),
            new EnemySkipStrategy()
            ]);
    private readonly IEnemyActionStrategy defaultAction = new EnemySkipStrategy();
    private readonly string fallbackText = "Enemy doesn't know what it's doing.";
    private readonly int chipDamage = 1;
    static public readonly string BodyName = "body";
}