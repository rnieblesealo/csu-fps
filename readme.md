# To-Do List

- [x] Jumping
- [x] Fix walk animation camera shake
- [ ] Start implementing multiplayer
- [ ] ~Fix arms disappearing~

# Specifications

## Equippables

- Delivered via drop crates
- 1 throwable crate every 15 seconds
- After 3 throwable crates, a shootable crate will drop
- *Shootables are upgraded, multi-firable throwables*

### Throwables 

- **Nerve Gas**
    - AOE acid cloud; upgraded by PAU cannon/flamethrower
- **Bomb**
    - Impact AOE damage; upgraded by bazooka
- **Tomahawk**
    - Precise throw weapon; insta-kill on hit; upgraded by crossbow

#### Specs

- **Ammo UI**:
    - Turns to 1 icon w/throwable sprite
    - Square box length
    - Colorize to match throwable
- **Anim**:
    - Hold throwable idle
    - Throw
    - Same for all

### Shootables

One-magazine substitute guns

- **PAU Cannon** 
    - Shoots 5x pellets that leave chemical puddles; stepping on them causes slowdown + constant damage
- **Bazooka** 
    - Fires 3x rockets that cause impact damage
- **Crossbow**
    - Insta-kill precision weapon; bullet has travel time 

#### Specs

- Ammo UI:
    - Shape + text stays same 
    - Colorize text to gun
    - Gun icon sprite 
    - Colorize sprite to gun 
- Arm animations:
    - Custom idle
    - Custom shoot
    - Per gun
