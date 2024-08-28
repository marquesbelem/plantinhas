class_name CursorSelectedState
extends State

func enter() ->void:
	super.enter()
	Input.set_default_cursor_shape(Input.CursorShape.CURSOR_POINTING_HAND)

func _process(delta):
	if is_running == false:
		return
