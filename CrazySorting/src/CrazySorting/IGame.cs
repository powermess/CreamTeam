internal interface IGame
{
    void GameOver();
    void LoadMainMenu();
    void OnCharacterSpawned(Character character);
    void OnCharacterDespawned(Character character);
}