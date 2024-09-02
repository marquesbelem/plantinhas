extends Node2D

#region PRIVATE FIELDS
var level = get_tree().get_nodes_in_group("level_group")
var ui = get_tree().get_nodes_in_group("ui_group")
#endregion


# Called when the node enters the scene tree for the first time.
func _ready():
	ui.get
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
