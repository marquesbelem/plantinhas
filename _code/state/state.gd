class_name State
extends Node

var is_running:bool = false

func enter(res:PlantResource) -> void:
	is_running = true
	
func exit() -> void:
	is_running = false
