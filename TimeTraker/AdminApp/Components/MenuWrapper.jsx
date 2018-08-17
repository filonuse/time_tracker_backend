import React, { Component } from 'react'
import SideMenuContainer from './Side Menubar/SideMenuContainer.jsx'
import SideMenuElement from './Side Menubar/SideMenuElement.jsx'
import SideSubMenuElement from './Side Menubar/SideSubMenuElement.jsx'
import { elementExpanding, elementCollapsing } from './Side Menubar/OnMenuActions.jsx'
import { setTimeout } from 'timers';

export default class MenuBar extends Component {
    constructor(props) {
        super(props);
        this.onMouseEvent = this.onMouseEvent.bind(this);
        this.menubarExpanded = false;
    }

    componentDidMount() {
        this.toggleElement = document.getElementById("menu-toggle");
        const menubar = document.getElementById('menubar');
        const menubarToggleButton = document.getElementById('menubar-toggle-button');
        const contentWrapper = document.getElementById('content').querySelector('section')
        const delayTime = 120   //ms

        // Set onClick event for the toggle button of menu.
        menubarToggleButton.onclick = () => this.onMouseEvent(this.menubarExpanded);

        // Seting onMouseEnter event for container of menu.
        // This event opens the menu
        menubar.onmouseenter = () => setTimeout(() => this.onMouseEvent(false), delayTime);

        // Setting onMouseEvent event for container of content.
        // This event closes the menu.
        contentWrapper.onmouseenter = () => setTimeout(() => this.onMouseEvent(true), delayTime);
    }

    // 
    onMouseEvent(expanded = false) {
        if (!expanded) {
            this.toggleElement.className = 'menubar-visible';
            this.menubarExpanded = true;

            // Adding className and style to the active element of menu.
            elementExpanding();
        }
        else {
            this.toggleElement.className = '';
            this.menubarExpanded = false;

            // Removing className and style from the active element of menu.
            elementCollapsing('expanded');
        }
    }

    render() {
        const inverseClassName = this.props.inverseColor ? "menubar" : "menubar-inverse";
        const animateClassName = this.props.animate ? "animate" : "";

        return (
            <div id="menubar" className={inverseClassName + " " + animateClassName}>
                <SideMenuContainer>
                    <SideMenuElement icon="md md-view-list" value="Обзор" />
                    <SideMenuElement icon="glyphicon glyphicon-calendar" value="Отчеты">
                        <SideSubMenuElement value="Текщий день" />
                        <SideSubMenuElement value="Произвольно" />
                    </SideMenuElement>
                    <SideMenuElement icon="fa fa-user" value="Сотрудники">
                        <SideSubMenuElement value="Все сотрудники" />
                        <SideSubMenuElement value="Активные" />
                        <SideSubMenuElement value="Неактивные" />
                    </SideMenuElement>
                    <SideMenuElement icon="fa fa-users" value="Группы" />
                    <SideMenuElement icon="fa fa-user-plus" value="Добавление сотрудника"/>
                </SideMenuContainer>
            </div>
        );
    }
}