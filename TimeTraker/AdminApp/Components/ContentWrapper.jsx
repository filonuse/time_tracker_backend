import React from 'react'
import Table from './Expanding Table/Table.jsx'
import styles from './loader.css'
import DateTimePicker from './DateTimePicker.jsx'

export default function Content(props) {
    let ref;
    let date;
    const onChange = () => {
        date = new Date();
    };

    return (
        <div id="base">
            <div id="content">
                <section>
                    <div className="row">
                        <div ref={node => ref = node}></div>
                    </div>

                    <div className="row">
                        <div className="col-md-3 col-sm-6">
                            <div className="card">
                                <div className="card-body no-padding" onClick={() => ref.className = styles.loader}>
                                    <div className="alert alert-callout alert-info no-margin">
                                        <strong className="pull-right text-success text-lg">0,38% <i className="md md-trending-up"></i></strong>
                                        <strong className="text-xl">$ 32,829</strong><br />
                                        <span className="opacity-50">Revenue</span>
                                        <div className="stick-bottom-left-right">
                                            <div className="height-2 sparkline-revenue"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="col-md-3 height-5">
                            <div className="card">
                                <div className="card-body no-padding">
                                </div>
                            </div>
                        </div>
                    </div>


                    <Table/>

                </section>
            </div>
        </div>
    );
}