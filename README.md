# CarFun

Welcome to **CarFun**! In this game, you play versus a friend on a racetrack that gets destroyed as either of you touch each piece. Green balls are speed boosts! Try to survive longer than your friend to win.

## Getting Started

Load "MainMenu" first, then everything else should work from there.

## Game Mechanics

I wanted to make this game as streamlined and simple as possible, so the entire game is inside one scene (besides the menu). This lets me reload the game easily by reloading the scene. From there, a random track prefab is selected, and each track piece contains logic for destroying itself. Then, when a player falls, they hit a trigger and the game manager pauses the scene and prompts for a restart.

## Controls

- **Player 1:** `WASD`
- **Player 2:** Arrow Keys
- `r` - restart
- `esc` - main menu
- `p` - pause

## Ref/Assets Used

- RaceTrackParts - [Racetrack Karting Microgame Add-ons](https://assetstore.unity.com/packages/3d/racetrack-karting-microgame-add-ons-174459)
- 3D Wheel Controller (replacement for built-in) - [Wheel Controller 3D](https://assetstore.unity.com/packages/tools/physics/wheel-controller-3d-74512)
- Game Manager - [YouTube Tutorial](https://www.youtube.com/watch?v=4I0vonyqMi8)
- Cart Model (used with permission, classmate) - [Robin Zeitlin Talamo's LinkedIn](https://www.linkedin.com/in/robin-zeitlin-talamo-778a9127a/)
- Coroutines - [YouTube Tutorial](https://www.youtube.com/watch?v=5L9ksCs6MbE)

## Unity Version

Developed using Unity 2021.3.15f1.

## Credits

Created by Dash Corning.