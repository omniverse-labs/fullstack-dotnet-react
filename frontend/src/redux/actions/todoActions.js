import axios from 'axios';


export const addTodo = (todo) => ({
    type: 'ADD_TODO',
    todo
});

export const removeTodo = (id) => ({
    type: 'REMOVE_TODO',
    id
});

export const updateTodo = (todo) => ({
    type: 'UPDATE_TODO',
    todo
});

export const setTodos = (todos) => ({
    type: 'SET_TODOS',
    todos
});

export const setTodo = (todo) => ({
    type: 'SET_TODO',
    todo
});

export const startAddingTodo = (todo) => {
    return (dispatch, getState) => {
        axios
            .post(`${process.env.REACT_APP_TODO_SERVICE}/todo`, todo)
            .then(response => {
                dispatch(addTodo(response.data));
            })
            .catch(error => console.log(error));
    };
};

export const startDeletingTodo = (id) => {
    return (dispatch, getState) => {
        axios
            .delete(`${process.env.REACT_APP_TODO_SERVICE}/todo/${id}`)
            .then(response => {
                dispatch(removeTodo(id));
            })
            .catch(error => console.log(error));
    };
};

export const startUpdatingTodo = (todo) => {
    return (dispatch, getState) => {
        axios
            .put(`${process.env.REACT_APP_TODO_SERVICE}/todo/${todo.id}`, todo)
            .then(response => {
                dispatch(updateTodo(todo));
            })
            .catch(error => console.log(error));
    };
};

export const startSettingTodos = () => {
    return (dispatch, getState) => {
        axios
            .get(`${process.env.REACT_APP_TODO_SERVICE}/todo`)
            .then(response => {
                dispatch(setTodos(response.data));
            })
            .catch(error => console.log(error));
    };
};

export const startSettingTodo = (id) => {
    return (dispatch, getState) => {
        axios
            .get(`${process.env.REACT_APP_TODO_SERVICE}/todo/${id}`)
            .then(response => {
                dispatch(setTodo(response.data));
            })
            .catch(error => console.log(error));
    };
};