## Narrative file syntax(.slum)
### Reserved Words
* BEGIN     - Denotes the beginning of a scene.
* END       - Denotes the end of a scene.
* SCENE [x] - Denotes number of scenes in the file.
* CONDITION - Denotes the condition for activating a narrative event
* |         - Separator between name and dialogue. Invalid in both.
### Line format
* [name]|[text to be said]
* Matt|This is what a line of dialogue looks like.
### Conditions
* Conditions are of the form "CONDITION variable predicate valueToCompare" and are placed at the beginning of a narrative event
#### Available variables
* Each tenant's happiness level can be accessed with the tenant's name
* Cash is available with the string "cash"
#### Available predicates
* ">=" - Checks if the left operand is equal to or greater than the right operand
### Notes
* ASCII encoding
* Each line of dialogue should occupy only one line in the file
* File should begin with SCENE [x] where x is the number of scenes in the document
* Any number of newlines before any BEGIN statement is valid
