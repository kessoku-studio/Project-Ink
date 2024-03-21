using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCarloState
{

    public int Score = 0;

    public List<MonteCarloState> nextStates;
    public MonteCarloState parentState;
    public MonteCarloState DeepCopy(){
        return new MonteCarloState();
    }

    private void getAvailableActions(){

    }


    private MonteCarloState SimulateNextState(){
        return new MonteCarloState();
    } 
}
