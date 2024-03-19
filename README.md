# LCSpawnOnPlayerFix
Ever got an **Eyeless Dog** or **Forest Giant** spawn directly on top of you? Yeah... This mod fixes that.

NB: It only works on outside monsters, stay clear of vents if you want to avoid inside monsters spawning on you

## Details
<details open>
  <summary>Click to expand/collapse</summary>

The `RoundManager.SpawnRandomOutsideEnemy()` function take a list of spawn points that are already pre-placed on each moon, and unlike inside the factory where those points are represented by vents, outside those points are invisible and arbitrarily scattered around.  
So the player can either use some debug tool and learn all the spawn points to dodge them, but there are easily over 40 of them per moon, or they can accept their fate whenever a monster spawn on top of them and kills a perfectly legit run.  
What this mod does is filter out any spawn points too close to players that are still alive and not in the ship (so monsters can still spawn on dead bodies and near the ship).  
It also allows more upward safety to account for the Jetpack as a player can't look directly below their feet and probably wouldn't do it when landing anyway, which should also work when dropping from pipes for instance.

Here is some debug example in action (red line means a monster can spawn, green means it's safe):  
![debugSpawnPoints.gif](https://github.com/tonydimalta/LCSpawnOnPlayerFix/tree/main/LCSpawnOnPlayerFix/Package/debugSpawnPoints.gif)

</details>