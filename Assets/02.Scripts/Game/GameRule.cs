public static class GameRule
{
    #region const 변수

    public const int MainSceneCols = 20;
    public const int MainSceneRows = 10;

    #endregion

    #region static 변수

    public static readonly int MainSceneAppleCount = MainSceneCols * MainSceneRows;

    public static int GameSceneCols = 20;
    public static int GameSceneRows = 10;
    public static int GameSceneAppleCount = GameSceneCols * GameSceneRows;
    public static int TargetNumber = 10;
    public static int GameTime = 200;

    #endregion
}
