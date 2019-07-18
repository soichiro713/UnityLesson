using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineTest : MonoBehaviour
{
    enum State : int
    {
        A,
        B,
        C
    }
    StateMachineSprict<State> statemachine = new StateMachineSprict<State>();
    [SerializeField] float time;
    // Start is called before the first frame update
    void Start()
    {
        statemachine.Add(State.A,
        () =>
        {
            time += Time.deltaTime;
            if (time > 3.0f)
            {
                statemachine.ChangeState(State.B);
            }
        },
        () =>
        {
            Debug.LogFormat($"EnterState=(State.A)");
        },
        () =>
        {
            Debug.LogFormat($"EnterState=(State.A)");
        });

        statemachine.Add(State.B,
        () =>
        {
            time += Time.deltaTime;
            if (time > 5.0f)
            {
                statemachine.ChangeState(State.C);
            }
        });

        statemachine.Add(State.C,
        () =>
        {
            time += Time.deltaTime;
            if (time > 3.0f)
            {
                statemachine.ChangeState(State.A);
            }
        },
        () =>
        {
            Debug.LogFormat($"EnterState=(State.C)");
            time = 0.0f;
        },
        () =>
        {
            Debug.LogFormat($"EnterState=(State.C)");
            time = 0.0f;
        });
        statemachine.ChangeState(State.A);
       
    }
    // Update is called once per frame
    void Update()
    {
        statemachine.Update();
    }
}
