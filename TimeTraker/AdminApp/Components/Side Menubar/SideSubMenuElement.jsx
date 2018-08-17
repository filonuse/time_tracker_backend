import React from 'react'
import { onSubmenuItemClick } from './OnMenuActions.jsx'

export default function SideSubMenuElement(props) {
    return (
        <li>
            <a onClick={(event) => onSubmenuItemClick(event)}>
                <span className="title">{props.value}</span>
            </a>
        </li>
    );
}
