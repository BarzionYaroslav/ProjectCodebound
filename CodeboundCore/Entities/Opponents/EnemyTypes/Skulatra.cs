using Codebound.Drawing;
using Codebound.System;
using Codebound.System.Functions;
namespace Codebound.Entities.Opponents;

public class Skulatra : Enemy
{
    public Skulatra() : base()
    {
        Expectations.Add(HeadName);
        body.ChangeExpectations(Expectations);
        danceChoice = GameManager.Instance.Randomizer.GetInt(danceNumber);
    }

    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        Sprite head = body[HeadName];
        var degree = (360 / bod.ImageCount) * bod.ImageIndex;
        switch (danceChoice)
        {
            case 1:
                DefaultDance(degree, defaultHeadIndex, rightHeadIndex, leftHeadIndex);
                break;
            default:
                DefaultDance(degree, defaultHeadIndex, leftHeadIndex, rightHeadIndex);
                break;
        }
        var changeY = Math.Abs(MathFunctions.DCos(degree) * waveYMagnitude);
        head.Y = head.StartY - (int)changeY;
    }

    private void DefaultDance(float degrees, int defaultIndex, int leftIndex, int rightIndex)
    {
        Sprite head = body[HeadName];
        var changeX = MathFunctions.DSin(degrees) * waveXMagnitude;
        head.X = head.StartX - (int)changeX;
        if (changeX >= changeXTreshold)
            head.ImageIndex = rightIndex;
        else if (changeX <= -changeXTreshold)
            head.ImageIndex = leftIndex;
        else
            head.ImageIndex = defaultIndex;
    }
    private int danceChoice;
    private readonly int danceNumber = 2;
    private readonly int changeXTreshold = 1;
    private readonly int defaultHeadIndex = 0;
    private readonly int leftHeadIndex = 1;
    private readonly int rightHeadIndex = 3;
    private readonly int waveYMagnitude = 2;
    private readonly int waveXMagnitude = 2;
    static public readonly string HeadName = "head";
}