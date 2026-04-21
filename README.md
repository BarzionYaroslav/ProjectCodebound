# Project Codebound

## Comment to Task 8

For Task 8, adapter was chosen. Since the game will need quite a bit of randomness, it would be good to put that in one randomizer class. So far not much of the game actually *uses* randomness, except, like, Skulatra and GameManager, but I do suspect it will be more needed later.

## Description

Simple RPG game project in `C#` made as part of study material. Concrete list of features will vary with the study session. 

Starting ideas are as follows:

- UI and enemy placement inspired by ***Earthbound*** (***Mother*** series of games) ~~as the name suggests~~

- Dynamic backgrounds and enemy animations

- *Boss-rush* type gameplay (game consisting mostly of enemy waves with little rest in-between)

## Docs
***Strategy*** code can be found at `Entities/Opponents/EnemyActions`. It is used by the Enemy class and gets called in `System/BattleManager` to make the turns work.

***Observer*** code can be found in `System/GameManager`. Events are used for rendering, update, input and change to buffer size. Rendering is mostly used by `IDrawableDynamic` classes, update is used mostly by `IEntity` classes

***Adapter*** code can be found at `System/Randomness/RandomAdapter`, where C#'s `Random` is adapted to `IRandomProvider`

***Decorator*** code can be found at `Items/Weapons/Decorators`, where Decorators are used for weapons. The testing "inventory" is located in `System/UI/Panel` for the time being

***Builder*** code can be found at `Entities`, where it is used for both enemies in `Entities/Opponents` and Hero

***Factory*** code can be found at `Entities/Opponents/EnemyFactories`, where it is used for each enemy type in `Entities/Opponents/EnemyTypes`

***Singleton*** is used in both `GameManager` and `BattleManager` classes at `System`

## Launch

Project can be accessed via command line with `dotnet run --project CodeboundCore`

***IMPORTANT NOTE***: Project *needs* to have its 180 X 50 buffer size to be rendered. The resize code is *still unfinished*, so for now make sure it is, indeed, 180 X 50. You can change it by lowering the font size in the terminal settings.