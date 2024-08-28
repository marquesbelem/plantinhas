class_name CursorDefaultState
extends State

func enter() ->void:
	super.enter()
	Input.set_default_cursor_shape(Input.CursorShape.CURSOR_ARROW)
	
func _process(delta):
	if is_running == false:
		return


