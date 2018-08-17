import React from 'react'
import Logo from './Header/Logo.jsx'
import BlockOfElemnts from './Header/BlockOfElements.jsx'
import InfoElement from './Header/InfoElement.jsx'

export default function Header(props) {
    return (
        <header id="header" >
            <div className="headerbar">
                <Logo />
                <BlockOfElemnts>
                    <InfoElement />
                </BlockOfElemnts>
            </div>
        </header>
    );
}
