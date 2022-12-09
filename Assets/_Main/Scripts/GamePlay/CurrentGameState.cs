using UnityEngine;

public class CurrentGameState : MonoBehaviour
{
    internal GameState state;

    private void Awake()
    {
        UIManager.OnFail += Fail;
        UIManager.OnSuccess += Succes;
        UIManager.OnFail += Fail;
    }

    private void Update()
    {
        if (state == GameState.Stop)
        {
            if (Input.GetMouseButtonUp(0))
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        state = GameState.Play;
    }

    public void Succes()
    {
        state = GameState.Succes;
    }

    public void Fail()
    {
        state = GameState.Fail;
    }
}