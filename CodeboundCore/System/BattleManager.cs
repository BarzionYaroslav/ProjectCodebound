using Codebound.Entities.Opponents;
using Codebound.Entities;
namespace Codebound.System;

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
    private string prepText;
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
                prepText = "THEY OPENED THE GAME!!! RATTLE 'EM BOYS!!!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,0),
                    new SkulatraFactory(30,-1,0),
                    new SkulatraFactory(60,-2,0)
                    ]
                );
                break;
            case 1:
                switch (random.GetInt(5))
                {
                    case 0:
                        prepText = "Punchy Bad accidentally blocked your path. You intentionally started the fight!";
                        break;
                    case 1:
                        prepText = "Punchy Bad fills his Handbads with horseshoes.";
                        break;
                    case 2:
                        prepText = "Punchy Bad contemplates his name for a moment. Bad Punch blocks your path!";
                        break;
                    case 3:
                        prepText = "Punchy Bad considers sandbagging, but remembers that he isn't a Desert Boss.";
                        break;
                    case 4:
                        prepText = "Punchy Bad wonders when Yaroslav will finish the screen resize code. He easily gets distracted with a fight!";
                        break;
                    default:
                        prepText = "Punchy Bad swings in like a wrecking ball!";
                        break;
                }
                prepFactories = new List<IEnemyFactory>(
                    [
                    new PunchyBadHandFactory(0,-3,1),
                    new PunchyBadFactory(30,-16,0),
                    new PunchyBadHandFactory(90-32,-3,1)
                    ]
                );
                break;
            case 3:
                prepText = "I spy with my floating eye something ending with this fight.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new IbiruaiFactory(30,10,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 4:
                prepText = "It stares at you through the stitches.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(30,7,0)
                    ]
                );
                break;
            case 5:
                prepText = "Amblyopia.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,1),
                    new BlaindaiFactory(30,7,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 6:
                prepText = "Sleep eternal quiescent dreams.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(2,6,1),
                    new BlaindaiFactory(30,7,0),
                    new BlaindaiFactory(58,6,1)
                    ]
                );
                break;
            case 7:
                prepText = "Uneasy alliance.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new BlaindaiFactory(30,7,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            case 8:
                prepText = "Not again...";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new IbiruaiFactory(30,7,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            case 9:
                prepText = "Ibiruai would take revenge, if only context for it was provided.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new SkulatraFactory(30,-1,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 10:
                prepText = "THEY OPENED THE GAME!!! RATTLE 'EM... boys...?";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(2,6,2),
                    new SkulatraFactory(30,-1,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 11:
                prepText = "Yokanten slides in!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new YokantenFactory(37,15,0)
                    ]
                );
                break;
            case 12:
                prepText = "Early Game Enemy convention.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new YokantenFactory(37,15,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 13:
                prepText = "Maybe the true enemies were the friends we made along the way...";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiderFactory(30,-6,0)
                    ]
                );
                break;
            case 14:
                prepText = "Kneel before the Great Ibiruai Tamer! Let his matcha colors be known across the lands!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new IbiruaiderFactory(30,-6,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 15:
                prepText = "Desu Mashin: Mk.I flies in!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new MekaiFactory(30,1,0)
                    ]
                );
                break;
            case 16:
                prepText = "An Ibiruai, a Mek-AI and a Blaindai walk into a bar...";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new MekaiFactory(30,1,0),
                    new BlaindaiFactory(58,6,2)
                    ]
                );
                break;
            case 17:
                prepText = "Finally an eye who understands...";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new MekaiFactory(30,1,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            default:
                prepText = "Punchy Bag swings in like a fluff-filled pinata!";
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
        GameManager.Instance.MainPanel.RText = prepText;
        foreach (IEnemyFactory i in prepFactories)
        {
            CurrentWave.Add(i.Create());
        }
    }
}