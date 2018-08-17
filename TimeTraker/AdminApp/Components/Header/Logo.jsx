import React from 'react'

export default function Logo(props) {
    return (
        <div className="headerbar-left">
            <ul className="header-nav header-nav-options">
                <li className="header-nav-brand" >
                    <div className="brand-holder">
                        <a href="../../html/dashboards/dashboard.html">
                            <span className="text-lg text-bold text-primary">Amcon Soft TRACKER</span>
                        </a>
                    </div>
                </li>
                <li>
                    <a id="menubar-toggle-button" className="btn btn-icon-toggle menubar-toggle" style={{ cursor: "pointer" }}>
                        <i className="fa fa-bars"></i>
                    </a>
                </li>
            </ul>
        </div>
    );
}