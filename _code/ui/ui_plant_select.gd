class_name UIPlantSelect
extends Sprite2D

#region PUBLIC FIELDS
@export var e_cursor_state_machine: CursorStateMachine
@export var e_plant_resource: PlantResource
#endregion 

#region BASE
func _ready():
	name = e_plant_resource.e_name
	texture = e_plant_resource.e_icon
	
func _input(event):
	if event.is_action_pressed("select") && e_cursor_state_machine.has_current_state(EnumsStates.CURSOR.DEFAULT):
		if is_pixel_opaque(get_local_mouse_position()):
			e_cursor_state_machine.set_state_selected(e_plant_resource)
			
	elif event.is_action_pressed("select") && e_cursor_state_machine.has_current_state(EnumsStates.CURSOR.SELECTED):		
		if is_pixel_opaque(get_local_mouse_position()):
			e_cursor_state_machine.set_state_selected(e_plant_resource)
#endregion
