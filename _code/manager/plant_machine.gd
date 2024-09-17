extends Node2D
class_name PlantMachine

#region PUBLIC FIELDS
@export var e_terrain_layer: TileMapLayer
@export var e_interactive_layer: TileMapLayer
#endregion

#region PRIVATE FIELDS
var seed_atlas_coords: Vector2i = Vector2i(3,0)
var source_id_atlas: int = 0
var tile_terrain_type_selected: Enums.TERRAIN_TYPE
var tile_terrain_current_state: EnumsStates.PLANT
#endregion 

#region BASE
func _ready():
	pass

func _process(delta):
	pass

func _input(event):
	if event.is_action_pressed("select"):
		var tile_picked : Vector2i = e_terrain_layer.local_to_map(e_terrain_layer.get_global_mouse_position())
		var tile_data: TileData = e_terrain_layer.get_cell_tile_data(tile_picked)
		
		if(tile_data):
			tile_terrain_type_selected = tile_data.get_custom_data("TERRAIN_TYPE") as Enums.TERRAIN_TYPE
			tile_terrain_current_state = tile_data.get_custom_data("STATE") as EnumsStates.PLANT
			
			if(tile_terrain_type_selected != -1):
				initial_tile_state(tile_picked)
				flow_state()
				
				tile_data.set_custom_data("STATE", tile_terrain_current_state)
		else:
			print("no tile data")	
#endregion

#region UTILS		
func initial_tile_state(pos) -> void: 
	tile_terrain_current_state = EnumsStates.PLANT.SEED
	e_interactive_layer.set_cell(pos,source_id_atlas,seed_atlas_coords)	
#endregion

#region FLOW
func flow_state(): 
	if(PlayerData == null || PlayerData.e_plant_resource == null):
		return
		
	match tile_terrain_current_state:
		EnumsStates.PLANT.SEED:
			if(PlayerData.e_plant_resource.e_terrainType == tile_terrain_current_state):
				print("pode plantar")
		
#endregion 
