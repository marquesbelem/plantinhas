class_name PlantResource
extends Resource

@export var name:String
@export var terrainType: Enums.TERRAIN_TYPE
@export var icon:Texture2D

func log() -> void: 
	var format_string = "%s do Terreno %s"
	print(format_string % [name, terrainType])
