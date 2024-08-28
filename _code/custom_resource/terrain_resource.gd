class_name TerrainResource
extends Resource

@export var terrainType: Enums.TERRAIN_TYPE
@export var icon:Texture2D

func log() -> void: 
	var format_string = "Terreno %s"
	print(format_string % [terrainType])
