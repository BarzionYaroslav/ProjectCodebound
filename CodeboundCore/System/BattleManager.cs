using Codebound.Entities.Opponents;
using Codebound.Entities;
namespace Codebound.System;
using Codebound.System.Randomness;
using Codebound.Drawing;

public class BattleManager
{
    private static BattleManager? _instance;
    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleManager();
            }
            return _instance;
        }
    }
    public Wave CurrentWave;
    public Hero MainChar;
    private RandomList<string> prepText;
    private List<IEnemyFactory> prepFactories;
    private BattleManager()
    {
        MainChar = new HeroBuilder().SetAtk(5)
                    .SetDef(2)
                    .SetHp(40)
                    .SetMana(40)
                    .SetName("Rika")
                    .SetFace("rika_extra")
                    .Build();
        CurrentWave = new Wave();
        IRandomProvider random = GameManager.Instance.Randomizer;
        int choice = random.GetInt(18);
        switch (choice)
        {
            case 0:
                prepText = new RandomList<string>(["THEY OPENED THE GAME!!! RATTLE 'EM BOYS!!!"]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,0),
                    new SkulatraFactory(30,-1,0),
                    new SkulatraFactory(60,-2,0)
                    ]
                );
                break;
            case 1:
                prepText = new RandomList<string>([
                    "Punchy Bad accidentally blocked your path. You intentionally started the fight!",
                    "Punchy Bad fills his Handbads with horseshoes.",
                    "Punchy Bad contemplates his name for a moment. Bad Punch blocks your path!",
                    "Punchy Bad considers sandbagging, but remembers that he isn't a Desert Boss.",
                    "Punchy Bad wonders when Yaroslav will finish the screen resize code. He easily gets distracted with a fight!",
                    "Punchy Bad swings in like a wrecking ball!"
                    ]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new PunchyBadHandFactory(0,-3,1),
                    new PunchyBadFactory(30,-16,0),
                    new PunchyBadHandFactory(90-32,-3,1)
                    ]
                );
                break;
            case 3:
                prepText = new RandomList<string>(["I spy with my floating eye something ending with this fight."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new IbiruaiFactory(30,10,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 4:
                prepText = new RandomList<string>(["It stares at you through the stitches."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(30,7,0)
                    ]
                );
                break;
            case 5:
                prepText = new RandomList<string>(["Amblyopia."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,1),
                    new BlaindaiFactory(30,7,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 6:
                prepText = new RandomList<string>(["Sleep eternal quiescent dreams."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(2,6,1),
                    new BlaindaiFactory(30,7,0),
                    new BlaindaiFactory(58,6,1)
                    ]
                );
                break;
            case 7:
                prepText = new RandomList<string>(["Uneasy alliance."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new BlaindaiFactory(30,7,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            case 8:
                prepText = new RandomList<string>(["Not again..."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new IbiruaiFactory(30,7,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            case 9:
                prepText = new RandomList<string>(["Ibiruai would take revenge, if only context for it was provided."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new SkulatraFactory(30,-1,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 10:
                prepText = new RandomList<string>(["THEY OPENED THE GAME!!! RATTLE 'EM... boys...?"]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(2,6,2),
                    new SkulatraFactory(30,-1,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 11:
                prepText = new RandomList<string>(["Yokanten slides in!"]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new YokantenFactory(37,15,0)
                    ]
                );
                break;
            case 12:
                prepText = new RandomList<string>(["Early Game Enemy convention."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new YokantenFactory(37,15,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 13:
                prepText = new RandomList<string>(["Maybe the true enemies were the friends we made along the way..."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiderFactory(30,-6,0)
                    ]
                );
                break;
            case 14:
                prepText = new RandomList<string>(["Kneel before the Great Ibiruai Tamer! Let his matcha colors be known across the lands!"]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new IbiruaiderFactory(30,-6,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 15:
                prepText = new RandomList<string>(["Desu Mashin: Mk.I flies in!"]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new MekaiFactory(30,1,0)
                    ]
                );
                break;
            case 16:
                prepText = new RandomList<string>(["An Ibiruai, a Mek-AI and a Blaindai walk into a bar..."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new MekaiFactory(30,1,0),
                    new BlaindaiFactory(58,6,2)
                    ]
                );
                break;
            case 17:
                prepText = new RandomList<string>(["Finally an eye who understands..."]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new MekaiFactory(30,1,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            default:
                prepText = new RandomList<string>(["Punchy Bag swings in like a fluff-filled pinata!"]);
                prepFactories = new List<IEnemyFactory>(
                    [
                    new PunchyBagFactory((90-20)/2,-6,0)
                    ]
                );
                break;
        }
    }

    public void PrepareFight()
    {
        GameManager.Instance.MainPanel.RText = prepText.GetRandom();
        foreach (IEnemyFactory i in prepFactories)
        {
            CurrentWave.Add(i.Create());
        }
    }

    public void StartEnemyTurn()
    {
        GameManager.Instance.MainPanel.Active = false;
        GameManager.UpdateStarted += EnemyTurn;
        sleepyTime = defaultPlayerTurnLength;
    }

    public void EnemyTurn()
    {
        if (sleepyTime == 0)
        {
            if (currentEnemyIndex < CurrentWave.Count)
            {
                sleepyTime = CurrentWave[currentEnemyIndex].DoAction();
                CurrentWave[currentEnemyIndex].RandomizeAction();
                currentEnemyIndex++;
            }
            else
            {
                currentEnemyIndex = 0;
                EndEnemyTurn();
            }
        }
        else
        {
            sleepyTime--;
        }
    }

    public void EndEnemyTurn()
    {
        GameManager.Instance.MainPanel.RText = prepText.GetRandom();
        GameManager.Instance.MainPanel.Active = true;
        GameManager.UpdateStarted -= EnemyTurn;
    }

    public Sprite AddEffect(int enemyIndex, string effectAsset, float animateSpeed)
    {
        Enemy enemyInUse = CurrentWave[enemyIndex];
        Sprite effect = new SpriteBuilder().SetSprite(effectAsset)
            .SetImageSpeed(animateSpeed)
            .SetPosition(
                enemyInUse.Body[Enemy.BodyName].X + (enemyInUse.Body[Enemy.BodyName].DrawWidth / 2),
                enemyInUse.Body[Enemy.BodyName].Y + (enemyInUse.Body[Enemy.BodyName].DrawHeight / 2)
                )
            .SetDepth(0)
            .SetVanish(true)
            .Build();
        effect.X -= effect.DrawWidth / 2;
        effect.Y -= effect.DrawHeight / 2;
        return effect;
    }

    private int sleepyTime = 0;
    private int currentEnemyIndex = 0;
    private readonly int defaultPlayerTurnLength = 40;
}