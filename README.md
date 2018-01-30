# Interactive Systems - IID Runner

## Spielprinzip & Aufbau

**2D Runner mit 2 modularen Anwendungen**

* Prozedurale Levelgenrierung
	* Random based via Algorithmus
	* Erstellung der einzelnen Levelparts via Loops (abhängig zur Levellänge)
	* Vordefinierte maximale Höhen und Breiten für Elemente, abhängig von Sprunghöhe & Weite
	* 4 Elemente
		* Plates (Ground oder Bridge - random)
		* Other (Checkpoints, Finish)
		* Enemies (Rolling, Jumping, Following, Shooting) - random & abhängig von Schwierigkeitsgrad (Range)
		* Items (Unverwundbarkeit, Ammo, High-Jumping) - random & abhängig von Schwierigkeitsgrad (Range)
		
* Charakter-Editor
	* Gesteuert über Sprite-Manager
	* 3 austauschbare Elemente - Head, Chest, Legs
	
**Screens**

* Menu
* Editor
* Settings
	* Schwierigkeitsgrad
	* Levellänge
	* Sound-Toggle

## Herausforderungen

* Level muss immer machbar sein
* Angemessene Abstimmung der physikalischen Kräfte untereinander
* Erstellung der Spritesheets