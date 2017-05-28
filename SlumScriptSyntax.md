## Narrative file syntax(.slum)
### Note
* This will change when conditions are added
### Requirements
* ASCII encoding
* Each line of dialogue should occupy only one line in the file
* File should begin with SCENE [x] where x is the number of scenes in the document
### Reserved Words
* BEGIN     - Denotes the beginning of a scene.
* END       - Denotes the end of a scene.
* SCENE [x] - Denotes number of scenes in the file.
* |         - Separator between name and dialogue. Invalid in both.
### Line format
* [name]|[text to be said]
* Matt|This is what a line of dialogue looks like.