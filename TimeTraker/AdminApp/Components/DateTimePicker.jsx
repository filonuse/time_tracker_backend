import React from 'react';
import DateTimePicker from 'material-ui-datetimepicker';
import DatePickerDialog from 'material-ui/DatePicker/DatePickerDialog';
import TimePickerDialog from 'material-ui/TimePicker/TimePickerDialog';

class Demo extends React.Component {
    state = {
        dateTime: null
    }

    setDate = (dateTime) => this.setState({ dateTime })

    render() {
        return (
            <DateTimePicker
                onChange={this.setDate}
                DatePicker={DatePickerDialog}
                TimePicker={TimePickerDialog}
            />
        );
    }
}