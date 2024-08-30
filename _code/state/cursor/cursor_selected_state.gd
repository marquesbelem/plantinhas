class_name CursorSelectedState
extends State

var plant_res:Resource;

func enter(res:Resource) ->void:
	super.enter(res)
	plant_res = res; 
	Input.set_default_cursor_shape(Input.CursorShape.CURSOR_POINTING_HAND)

func _process(delta):
	if is_running == false:
		return
