import React from 'react'
import styles from './tableStyle.css'

export default function TableBody(props) {
    return (
        <tbody>
        <tr onClick={(event) => props.expandingRow(event)} data-state="collapsed">
            <td className={styles.expansion_indicator}><i className="md md-add" /></td>
            <td>props.name</td>
            <td>props.nickname</td>
            <td>props.workDirection</td>
        </tr>
        <tr>
            <td className="no-padding" colSpan="4">
                <div className={styles.expansible_container} style={{height: '0'}}>
                    <div className="card">
                        <div className="card-body">
                            {/*<div className="row">*/}
                                {/*<div className="col-xs-3">Full name:</div>*/}
                                {/*<div className="col-xs-9">Ashton Cox</div>*/}
                            {/*</div>*/}
                            {/*<div className="row">*/}
                                {/*<div className="col-xs-3">Extension number:</div>*/}
                                {/*<div className="col-xs-9">1562</div>*/}
                            {/*</div>*/}
                            <div className="row">
                                <div className="col-md-4 col-sm-6">
                                    <div className="card no-margin">
                                        <div className="card-body no-padding">
                                            <div className="alert alert-callout alert-warning no-margin">
                                                <strong className="pull-right text-warning text-lg">0,01% <i className="md md-swap-vert" /></strong>
                                                <strong className="text-xl">432,901</strong><br/>
                                                <span className="opacity-50">Visits</span>
                                                <div className="stick-bottom-right">
                                                    <div className="height-1 sparkline-visits" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-4 col-sm-6">
                                    <div className="card no-margin">
                                        <div className="card-body no-padding">
                                            <div className="alert alert-callout alert-danger no-margin">
                                                <strong className="pull-right text-danger text-lg">0,18% <i className="md md-trending-down" /></strong>
                                                <strong className="text-xl">42.90%</strong><br/>
                                                <span className="opacity-50">Bounce rate</span>
                                                <div className="stick-bottom-left-right">
                                                    <div className="progress progress-hairline no-margin">
                                                            <div className="progress-bar progress-bar-danger" style={{ "width": "43%" }} />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-4">
                                    <div className="card no-margin">
                                        <div className="card-body no-padding">
                                            <div className="alert alert-callout alert-info no-margin">
                                                <strong className="pull-right text-success text-lg">0,38% <i className="md md-trending-up" /></strong>
                                                <strong className="text-xl">$ 32,829</strong><br />
                                                <span className="opacity-50">Revenue</span>
                                                <div className="stick-bottom-left-right">
                                                    <div className="height-2 sparkline-revenue" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        </tbody>
    );
}