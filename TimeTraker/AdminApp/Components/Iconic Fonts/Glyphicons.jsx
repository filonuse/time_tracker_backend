import React from 'react'

export default function Skycons(props) {
    const icon = props.icon;

    return (
        <div class="gui-icon">
            <span class={"glyphicon " + icon}></span>
        </div>
    );
}