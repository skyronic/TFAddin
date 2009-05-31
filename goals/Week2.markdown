# Goals for week 2
## June 1 to June 8

## Things to learn/explore
* What information is important in a bug report
* Study bug report frontends from Mylyn, Visual Studio Team Foundation, etc.
* Get ideas from Github's swanky issues functionality and try simplifying the interface with that in mind.
* How to refresh the ExtensibleTreeView from a NodeCommandHandler after drag/drop

## Things to plan/conceptualize
* Design Patterns, Design Patterns, Design Patterns
* How to store tasks, queries, and task providers
* How to represent them in objects
  * Should it be in one giant tree (ITaskData interface and children)
  * should it be more concrete and grouped into seperate trees and handled sepeately in the Pad's tree.
* Should the GUI be fixed or dynamically generated
* How to represent data in a task - keyvalue table or static members?
* How to ensure maximum flexibility for task providers
* Look at using an embedded sqlite database for tasks, querying comes free with this and we don't need to implement it again.
* Should there be a different task data class for each provider, or should the provider abstract the functionality enough such that the task can work independent of the provider.


## Things to do/implement
* create a new directory structure
* Create a new abstract class for task types, so that different kinds can be added on the fly, yet common things like reparenting should be handled once and not again.
* Create a new abstract default nodebuilder - capable of tying in with the task type so some operations are trivial
* Implement the Task editor and viewer. task editor must allow editing of values, but viewer must enable only commenting, closing, etc.
* Experiment with implementing a basic task provider.

## Bonus
* Implement a basic local task provider
