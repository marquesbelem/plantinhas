class_name PlantResource
extends Resource

#region PUBLIC FIELDS
@export var e_name:String
@export var e_terrainType: Enums.TERRAIN_TYPE
@export var e_icon:Texture2D
#endregion

func log() -> void: 
	var format_string = "%s do Terreno %s"
	print(format_string % [e_name, e_terrainType])
