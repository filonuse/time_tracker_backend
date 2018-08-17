import React, { Component } from 'react'
import { fail } from 'assert';

export default class Element extends Component {
    constructor(props) {
        super(props);
        this.openDropdownMenu = this.openDropdownMenu.bind(this);
        this.isOpened = false;
    }

    componentDidMount() {
        this.addClass = document.createAttribute('class');
        this.li.attributes.setNamedItem(this.addClass);
        this.addClass.value = 'dropdown';
    }

    openDropdownMenu() {
        this.isOpened = !this.isOpened;

        if (this.isOpened) {
            this.addClass.value += ' open';
        }
        else {
            this.addClass.value = this.addClass.value.replace(/\sopen/, '');
        }
    }

    render() {
        return (
            <li onClick={this.openDropdownMenu} ref={li => this.li = li}>
                <a href="javascript: void(0)" className="dropdown-toggle ink-reaction">
                    <img src="../../assets/img/avatar1.jpg?1403934956" alt="" />
                    <span className="profile-info">
                        Daniel Johnson
			        <small>Administrator</small>
                    </span>
                </a>
                <ul className="dropdown-menu animation-dock">
                    <li className="dropdown-header">Config</li>
                    <li><a href="../../html/pages/profile.html">My profile</a></li>
                    <li><a href="../../html/pages/blog/post.html">My blog <span className="badge style-danger pull-right">16</span></a></li>
                    <li><a href="../../html/pages/calendar.html">My appointments</a></li>
                    <li className="divider"></li>
                    <li><a href="../../html/pages/locked.html"><i className="fa fa-fw fa-lock"></i> Lock</a></li>
                    <li><a href="../../html/pages/login.html"><i className="fa fa-fw fa-power-off text-danger"></i> Logout</a></li>
                </ul>
            </li>
        )
    }
}
