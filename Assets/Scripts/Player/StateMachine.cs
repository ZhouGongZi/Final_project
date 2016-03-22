using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// State Machines are responsible for processing states, notifying them when they're about to begin or conclude, etc.
public class StateMachine
{
	protected State _current_state;
	
	public virtual void ChangeState(State new_state)
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
<<<<<<< HEAD
        default_anim.state_machine = this;
        _current_state = default_anim;
        _current_state.state_machine = this;
=======
		this._current_state=default_anim;
		this._current_state.state_machine = this;
		default_anim.state_machine = this;
>>>>>>> 5cfb6a0dbc889660d04eb0848231e7ae0f91ba73
    }
    public void ChangeState(AnimState new_state)
    {
        if (_current_state != null)
        {
            _current_state.OnFinish();
        }
            //do the normal change state thing
            _current_state = new_state;
            _current_state.state_machine = this;
            _current_state.OnStart();
    }
    public override void Update()
<<<<<<< HEAD
    {
        if (_current_state != null)
        {
            float time_delta_fraction = Time.deltaTime / (1.0f / Application.targetFrameRate);
            _current_state.OnUpdate(time_delta_fraction);
        }
=======
	{	if(_current_state != null)
		{
			float time_delta_fraction = Time.deltaTime / (1.0f / Application.targetFrameRate);
			_current_state.OnUpdate(time_delta_fraction);
		}
        //base.Update();
>>>>>>> 5cfb6a0dbc889660d04eb0848231e7ae0f91ba73
        //control block for state transition
    }
    public void SetToDefaultState()
    {
        ChangeState(new AnimState("ready 2", _current_state.player_state, PlayerState.idle));
    }
}

public enum PlayerState { none,idle,moving,attacking, jumping };

public class AnimState :State
{
    public new AnimStateMachine state_machine;
    public PlayerState player_state;
    public string anim_clip;
    public bool if_queued = false;
    public AnimState(string anim_clip, PlayerState last_state, PlayerState player_state)
    {
        this.anim_clip = anim_clip;
        if ((int)last_state <= (int)player_state)
        {
            if_queued = false;
        }
        else
        {
            if_queued = true;
        }
        this.player_state = player_state;
    }
    public override void OnUpdate(float time_delta_fraction)
    {
        if (!if_queued)
        {
            state_machine.anim.Play(anim_clip);
        }
        else
        {
            state_machine.anim.PlayQueued(anim_clip);
        }
        
    }
    public void setIfExist (bool if_queued = true)
    {
        this.if_queued = if_queued;
    }
}