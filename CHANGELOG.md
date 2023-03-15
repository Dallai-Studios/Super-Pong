# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.2] (15-03-2023)

### Fixes
- Fixed a bug on the walls colliders that was softlocking the player
- Fixed some 3d models that was not static, making batch calls more expensive

## [1.0.0] (27-02-2023)

### Added
- Game core loop
- Play againts AI and Player 2
- Game main menu
- Game options menu
- Sound effects system
- Music system
- 3 levels of difficult for the AI
- Gameplay countdown animations
- Score system
- Game core loop event system

### Fixes
- Fixed a small bug on the racket and ball colliders when the ball is too fast
- Added a new collider on the walls to reduce the possibility of 'fall ball'
- Now the version comes from the application