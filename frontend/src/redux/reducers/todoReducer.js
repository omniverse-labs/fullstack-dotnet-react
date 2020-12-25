export const initialState = {
    todos: [
        { id: 1, description: "todo one", isCompleted: false },
        { id: 2, description: "todo two", isCompleted: false },
        { id: 3, description: "todo three", isCompleted: true },
        { id: 4, description: "todo four", isCompleted: false },
        { id: 5, description: "todo fix", isCompleted: true },
    ]
};


export const reducer = (state = initialState, action) => {
    switch (action.type) {
        case 'SET_TODOS':
            return { ...state, todos: action.todos };
        case 'ADD_TODO':
            return { ...state, todos: [...state.todos, action.todo] };
        case 'REMOVE_TODO':
            return {
                ...state,
                todos: state.todos.filter(({ id }) => id !== action.id)
            };
        case 'UPDATE_TODO':
            return {
                ...state,
                todos: state.todos.map(todo => {
                    if (todo.id === action.todo.id) {
                        return action.todo;
                    } else {
                        return todo;
                    }
                })
            };
        default:
            return state;
    }
}