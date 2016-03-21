using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// State Machines are responsible for processing states, notifying them when they're about to begin or conclude, etc.
public class StateMachine
{
	protected State _current_state;
	
	public void ChangeState(State new_state)
	{
		if(_current_state != null)
		{
			_current_state.OnFinish();
		}
		
		_current_state = new_state;
		// States sometimes need to reset their machine. 
		// This reference makes that possible.
		_current_state.state_machine = this;
		_current_state.OnStart();
	}
	
	public void Reset()
	{
		if(_current_state != null)
			_current_state.OnFinish();
		_current_state = null;
	}
	
	public virtual void Update()
	{
		if(_current_state != null)
		{
			float time_delta_fraction = Time.deltaTime / (1.0f / Application.targetFrameRate);
			_current_state.OnUpdate(time_delta_fraction);
		}
	}

	public bool IsFinished()
	{
		return _current_state == null;
	}
}

// A State is merely a bundle of behavior listening to specific events, such as...
// OnUpdate -- Fired every frame of the game.
// OnStart -- Fired once when the state is transitioned to.
// OnFinish -- Fired as the state concludes.
// State Constructors often store data that will be used during the execution of the State.
public class State
{
	// A reference to the State Machine processing the state.
	public StateMachine state_machine;
	
	public virtual void OnStart() {}
	public virtual void OnUpdate(float time_delta_fraction) {} // time_delta_fraction is a float near 1.0 indicating how much more / less time this frame took than expected.
	public virtual void OnFinish() {}
	
	// States may call ConcludeState on themselves to end their processing.
	public void ConcludeState() { state_machine.Reset(); }
}


// Defined for animation controls
public class AnimStateMachine : StateMachine
{
    public new AnimState _current_state;
    public AnimState default_anim;
    public Animation anim;
    public AnimStateMachine (Animation anim, AnimState default_anim)
    {
        this.anim = anim;
        this.default_anim = default_anim;
    }
    public void ChangeState(AnimState new_state)
    {
        if (_current_state != null)
        {
            _current_state.OnFinish();
        }
        if (_current_state.can_exit)
        {
            //do the normal change state thing
            _current_state = new_state;
            _current_state.state_machine = this;
            _current_state.OnStart();
        }
        else
        {
            return;
        }

        
    }
    public override void Update()
    {
        base.Update();
        //control block for state transition
    }
}

public enum PlayerState { idle,moving,attacking,jumping};

public class AnimState :State
{
    public new AnimStateMachine state_machine;
    public PlayerState player_state;
    public string anim_clip;
    public bool can_exit = false;
    private bool last_anim_over = true;
    public AnimState(string anim_clip, bool can_exit, PlayerState player_state)
    {
        this.anim_clip = anim_clip;
        this.can_exit = can_exit;
        this.player_state = player_state;
    }
    public override void OnUpdate(float time_delta_fraction)
    {
        /*if (can_exit)
        {
            state_machine.anim.Play(anim_clip);
        }
        else
        {
            state_machine.anim.PlayQueued(anim_clip);
        }
        */
        state_machine.anim.Play(anim_clip);
    }
    public void setIfExist (bool can_exit = true)
    {
        this.can_exit = can_exit;
    }
}