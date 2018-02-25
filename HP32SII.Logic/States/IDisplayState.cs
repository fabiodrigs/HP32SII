namespace HP32SII.Logic.DisplayStates
{
    interface IDisplayState
    {
        string Mainline { get; }
        bool IsWarningDisplayed { get; }
        bool IsAlphabetDisplayed { get; }

        IDisplayState HandleButton(Button button);
    }
}
