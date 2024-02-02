# TODO

- Create IUserInput (and implementing classes PrioritizedUserInput and NonPrioritizedUserInput)
    - They have different implementations.
    - There must be a way to transfer NonPrioritizedUserInput to PrioritizedUserInput for when the user does something like Out.Of().Value(2).AddValue(3).WithPriority(..)
    - PrioritizedUserInput holds LinkedList<INonPrioritizedContainer> and the other has LinkedList<IPrioritizedContainer>