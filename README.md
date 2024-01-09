### This is a code sample project of Tower Defense created with the Unity game engine and C#.

### Single sentence concept 
Endless tower defense where you upgrade various weapons of the only tower and protect it from enemies.

### Gameplay Video ([youtube](https://youtu.be/xxPMwKHqqY8)) 
[![demo video preview](https://img.youtube.com/vi/xxPMwKHqqY8/0.jpg)](https://youtu.be/xxPMwKHqqY8)


### The whole concept
You have a tower with 5 different weapons, you have passive money income and you receive money after killing enemies, you can spend money on various upgrades - they can improve the tower's or weapon's attributes. Enemies of different types spawn and come towards the tower, at the range of attack they stop and start attacking. Your main goal is to stay alive as long as possible.

### Weapon types: 
- <b>Debuff</b>: fires blue projectiles at a high range hitting random enemies, but every enemy can be hit only once. When upgraded it can apply debuffs on targets: decrease the target's armor or increase the reward for killing the target.
- <b>Focus Fire</b>: rapidly fires red projectiles at a low range hitting one target until it dies then selecting the next closest target. When upgraded it can deal more damage with every hit on the same target or decrease enemy armor.
- <b>Health Steal</b>: hits enemies with a green beam at a low range hitting one target until it dies then selecting the next random target. When upgraded it can heal a tower with every hit or heal a tower with every kill.
- <b>Splash</b>: hits enemies with a purple wave at a low range simultaneously hitting all reachable targets. When upgraded deals more damage depending on the number of targets or hits at a higher range. 
- <b>Vengeance</b>: fires orange projectiles hitting random enemies who attacked the tower. When upgraded deals damage depending on how many times the target attacked the tower or decreases the current attack's cooldown on every enemy's attack.

### Project architecture:
Unity version 2021.3.15f1


The main actors are [Tower](TowerDefense/Assets/Scripts/Game/Tower.cs) and [Unit](TowerDefense/Assets/Scripts/Game/Unit.cs) components that can contain children with [Weapon](TowerDefense/Assets/Scripts/Game/WeaponSystem/AbstractWeapon.cs) components, Weapon has [TargetSelector](TowerDefense/Assets/Scripts/Game/WeaponSystem/TargetSelectors/AbstractTargetSelector.cs) defining who the weapon will attack. The player can buy [upgrades](TowerDefense/Assets/Scripts/Game/AttributeSystem/Upgrades/Upgrade.cs), and upgrades apply [buffs](TowerDefense/Assets/Scripts/Game/AttributeSystem/Buffs/AbstractBuff.cs) to the tower or the tower's weapons, and buffs can modify [attributes](TowerDefense/Assets/Scripts/Game/AttributeSystem/AttributeType.cs) of tower, unit, or weapon. Buffs can be applied directly or by hitting an enemy. Attribute values are managed with [AbstractAttributeOwner](TowerDefense/Assets/Scripts/Game/AttributeSystem/AbstractAttributeOwner.cs) subclasses and buffs are managed with [BuffOwner](TowerDefense/Assets/Scripts/Game/AttributeSystem/Buffs/BuffOwner.cs) class.
There are a bunch of [attributes](TowerDefense/Assets/Scripts/Game/AttributeSystem/AttributeType.cs), such as money passive income, money kill bonus, and armor, that can be buffed.

The most interesting part about this solution is that you can easily create different enemies, weapons, upgrades, and buffs in the Unity editor and it's easy to extend behavior if you will need new gameplay attributes or new completely different weapons.

There is a custom object pool implementation in the project for demonstration purposes (but I am aware of Unity's built-in object pools). 

All models, materials, and explosion effects are from free sources. Icons are generated with neural networks. The portal shader graph and ugly weapon attack animation effects are my creations though.
