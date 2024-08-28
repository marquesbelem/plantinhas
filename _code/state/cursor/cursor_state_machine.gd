class_name CursorStateMachine
extends StateMachine

@export var state_default: CursorDefaultState
@export var state_selected: CursorSelectedState

# Called when the node enters the scene tree for the first time.
func _ready():
	change_state(state_default)
	
func _input(event):
	if event.is_action_pressed("click"):
		print("oi")
