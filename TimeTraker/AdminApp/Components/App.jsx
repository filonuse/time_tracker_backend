import React, { Component, createRef } from 'react'
import { connect } from 'react-redux'
import Header from './HeaderWrapper.jsx'
import MenuBar from './MenuWrapper.jsx'
import Content from './ContentWrapper.jsx'

export default function App(props) {
    return (
        <div id="menu-toggle">
            <Header />
            <MenuBar
                animate={true}
                inverseColor={false}
            />
            <Content />
        </div>
    );
}

