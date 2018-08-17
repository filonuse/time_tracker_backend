import React, {Component} from 'react'
import styles from './tableStyle.css'
import * as Actions from './TableActions.jsx'
import TableBody from './TableBody.jsx'

export default function Table(props) {
    return (
        <div className="row">
            <div className="col-md-9 col-lg-7">
                <div className="card">
                    <div className="row">
                        <div className="col-md-12">
                            <table className="no-border no-margin table table-hover table-responsive">
                                <thead className={styles.thead_dark}>
                                <tr>
                                    <th
                                        className="md md-add"
                                        style={{cursor: "pointer"}}
                                        onClick={(event) => Actions.onIndicatorClick(event)}
                                    />
                                    <th>Name</th>
                                    <th>Nickname</th>
                                    <th>Work direction</th>
                                </tr>
                                </thead>
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                                <TableBody expandingRow={Actions.expandingRow} />
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
