using System.Collections.Generic;

internal interface IGame
{
    List<Character> ActiveCharacters { get; }
    void GameOver();
    void LoadMainMenu();
    void OnCharacterSpawned(Character character);
    void OnCharacterDespawned(Character character);
}