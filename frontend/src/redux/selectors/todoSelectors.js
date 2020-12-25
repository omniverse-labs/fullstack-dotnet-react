import { createSelector } from 'reselect';

const getTodoState = (state) => state.todos;

export const getTodos = createSelector(
    [getTodoState],
    s => s.todos
);