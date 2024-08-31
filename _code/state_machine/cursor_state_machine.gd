class_name CursorStateMachine
extends Node2D

@export var cursor_sprite: Sprite2D 

var current_state: EnumsStates.CURSOR

func  _ready():
	current_state = EnumsStates.CURSOR.DEFAULT
	
func _process(_delta):
	match  current_state:
		EnumsStates.CURSOR.DEFAULT:
			print("We are number DEFAULT!")
		EnumsStates.CURSOR.SELECTED:	
			cursor_sprite.global_position = get_global_mouse_position()
			
func set_state_selected(res:PlantResource=null) -> void: 
	current_state = EnumsStates.CURSOR.SELECTED
	cursor_sprite.texture = res.icon
	
func set_state_default() -> void: 
	current_state = EnumsStates.CURSOR.DEFAULT
	cursor_sprite.texture = null
	
func has_current_state(state:EnumsStates.CURSOR) -> bool: 
	return current_state == state
