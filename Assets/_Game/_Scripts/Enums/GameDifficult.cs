namespace DallaiStudios.SuperPong.Enums
{
    public enum GameDifficult
    {
        NORMAL,
        HARD,
        UNFAIR,
    }

    public static class GameDifficultExtensions
    {
        public static int Value(this GameDifficult difficult)
        {
            return (int)difficult;
        }
    }
}