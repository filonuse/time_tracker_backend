import React from 'react'

export default function Skycons(props) {
    const icon = "fa-" + props.icon;

    return (
        <div className="gui-icon">
            <i className={"fa " + icon}></i>
        </div>
    );
}

