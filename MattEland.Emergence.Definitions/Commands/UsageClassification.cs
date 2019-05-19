namespace MattEland.Emergence.Definitions.Commands
{

    /// <summary>
    /// An enum containing generic usage classification information surrounding commands, for use in choosing how to display
    /// a command or command pickup in the UI.
    /// </summary>
    public enum UsageClassification
    {
        Offensive,
        Defensive,
        Restorative,
        Movement,
        Utility
    }
}