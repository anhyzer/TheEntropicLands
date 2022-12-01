Author: Ryan Zimmerman
Game: The Entropic Lands


This MUD (multi-user dungeon) has been created inline with required coursework for Dr. Rodrigo Obando's CPSC3175 - Object-Oriented Design class at 
Columbus State University. The architecture herein is meant to practice various OOD Patterns, promoting scalability and future develoment.
Patterns used:

   Singleton  => Used in GameWorld
   Command    => The game is a Command-driven FSM.
   Observer   => Used in triggering worldtime (24-hr clock - 0-23) Room delegates, victory, and the FightClock (currently hardcoded to 3000ms combat 
rounds)
   Delegate   => Room variations (traps/echo/teleporter)
   Decorators => Used in item sockets (sword damage type => when diamond socketed, normal otherwise)
   Facade     => Used for ICharacter inventory
   State      => ICharacterState currently tracks whether the NPC or Player is in Combat

Currently the only task of the Entropic Lands is to escape the tomb by defeating Vlad and taking his key to unlock the Tomb entrance. The player
has to first kill the Torturer for his diamond, equip the diamond to a socketable weapon (all weapons currently socketable) and then will be able
to kill Vlad for the exit key. This prototype of the game is used to refine the architecture before expanding the persistence, game creation, and 
multiplayer functionalities.

BUGS: 
 |======> Combat system clunky - too many hardcoded interactions
 |======> NPC's DropAll() won't drop items. Current transferrence of items hardcoded
 |======> Player prompt hardcoded - not scalable
 |======> Combat Timer is overriding Regen() in GameTimer - NO REGEN FOR PLAYERS RIGHT NOW