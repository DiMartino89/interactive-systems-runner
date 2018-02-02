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
		 * Beinhaltet 3 GameObject fields fuer Kopf, Oberkoerper und Unterkoerper die einen Canvas Renderer
		 * Managed 3 Sprite array welches alle Sprites fuer Kopf Ober- und Unterkoeper beihnalten die im Spiel verwendet werden
		 * Alle AnimationController die Anhand von den Sprites generiert wurden um eine Verknuepfung zu ermoeglichen
	* Bedingung zur Funktion:
		* Sprite und AnimationController sollte gleich benannt sein

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
* Spieler muss zur Runtime mit den neusten Konfigurationen instanziert werden

## Technologie, Voraussetzungen und Aufsetzen des Games

* Plattform: Unity 2017.2.0f3
* Aufsetzen: 
	* git clone https://github.com/DiMartino89/interactive-systems-runner.git
	* Target-Branch = master (oder develop)
	* Am besten die Menu-Scene aus dem Editor starten, von dort gelangt man dann an alle Features
	* Anmerkung: Build war zwar möglich, jedoch wurden nicht alle Features wie gewünscht adapted, daher bitte den Editor nutzen
