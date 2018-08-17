import React, { Component } from 'react'
import { onMenuItemClick } from './OnMenuActions.jsx'

export default class sideMenuElement extends Component {
    constructor(props) {
        super(props);
    }

    componentWillMount() {
        this.hasChildren = this.props.children ? true : false;
        this.children = React.Children.map(this.props.children, child => {
            return (
                React.cloneElement(child, {
                    onSubmenuItemClick: this.props.onSubmenuItemClick
                })
            )
        });
        this.childrenAmount = React.Children.count(this.props.children);
    }

    componentDidMount() {
        this.elementHeight = this.li.clientHeight;
        this.childElementHeight = document.getElementById('main-menu').querySelector('ul ul a').clientHeight;

        this.li.style.height = this.elementHeight + 'px';
        this.childElementsHeight = this.childrenAmount * this.childElementHeight;
    }

    render() {
        return (
            <li className={this.hasChildren ? 'gui-folder' : ''} ref={node => this.li = node} >
                <a onClick={(event) => onMenuItemClick(event, this.hasChildren, this.elementHeight, this.childElementsHeight)}>
                    <div className="gui-icon"><i className={this.props.icon}></i></div>
                    <span className="title">{this.props.value}</span>
                </a>


                <ul>
                    {this.children}
                </ul>
            </li>
        )
    }
}
