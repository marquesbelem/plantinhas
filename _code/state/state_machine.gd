class_name StateMachine
extends Node

var current_state: State
	
func change_state(new_state:State, res:PlantResource):
	if current_state is State:
		current_state.exit()
	new_state.enter(res)
	current_state = new_state
	
