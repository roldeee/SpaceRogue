Team: Trash Pandas
Game: Space Rogue
Starting Scene: Assets/Scenes/StartMenu

How to Play:
Move the player with WASD. Change directions with the mouse. Use "F" key near a door to go to the next the room. Press "Shift" to dash and evade enemies. Press ESC to access the Pause Menu. You play as a astronaut on a spaceship that has been taken over by aliens. The goal is to fight through various rooms, kill aliens, and escape the ship. The game will get harder after successful escapes.

Observed Tech Requirements:
- Game Feel
-- Goal described at the beginning of a new run.
-- During play, you can press ESC to pause the game and access a menu that allowsn restarting game.
- Fun Gameplay
-- Player make choices such as choosing which room reward to go for and how to spend money in the shop.
-- Player is rewarded gold for killing aliens.
-- Starting room describes the goals and controls that is safe from the invading aliens. Allows player to practice the controls.
- 3D Character w/ Control
-- Player is an astronaut that is controlled by using a mouse and keyboard.
-- Audio and visual feedback when the player damage.
-- Enemies ragdoll when a barrel is shot near them. 
- 3D World w/ Physics
-- Exploding crates
- Steering Behaviors & AI
-- Enemy pathfinding is powered by a Nav Mesh in each room.
-- Enemy states include idling, roaming, chasing, and attacking.
-- Audio feedback when an alien is attacking and destroyed.
- Polish
-- Includes start, pause, and game over menus.
-- Doors and rewards show tooltipp when near.
-- Particle physics use when dashing with "Shift"

Known problem areas:
- Game will crash on rare ocassions in Scene4
- Enemy slides towards Player when attacking.

Manifest:
Harold Sandoval
- Scripts:
AudioEvents, AudioEventManager, EnemyHealth, EnemyAI, EnemyAttackEmitter, EnemySpawner, PlayerHealth, EventManager, DontDestroyOnLoad, GameQuitter, GameStarter, PauseMenuToggle, UnpauseGame

-Assets:
Basic enemies (w/ animation), Sounds, Skybox, Background Music, Fonts, Start Menu, Pause Menu, Rewards Prefabs, AudioEventManager, Damage Audio/Visual Feedback

Christopher Luafau
- Scripts:
CreditsHandler, ExplosiveBarrel, ExplosiveBarrelSpawner, PlayerController, PlayerData, PlayerDataManager, PlayerWeapon, Projectile, RewardObtainer, RewardsHandler, RoomClearChecker, CameraController, RotateWithMouse
- Assets:
Projectiles, player weapon, rooms, player and animation, player dashing effects, rewards, boss and animation, health HUD canvas.

Richard Lee:
- Scripts: MapController (Procedural Level Generation), Levels, LevelTree, WinStreak, EntryMenu, GameOverMenu, Destroyable, EnemyHealth
- Assets: Basic Room, Spawn Room, Room Clear Count UI, Enemy Health Bar, Game Over Menu, EntryMenu, Dynamic Doors & Waypoints in Each Scene

Junwei Li:
- Scripts: ShopPanel, PlayerDataPanel, PlayerController, CameraController, EnemyAI, Player
- Assets: Alien, Cocoons, Rooms (Multiple), StartScene, Scene2, Scene3, Scene4

Ziyin Zhang:
