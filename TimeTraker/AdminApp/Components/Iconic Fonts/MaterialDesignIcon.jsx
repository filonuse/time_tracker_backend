import React from 'react'

export default function Skycons(props) {
    const icon = props.icon;

    return (
        <div className="gui-icon">
            <i className={"md " + icon}></i>
        </div>
    );
}