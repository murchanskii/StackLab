# StackLab

* A project for university. It shows several patterns which will be described later.

# Consept

There are two armies. They are represented by two lists. </br>
* **List of unit types:**
  + Light Infantry - can attack only in front of them, dress (put on armor or give a Horse) Heavy Infantry
  + Heavy Infantry - can attack only in front of them, has more ATK power and greater DEF
  + Archer - can attack in front of them, attack units in their Range
  + Healer - can attack in front of them, attack units in their ATKRange, heals allies in their HomeRange
  + Magician - can attack in front of them, attack units in their ATKRange, clones allies in their HomeRange
  + Protecting Wall - doesn't attack, can't be healed, has increased HP and DEF.
* **The turn logic:**
  + There are three stategies in this game. You can choose one out of the next list: 
    - Army stands like in queue, only first unit can "see" his opponent (Default)
    - Army splits into several rows by 3 units and each row act according the first strategy
    - Army splits one by one and each unit attacks the opposite side
  + Random generated value determines which army attacks first.
  + Unit at position 0 in the list attacks the opposite unit at position 0 from the other list.
  + If the second unit is alive, he attacks back.
  + Each capable unit from the first army attacks the second (only Ranged Units attack), heals/clones/dresses his allies
  + If unit is killed then it is deleted from the army at the exact same moment
  + The moves of Heavy Infantry are logged
  + Every death is logged
 * **The structure of patterns**
  + Singleton - game engine, menu for user.
  + Abstract factory - create units in an army
  + Prototype - clone units
  + Adapter - third-party dll (Protecting Wall)
  + Decorator - Heavy Unit dress up
  + Facade - the logic of Turn() in GameEngine
  + Proxy - Heavy Unit log
  + Command - undo/redo each act (not turn. turn consists of several acts. so here you can undo/redo half of the turn)
  + Observer - Console.Beep() and log into a .txt file each death
  + Strategy - army setting
