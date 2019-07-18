using System;
using System.Collections.Generic;

public class StateMachineSprict<T>//ジェネレート関数（テンプレート）
{
    private class State
    {
        //関数
        private readonly Action mEnterAction; //Action...引数なし戻り値なし
        private readonly Action mUpdateAction;//readonly...c++のconst *
        private readonly Action mExitAction;
        public State(Action updateAct = null,
        Action enterAct = null,
        Action exitAct = null)
        {
            //??演算子　c#限定
            mUpdateAction = updateAct ?? delegate { };
            mEnterAction = enterAct ?? delegate { };
            mExitAction = exitAct ?? delegate { };
        }

        public void Update()
        {
            mUpdateAction();
        }
        public void Enter()
        {
            mEnterAction();
        }
        public void Exit()
        {
            mExitAction();
        }
    }

   
    private Dictionary<T, State> mStateDictionary = new Dictionary<T, State>();

    private State mCurrentState;

    public void Add(T key, Action updateAct = null,
        Action enterAct = null,
        Action exitAct = null)
    {
        mStateDictionary.Add(key, new State(updateAct, enterAct, exitAct));
    }
    public void ChangeState(T key)
    {
        mCurrentState?.Exit();
        mCurrentState = mStateDictionary?[key];
        mCurrentState?.Enter();
    }

    // Update is called once per frame
    public void Update()
    {
        if (mCurrentState == null)
        {
            return;
        }
        mCurrentState.Update();
    }
    public void Clear()
    {
        mStateDictionary.Clear();
        mCurrentState = null;
    }
}
