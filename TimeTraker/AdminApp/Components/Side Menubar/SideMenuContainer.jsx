import React, { Component } from 'react'
import { connect } from 'react-redux'

export default class SideMenuContainer extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.ul.firstChild.className += ' active'; 
    }
    
    render() {
        return (
            <div
                className="nano has-scrollbar"
            >
                <div className="nano-content" style={{ right: "-15px" }}>
                    <div className="menubar-scroll-panel" style={{ paddingBottom: "33px" }}>

                        <ul id="main-menu" className="gui-controls" ref={node => this.ul = node}>
                            {this.props.children}
                        </ul>

                        <div className="menubar-foot-panel">
                            <small className="no-linebreak hidden-folded">
                                <span className="opacity-75">Copyright © 2014</span> <strong>CodeCovers</strong>
                            </small>
                        </div>
                    </div>
                </div>
                <div className="nano-pane" style={{ display: "none" }}>
                    <div className="nano-slider" style={{ height: "584px", transform: "translate(0px, 0px)" }}>
                    </div>
                </div>
            </div>
        );
    }
}