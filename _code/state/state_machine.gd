class_name StateMachine
extends Node

var current_state: State
	
func change_state(new_state:State):
	if current_state is State:
		current_state.exit()
	new_state.enter()
	current_state = new_state
	
