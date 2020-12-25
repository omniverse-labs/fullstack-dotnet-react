import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getTodos } from '../../redux/selectors/todoSelectors';
import TodoList from './TodoList';
import AddTodoForm from './AddTodoForm';
import {
    startAddingTodo,
    startDeletingTodo,
    startUpdatingTodo,
    startSettingTodos
} from '../../redux/actions/todoActions';

class TodoPage extends Component {
    componentDidMount() {
        this.props.startSettingTodos();
    }

    render() {
        return (
            <div>
                <TodoList todos={this.props.todos}
                    onTodoUpdating={this.props.startUpdatingTodo}
                    onTodoDeleting={this.props.startDeletingTodo}
                />
                <AddTodoForm handleSubmit={this.props.startAddingTodo} />
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        todos: getTodos(state)
    }
}

const mapDispatchToProps = (dispatch) => ({
    startAddingTodo: bindActionCreators(startAddingTodo, dispatch),
    startDeletingTodo: bindActionCreators(startDeletingTodo, dispatch),
    startUpdatingTodo: bindActionCreators(startUpdatingTodo, dispatch),
    startSettingTodos: bindActionCreators(startSettingTodos, dispatch),
});

export default connect(mapStateToProps, mapDispatchToProps)(TodoPage)