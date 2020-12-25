import { combineReducers } from 'redux';
import * as fromTodo from './todoReducer';

export const initialState = {
    todos: fromTodo.initialState
};

export const rootReducer = combineReducers({
    todos: fromTodo.reducer
});