namespace Script.Utils
{
    public class Constant
    {
        public const string SpriteIndex = " <sprite=0>";
        public const string ArrowCountError = "Arrow count can't be more than 5";
        public const int LastLevel = 16;

        public static readonly string[] Tips =
        {
            "Don't forget to use upgrades they will make you stronger.",
            "By buying 'Arrow Count Upgrade' you can fire more than one arrow.",
            "Even if you fail a level you still keep every gold and diamond from that level. That means you can return stronger.",
            "Try to focus on range units first.",
            "It is normal to fail a level try to upgrade your gear and try again"
        };

        public const int MenuScene = 0;
        public const int MapScene = 1;
        public const int GameScene = 2;
        public const int LoadingScene = 3;
    }
}