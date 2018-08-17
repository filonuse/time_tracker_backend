import React from 'react'

export default function Skycons(props) {
    const mdSize = props.middleScreenSize;
    const smSize = props.smallScreenSize;
    const iconType = props.skyconsType;

    return (
        <div className="col-md-2 col-sm-4 text-center" >
            <canvas width="100" height="100" dataType={iconType}></canvas>
        </div>
    );
}