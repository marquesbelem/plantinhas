class_name CursorStateMachine
extends Node2D

#region PUBLIC FIELDS
@export var e_cursor_sprite: Sprite2D 
#endregion

#region PRIVATE FIELDS
var current_state: EnumsStates.CURSOR
#endregion

#region BASE
func  _ready():
	current_state = EnumsStates.CURSOR.DEFAULT
	
func _process(_delta):
	match  current_state:
		EnumsStates.CURSOR.SELECTED:	
			e_cursor_sprite.global_position = get_global_mouse_position()
#endregion 
			
func set_state_selected(res:PlantResource=null) -> void: 
	current_state = EnumsStates.CURSOR.SELECTED
	e_cursor_sprite.texture = res.e_icon
	PlayerData.e_plant_resource = res
	
func set_state_default() -> void: 
	current_state = EnumsStates.CURSOR.DEFAULT
	e_cursor_sprite.texture = null
	
func has_current_state(state:EnumsStates.CURSOR) -> bool: 
	return current_state == state
