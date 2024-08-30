class_name CursorStateMachine
extends StateMachine

@export var state_default: CursorDefaultState
@export var state_selected: CursorSelectedState

# Called when the node enters the scene tree for the first time.
func _ready():
	set_state_default()
	
func set_state_selected(res:PlantResource=null) -> void: 
	print("%s" % res.name)
	change_state(state_selected, res)
	
func set_state_default(res:PlantResource=null) -> void: 
	change_state(state_default, res)
