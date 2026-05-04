# Project Codebound

## Description

Simple RPG game project in `C#` made as part of study material. Concrete list of features will vary with the study session. 

Starting ideas are as follows:

- UI and enemy placement inspired by ***Earthbound*** (***Mother*** series of games) ~~as the name suggests~~

- Dynamic backgrounds and enemy animations

- *Boss-rush* type gameplay (game consisting mostly of enemy waves with little rest in-between)

## Launch

Project can be accessed via command line with `dotnet run --project CodeboundCore`

***IMPORTANT NOTE***: Project *needs* to have its 180 X 50 buffer size to be rendered. The resize code is *still unfinished*, so for now make sure it is, indeed, 180 X 50. You can change it by lowering the font size in the terminal settings.

## Docs

### States

#### ***WIP***

### Strategy

#### Used by

* `Entities/Opponents/EnemyActions`
* `System/UI/ButtonStrategy`

### Observer 

#### Used by

* `System/GameManager`

#### Comments

Events are used for rendering, updates, input and changes to buffer size. Usually, rendering is used by the `Drawing/Dynamic/IDrawableDynamic` classes, update is used by `Entities/IEntity`

### Adapter

#### Used by

* `System/Randomness/RandomAdapter`

#### Comments

Used to adapt Random to IRandomProvider

### Decorator

#### Used by

* `Items/Weapons/Decorators`

#### Comments

Inventory isn't yet done, and as such is located at `System/UI/Panel`

### Builder

#### Used by

* `Entities/HeroBuilder`
* `Entities/Opponents/EnemyBuilder`

### Factory

#### Used by 

* `Entities/Opponents/EnemyFactories`

#### Comments

EnemyFactories uses it to create each of the Enemy class

### Singleton

#### Used by

* `System/GameManager`
* `System/BattleManager`