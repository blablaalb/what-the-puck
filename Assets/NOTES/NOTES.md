The checkpoints for real puck and the ghost puck behave differently.
- The real pucks checkpoint switches the `Hit` bool to true true after the puck collides with the checkpoints collider. 
- The ghost puck should wait until the player shoots the puck. Only after the player shoots the puck and the puck is whithin the checkpoint's collider the `Hit` state of the *checkpoint* changes to true.

# Builder
## Making ghost puck target
### Adding new puck target types
- In order to add a new puck target type we should:
    1. Add it to the `PuckTargetType` enum
    2. Add the `PuckTarget` component to the game object.
    3. Make the game object a prefab.
    4. Assign the prefab to the `PuckTargetInstantiator`.