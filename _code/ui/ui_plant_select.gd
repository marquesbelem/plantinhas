class_name UIPlantSelect
extends Sprite2D

@export var cursor_state_machine: CursorStateMachine
@export var plant_resource: PlantResource; 

func _ready():
	name = plant_resource.name
	texture = plant_resource.icon
	
func _input(event):
	if event.is_action_pressed("select") && cursor_state_machine.current_state is CursorDefaultState:
		if is_pixel_opaque(get_local_mouse_position()):
			cursor_state_machine.set_state_selected(plant_resource)
