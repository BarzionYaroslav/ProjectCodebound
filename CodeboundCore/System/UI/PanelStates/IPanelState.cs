using System.Runtime.CompilerServices;

namespace Codebound.System.UI;

public interface IPanelState
{
    int LengthCounter { get; }
    int MaxLength { get; }
    void MoveUpAction();
    void MoveDownAction();
    void SelectAction();
    void BackAction();
    void AddLength(int length);
    void DrawUi();
    void PrepareUi();
    string DrawUiLoop(int i);
    void SetContext(Panel context);
    void ResetVariables();
}