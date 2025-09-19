namespace CoreFlashToolPro
{
    public sealed class AppSettings
    {
        public string Title { get; set; } = "Core Flash Tool Pro";
        public string Theme { get; set; } = "Dark";
    }

    public sealed class ToolsSettings
    {
        public bool DisableVbmetaByDefault { get; set; } = true;
    }

    public sealed class MtkSettings
    {
        public bool ForceBrom { get; set; } = true;
        public bool IncludePreloader { get; set; } = false;
    }

    public sealed class UserSettings
    {
        public string Username { get; set; } = "";
        public bool RememberLogin { get; set; } = false;
    }
}
