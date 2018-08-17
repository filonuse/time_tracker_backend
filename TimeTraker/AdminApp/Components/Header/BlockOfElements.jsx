import React from 'react'
import InfoElement from './InfoElement.jsx'

export default function BlockOfElements(props) {
    return (
        <div className="headerbar-right">
            <ul className="header-nav header-nav-profile">
                {props.children}
            </ul>
        </div>
    );
}
