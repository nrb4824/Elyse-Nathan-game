# The Curator
A first person 3D adventure, parkour, and stealth game made in Unity. Play as a spy that hunts down ancient artifacts
through a fantasy, Venice-inspired city, sneaking your way into places you shouldn’t be.

The designed level is a tutorial to teach the player the beggining mechanics of the game. The mechanics in the game
include:
  • Running
  • Crouching
  • Sliding
  • Wall Running
  • Ledge Grabbing
  • Rock Climbing (player doesn't fall off the wall and can move left and right easily on the wall)
  • Wall Climbing (A player must carry momentum up the wall, similar to the Nija Warrior wall)
  • Shooting

The tutorial level achieves its purpose of teaching the player the game but does not convey the actual movement the player
will experience in future levels. All the mechanics have been designed to work with each other so a player can find unique 
paths to a destination. Future level design will require the player to carry their momentum and use multiple mechanics together.

Game Features:
  • Two types of AI (I utialize state machines and a default AI class):
    • A spotlight bot that is stationary. If the player steps into the moving light the spotlight bot spawns a drone.
    • Drone: a flying bot that will follow the player and shoot at the player, if the player is out of its detecting
    range the drone will roam around.
  • Menus:
    • Pause menu
    • Controls menu
    • Settings menu
    • Start screen
    • Death menu
    • Victory menu
  • Audio: Only the most basic audio has been implemented so far. It is almost all placeholder until proper
  sound effects and music can be made.
![GameConcept-E-N 1_13_2024 4_21_56 PM](https://github.com/nrb4824/Elyse-Nathan-game/assets/78773812/481488b3-2072-46fe-a44a-266d5f2e3217)

  
Assets Used:
Health Bars: https://assetstore.unity.com/packages/2d/gui/icons/rpg-unitframes-1-powerful-metal-95252
Enemies: https://assetstore.unity.com/packages/3d/characters/robots/scifi-enemies-and-vehicles-15159
Gun: https://assetstore.unity.com/packages/3d/props/guns/rifle-enfield-base-95001
SkyMap: https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-sky-61217
DOTween for ease of Camera movement.
Laser shot: https://assetstore.unity.com/packages/tools/particles-effects/volumetric-lines-29160
Bird Calls: https://assetstore.unity.com/packages/audio/sound-fx/animals/sparrow-sounds-158174
Menu Music: https://assetstore.unity.com/packages/audio/music/25-fantasy-rpg-game-tracks-music-pack-240154
Footstep and jump sound: https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879

