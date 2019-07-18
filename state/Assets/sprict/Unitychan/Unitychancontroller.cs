using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Unitychancontroller : MonoBehaviour
{
    enum AnimationState : int
    {
        Idle,
        Run,
        Jump
    }
    StateMachineSprict<AnimationState> stateMachine = new StateMachineSprict<AnimationState>();

    Animator animator;
    Rigidbody rigidbody;
    AnimatorBehaviour animatorbehavior=null;

    [SerializeField] float rotateSpeed = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float JumpPower = 5.0f;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        InitialiseComponent();
        InitialiseState();
    }
    void InitialiseState()
    {
        stateMachine.Add(AnimationState.Idle, IdleUpdate, IdleInitialize);
        stateMachine.Add(AnimationState.Jump, JumpUpdate, JumpInitialize);
        stateMachine.Add(AnimationState.Run, RunUpdate, RunInitialize);
        stateMachine.ChangeState(AnimationState.Idle);
    }
    void DirectionChange()
    {
        float rot = 0.0f;

        if (Input.GetKey(KeyCode.A))
        {
            rot -= rotateSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rot += rotateSpeed;
        }
        transform.Rotate(0.0f, rot, 0.0f);
    }
    void Move(float aMoveSpeed)
    {
        velocity = transform.TransformDirection(new Vector3(0, 0, aMoveSpeed));
        var position = transform.position = velocity;
        rigidbody.MovePosition(position);
    }
    void InitialiseComponent()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        animatorbehavior = animator.GetBehaviour<AnimatorBehaviour>();
    }
    void IdleUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            stateMachine.ChangeState(AnimationState.Run);
            animator.CrossFadeInFixedTime("Run", 0.0f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            stateMachine.ChangeState(AnimationState.Jump);
            animator.CrossFadeInFixedTime("Jump", 0.0f);
        }
        DirectionChange();
    }
    void RunUpdate()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            stateMachine.ChangeState(AnimationState.Idle);
            animator.CrossFadeInFixedTime("Idle", 0.0f);
        }
        DirectionChange();
    }
    void JumpUpdate()
    {
       /*
        var animeState = animator.GetCurrentAnimatorStateInfo(0);
        if (animeState.normalizedTime > 1.0f)
        {
            stateMachine.ChangeState(AnimationState.Idle);
            animator.CrossFadeInFixedTime("Idle", 0.0f);
        }
        */

    }



    void IdleInitialize()
    {
        // 矢印で遷移していたものをこれで表現できる
                                            // ↓パーセントで設定
        animator.CrossFadeInFixedTime("Idle", 0.0f);
    }
    void RunInitialize()
    {
        // 矢印で遷移していたものをこれで表現できる
        animator.CrossFadeInFixedTime("Run", 0.0f);
    }

    void JumpInitialize()
    {
        // 矢印で遷移していたものをこれで表現できる
        animator.CrossFadeInFixedTime("Jump", 0.0f);
      
    }
  
    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        var animeState = animator.GetCurrentAnimatorStateInfo(0);
        Debug.LogFormat($"before normalizeTime{animeState.normalizedTime}");

        Debug.LogFormat($"after normalizeTime{animatorbehavior.NormalizedTime}");

        if (animatorbehavior.NormalizedTime > 1.0f)
        {
            animatorbehavior.ResetTime();
        }
    }
}
