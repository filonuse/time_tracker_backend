import React from 'react'
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import { createStore } from 'redux'
import rootReducer from './Reducers/index.jsx'
import App from './Components/App.jsx'

const store = createStore(rootReducer);

//// (Tests) Loading data from store to console
//console.log(store.getState())
//const unsubscribe = store.subscribe(() =>
//    console.log(store.getState())
//)
//// (Tests) End

render(
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('wrapper')
)