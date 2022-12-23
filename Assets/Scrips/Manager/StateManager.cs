using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] int life = 0;
    [SerializeField] int Score = 0;
    public int GetLife()
    {
        return life;
    }
    public void SetLoseLife()
    {
        life -= 1;
    }
    public void SetPushLife()
    {
        life += 1;
    }
    public void SetScore(int point)
    {
        Score += point;
    }
    public int GetScore()
    {
        return Score;
    }

}
